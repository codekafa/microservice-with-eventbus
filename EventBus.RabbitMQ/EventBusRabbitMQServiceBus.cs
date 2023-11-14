using EventBus.Base;
using EventBus.Base.Abstructure;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQServiceBus : BaseEventBus
    {
        private RabbitMQPersistentConnection pCon;

        private readonly IConnectionFactory factory;

        private readonly IModel consumeChannel;
        public EventBusRabbitMQServiceBus(EventBusConfig eventBusConfig, IServiceProvider serviceProvider) : base(eventBusConfig, serviceProvider)
        {
            if (EventBusConfig.Connection != null)
            {
                var conJson = JsonConvert.SerializeObject(eventBusConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                factory = JsonConvert.DeserializeObject<ConnectionFactory>(conJson);

            }
            else
            {
                factory = new ConnectionFactory();
                factory.UserName = "user";
                factory.Password = "bitnami";
            }

            pCon = new RabbitMQPersistentConnection(factory, eventBusConfig.ConnectionRetryCount);

            consumeChannel = CreateChannel();

            SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;

        }

        private void SubsManager_OnEventRemoved(object sender, string e)
        {

            e = ProcessEventName(e);


            if (!pCon.IsConnection)
            {
                pCon.TryConnect();
            }

            consumeChannel.QueueUnbind(queue: e, exchange: EventBusConfig.DefaultTopicName, routingKey: e);

            if (SubsManager.IsEmty)
            {
                consumeChannel.Close();
            }

        }

        public override void Publish(IntegrationEventHandler @event)
        {

            if (!pCon.IsConnection)
            {
                pCon.TryConnect();
            }

            var pol = Policy.Handle<SocketException>().Or<BrokerUnreachableException>().WaitAndRetry(EventBusConfig.ConnectionRetryCount, retyAttempt => TimeSpan.FromSeconds(Math.Pow(2, retyAttempt)), (ex, time) => { });


            var eventName = @event.GetType().Name;

            eventName = ProcessEventName(eventName);


            consumeChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");

            var message = JsonConvert.SerializeObject(@event);


            var body = Encoding.UTF8.GetBytes(message);

            pol.Execute(() =>
            {

                var properties = consumeChannel.CreateBasicProperties();

                properties.DeliveryMode = 2;


                consumeChannel.BasicPublish(exchange: EventBusConfig.DefaultTopicName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);

            });


        }

        public override void Subscribe<T, TH>()
        {
            var eventName = typeof(T).Name;
            eventName = ProcessEventName(eventName);

            if (!SubsManager.HasSubscribeForEvent(eventName))
            {
                consumeChannel.QueueDeclare(queue: GetSubName(eventName),

                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                consumeChannel.QueueBind(queue: GetSubName(eventName), exchange: EventBusConfig.DefaultTopicName, routingKey: eventName);
            }

            SubsManager.AddSubscribe<T, TH>();

            StartBasicConsume(eventName);
        }

        public override void UnSubscribe<T, TH>()
        {
            SubsManager.RemoveSubscribe<T, TH>();
        }

        public IModel CreateChannel()
        {
            if (!pCon.IsConnection)
            {
                pCon.TryConnect();
            }

            var channel = pCon.CreateModel();

            channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");

            return channel;
        }

        public void StartBasicConsume(string eventName)
        {

            if (consumeChannel != null)
            {
                var consumer = new EventingBasicConsumer(consumeChannel);
                consumer.Received += Consumer_Received;
                consumeChannel.BasicConsume(
                    queue: GetSubName(eventName),
                    autoAck: false,
                    consumer: consumer);
            }

        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {

            var eventName = e.RoutingKey;

            eventName = ProcessEventName(eventName);


            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {

            }

            consumeChannel.BasicAck(e.DeliveryTag, multiple: false);

        }
    }
}
