using EventBus.Base.Abstructure;
using EventBus.Base.SubManagers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base.Events
{
    public abstract class BaseEventBus : IEventBus
    {


        public readonly IServiceProvider ServiceProvider;
        public readonly IEventBusSubscribeManager SubsManager;
        public EventBusConfig EventBusConfig { get; set; }


        public BaseEventBus(EventBusConfig eventBusConfig, IServiceProvider serviceProvider)
        {
            this.EventBusConfig = eventBusConfig;
            ServiceProvider = serviceProvider;
            SubsManager = new InMemoryEventBusSubscribeManager(ProcessEventName);
        }

        public virtual string ProcessEventName(string eventName)
        {
            if (EventBusConfig.DeleteEventPrefix)
                eventName = eventName.TrimStart(EventBusConfig.EventNamePrefix.ToArray());

            if (EventBusConfig.DeleteEventSuffix)
                eventName = eventName.TrimEnd(EventBusConfig.EventNameSuffix.ToArray());

            return eventName;
        }


        public virtual string GetSubName(string eventName)
        {
            return $"{EventBusConfig.SubscribeClientAppName}.{ProcessEventName(eventName)}";
        }


        public virtual void Dispose()
        {
            EventBusConfig = null;
            SubsManager.Clear();
        }

        public async Task<bool> ProcessEvent(string eventName, string message)
        {

            eventName = ProcessEventName(eventName);

            var processed = false;


            if (SubsManager.HasSubscribeForEvent(eventName))
            {

                var subscriptions = SubsManager.GetHandlersForEvent(eventName);


                using (var scope = ServiceProvider.CreateScope())
                {
                    foreach (var subscription in subscriptions)
                    {

                        var handler = ServiceProvider.GetService(subscription.HandlerType);

                        if (handler == null)
                            continue;

                        var eventType = SubsManager.GetEventTypeByName($"{EventBusConfig.EventNamePrefix}{eventName}{EventBusConfig.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        var concreateType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreateType.GetMethod("handle").Invoke(handler, new object[] { integrationEvent });

                    }
                }

                processed = true;
            }

            return processed;


        }

        public abstract void Publish(IntegrationEventHandler @event);

        public abstract void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;


        public abstract void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
    }
}
