using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Base.Abstructure
{
    public  interface IEventBus
    {
        void Publish(IntegrationEventHandler @event);

        void Subscribe<T, TH>() where T: IntegrationEvent where TH : IIntegrationEventHandler<T>;

        void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

    }
}
