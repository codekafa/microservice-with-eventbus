using EventBus.Base;
using EventBus.Base.Abstructure;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Console.IntegrationEvents.Events;
using NotificationService.Console.IntegrationEvents.Handlers;
using System;

namespace NotificationService.Console
{
    class Program
    {
        static void Main(string[] args)
        {


            ServiceCollection services = new ServiceCollection();


            services.AddTransient<OrderFailedIntegrationEventHandler>();
            services.AddTransient<OrderSuccessIntegrationEventHandler>();

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig
                {
                    ConnectionRetryCount = 5,
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    SubscribeClientAppName = "NotificationService"
                };
                return EventBusFactory.Create(config, sp);
            });




            var sp = services.BuildServiceProvider();

            IEventBus eventBus = sp.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderSuccessEvent, OrderSuccessIntegrationEventHandler>();
            eventBus.Subscribe<OrderFailedEvent, OrderFailedIntegrationEventHandler>();


        }
    }
}
