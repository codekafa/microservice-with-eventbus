using EventBus.Base.Events;
using System;

namespace IdentityService.Api.Events.IntegrationEvents
{
    public class UserLoginIntegrationEvent : IntegrationEvent
    {
        public UserLoginIntegrationEvent(Guid id, DateTime createDate) : base(id, createDate)
        {
        }
    }
}
