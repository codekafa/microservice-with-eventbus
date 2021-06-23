using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ
{
    public class RabbitMQPersistentConnection : IDisposable
    {

        private IConnection connection;

        private IConnectionFactory factory;

        private object lock_object = new object();

        private int retryCount;
        public RabbitMQPersistentConnection(IConnectionFactory factory, int retryCount = 5)
        {
            this.factory = factory;
            this.retryCount = retryCount;
        }

        public bool IsConnection => connection != null && connection.IsOpen;



        public IModel CreateModel()
        {
            return connection.CreateModel();
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public bool TryConnect()
        {

            lock (lock_object)
            {
                var pol = Policy.Handle<SocketException>().Or<BrokerUnreachableException>().WaitAndRetry(retryCount, retyAttempt => TimeSpan.FromSeconds(Math.Pow(2, retyAttempt)), (ex, time) => { }

               );

                pol.Execute(() =>
                (
                    connection = factory.CreateConnection()
                )); ;
            }

            return IsConnection;

        }
    }
}
