using EventBus.Base.Abstructure;
using EventBus.Base.Events;
using System.Threading.Tasks;

namespace NotificationService.Console.Events.Handlers
{
    public class IntegrationEventHandlers : IIntegrationEventHandler<UserLoginIntegrationEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {
            //todo result

            string a = "";

            return Task.CompletedTask;
        }
    }
}
