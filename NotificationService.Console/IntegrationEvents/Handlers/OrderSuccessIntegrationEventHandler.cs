using EventBus.Base.Abstructure;
using EventBus.Base.Events;
using NotificationService.Console.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Console.IntegrationEvents.Handlers
{
    public class OrderSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderSuccessEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {

            // success event payment

            throw new NotImplementedException();
        }
    }
}
