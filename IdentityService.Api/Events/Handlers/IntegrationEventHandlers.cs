using EventBus.Base.Abstructure;
using EventBus.Base.Events;
using IdentityService.Api.Events.IntegrationEvents;
using System.Threading.Tasks;

namespace IdentityService.Api.Events.Handlers
{
    public class IntegrationEventHandlers : IIntegrationEventHandler<UserLoginIntegrationEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
