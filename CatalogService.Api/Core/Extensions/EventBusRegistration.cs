using EventBus.Base;
using EventBus.Base.Abstructure;
using EventBus.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CatalogService.Api.Core.Extensions
{
    public static class EventBusRegistration
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration _configuration)
        {

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig
                {
                    ConnectionRetryCount = 5,
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    SubscribeClientAppName = "CatalogService",
                    EventBusConnectionString = "http://localhost:5672"
                };
                return EventBusFactory.Create(config, sp);
            });

            var sp = services.BuildServiceProvider();
            IEventBus eventBus = sp.GetRequiredService<IEventBus>();
            return services;

        }

        public static IApplicationBuilder RegisterWithEvents(this IApplicationBuilder app, IHostApplicationLifetime lifeTime)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            return app;
        }
    }
}
