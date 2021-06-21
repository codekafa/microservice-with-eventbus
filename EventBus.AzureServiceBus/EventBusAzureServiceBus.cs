using EventBus.Base;
using EventBus.Base.Abstructure;
using EventBus.Base.Events;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.AzureServiceBus
{
    public class EventBusAzureServiceBus : BaseEventBus
    {

        private ITopicClient topicClient;
        private ManagementClient managementClient;
        private ILogger logger;
        public EventBusAzureServiceBus(EventBusConfig eventBusConfig, IServiceProvider serviceProvider) : base(eventBusConfig, serviceProvider)
        {
            managementClient = new ManagementClient(eventBusConfig.EventBusConnectionString);
            topicClient = createTopicClient();
            logger = ServiceProvider.GetService(typeof(ILogger<EventBusAzureServiceBus>)) as ILogger<EventBusAzureServiceBus>;
        }
        private ITopicClient createTopicClient()
        {
            if (topicClient == null || topicClient.IsClosedOrClosing)
            {
                topicClient = new TopicClient(EventBusConfig.EventBusConnectionString, EventBusConfig.DefaultTopicName, RetryPolicy.Default);
            }

            if (!managementClient.TopicExistsAsync(EventBusConfig.DefaultTopicName).GetAwaiter().GetResult())
            {
                managementClient.CreateTopicAsync(EventBusConfig.DefaultTopicName).GetAwaiter().GetResult();
            }

            return topicClient;
        }
        public override void Publish(IntegrationEventHandler @event)
        {

            var eventName = @event.GetType().Name;
            eventName = ProcessEventName(eventName);
            var eventStr = JsonConvert.SerializeObject(@event).ToCharArray();
            var bodyArr = Encoding.UTF8.GetBytes(eventStr);

            Message message = new Message();
            message.MessageId = Guid.NewGuid().ToString();
            message.Body = bodyArr;
            message.Label = eventName;

            topicClient.SendAsync(message).GetAwaiter().GetResult();

        }
        public override void Subscribe<T, TH>()
        {
            var eventName = typeof(T).Name;
            eventName = ProcessEventName(eventName);

            if (!SubsManager.HasSubscribeForEvent(eventName))
            {
                var subClient = createSubscriptionClientIfNotExist(eventName);
                RegisterSubscribeClient(subClient);
            }
            SubsManager.AddSubscribe<T, TH>();
        }
        private void RegisterSubscribeClient(ISubscriptionClient subClient)
        {
            subClient.RegisterMessageHandler(
                async (message, token) =>
                {
                    var eventName = $"{message.Label}";
                    var messageData = Encoding.UTF8.GetString(message.Body);

                    if (await ProcessEvent(ProcessEventName(eventName), messageData))
                    {
                        await subClient.CompleteAsync(message.SystemProperties.LockToken);
                    }

                },
                new MessageHandlerOptions(ExceptionReceivedHandler) { MaxConcurrentCalls = 10, AutoComplete = false });

        }
        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {

            var ex = exceptionReceivedEventArgs.Exception;
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            logger.LogError(ex, ex.Message);

            return Task.CompletedTask;
        }
        public override void UnSubscribe<T, TH>()
        {


            var eventName = typeof(T).Name;


            try
            {
                var subClient = createSubscriptionClient(eventName);

                subClient.RemoveRuleAsync(eventName).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
            }


            SubsManager.RemoveSubscribe<T, TH>();

        }
        private void RemoveDefaultRule(SubscriptionClient subClient)
        {

            try
            {
                subClient.RemoveRuleAsync(RuleDescription.DefaultRuleName).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
            }

        }
        private ISubscriptionClient createSubscriptionClientIfNotExist(string eventName)
        {
            var subClient = createSubscriptionClient(eventName);

            if (!managementClient.SubscriptionExistsAsync(EventBusConfig.DefaultTopicName, GetSubName(eventName)).GetAwaiter().GetResult())
            {
                managementClient.CreateSubscriptionAsync(EventBusConfig.DefaultTopicName, GetSubName(eventName)).GetAwaiter().GetResult();
                RemoveDefaultRule(subClient);
            }

            CreateRuleIfNotExist(ProcessEventName(eventName), subClient);

            return subClient;

        }
        private void CreateRuleIfNotExist(string eventName, ISubscriptionClient subClient)
        {
            bool ruleExist;

            try
            {
                var rule = managementClient.GetRuleAsync(EventBusConfig.DefaultTopicName, eventName, eventName).GetAwaiter().GetResult();
                ruleExist = rule != null;

            }
            catch (Exception ex)
            {
                ruleExist = false;
            }

            if (!ruleExist)
            {
                subClient.AddRuleAsync(new RuleDescription { Filter = new CorrelationFilter { Label = eventName }, Name = eventName }).GetAwaiter().GetResult();
            }




        }
        private SubscriptionClient createSubscriptionClient(string eventName)
        {
            return new SubscriptionClient(EventBusConfig.EventBusConnectionString, EventBusConfig.DefaultTopicName, GetSubName(eventName));
        }

    }
}
