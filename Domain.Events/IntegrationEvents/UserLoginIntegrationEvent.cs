using EventBus.Base.Events;

namespace Domain.Events.IntegrationEvents
{
    public class UserLoginIntegrationEvent : IntegrationEvent
    {

        private Domain.Dto.IdentityService.UserDto _user;
        public UserLoginIntegrationEvent(Guid id, DateTime createDate, Domain.Dto.IdentityService.UserDto user) : base(id, createDate)
        {
            _user = user;
        }
    }
}
