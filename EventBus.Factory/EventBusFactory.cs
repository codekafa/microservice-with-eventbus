using EventBus.AzureServiceBus;
using EventBus.RabbitMQ;
using EventBus.Base;
using EventBus.Base.Abstructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Factory
{
    public class EventBusFactory
    {

        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
        {
            return config.EventBusType switch
            {
                EventBusType.AzureServiceBus => new EventBusAzureServiceBus(config, serviceProvider),
                _ => new EventBusRabbitMQServiceBus(config, serviceProvider),
            };
        }

    }
}
