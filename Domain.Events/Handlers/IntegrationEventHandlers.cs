using Domain.Events.IntegrationEvents;
using EventBus.Base.Abstructure;
using EventBus.Base.Events;

namespace Domain.Events.Handlers
{
    public class IntegrationEventHandlers : IIntegrationEventHandler<UserLoginIntegrationEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {
            //todo result

            return Task.CompletedTask;
        }
    }
}
