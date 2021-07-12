using EventBus.Base.Abstructure;
using EventBus.Base.Events;
using PaymentService.Api.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Api.IntegrationEvents.Handlers
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {

            // start event payment

            throw new NotImplementedException();
        }
    }
}
