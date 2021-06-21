using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base.Abstructure
{
    public interface IIntegrationEventHandler<TIntegrationEvent> : IntegrationEventHandler where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(IntegrationEvent @event);
    }

    public interface IntegrationEventHandler
    {

    }

}
