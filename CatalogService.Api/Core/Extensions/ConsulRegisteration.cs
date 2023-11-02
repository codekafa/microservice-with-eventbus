using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CatalogService.Api.Core.Extensions
{
    public static class ConsulRegisteration
    {

        public static IServiceCollection ConfigureConsul(this IServiceCollection services, IConfiguration config)
        {

            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient
            (consulConfig =>
            {
                var address = config["ConsulConfig:Address"];
                consulConfig.Address = new System.Uri(address);
            }
            ));


            return services;
        }


        public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifeTime)
        {

            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();  
            var logginFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            var logger = logginFactory.CreateLogger<IApplicationBuilder>();

            var features = app.Properties["server.Features"] as FeatureCollection;

            var addresses = features.Get<IServerAddressesFeature>();

            var address = addresses.Addresses.FirstOrDefault();

            var uri = new Uri(address);


            var registration = new AgentServiceRegistration()
            {
                ID = $"CatalogService",
                Name = "CatalogService",
                Address = $"{uri.Host}",
                Port = uri.Port,
                Tags = new[] { "Catalog Service", "Catalog" }
            };

            logger.LogInformation("Registration with consul for catalog service");

            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            lifeTime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("De registered catalog service!");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });


            return app;
        }

    }
}
