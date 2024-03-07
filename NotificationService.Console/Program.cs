using Domain.Events.Handlers;
using Domain.Events.IntegrationEvents;
using EventBus.Base;
using EventBus.Base.Abstructure;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace NotificationService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<IntegrationEventHandlers>();

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig
                {
                    ConnectionRetryCount = 5,
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    SubscribeClientAppName = "NotificationService",
                    EventBusConnectionString = "http://localhost:5672/"
                };
                return EventBusFactory.Create(config, sp);
            });


            //hasine 1 geliştirme

            var sp = services.BuildServiceProvider();

            IEventBus eventBus = sp.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserLoginIntegrationEvent, IntegrationEventHandlers>();

        }
    }
}
