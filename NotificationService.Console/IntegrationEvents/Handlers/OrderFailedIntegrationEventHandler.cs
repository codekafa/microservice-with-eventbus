using EventBus.Base.Abstructure;
using EventBus.Base.Events;
using NotificationService.Console.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Console.IntegrationEvents.Handlers
{
    public class OrderFailedIntegrationEventHandler : IIntegrationEventHandler<OrderFailedEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {

            // failed event payment

            throw new NotImplementedException();
        }
    }
}
