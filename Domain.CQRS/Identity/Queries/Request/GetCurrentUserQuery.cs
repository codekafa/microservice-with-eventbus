using Domain.Dto.IdentityService;
using MediatR;

namespace Domain.CQRS.Identity.Queries.Request
{
    public class GetCurrentUserQuery : IRequest<UserDto>
    {
        private string _token;
        public GetCurrentUserQuery(string token)
        {
            _token = token;
        }

        public string Token { get { return _token; } }
    }
}
