using Domain.Dto.IdentityService;
using MediatR;

namespace Domain.CQRS.Identity.Commands.Request
{

    public class CreateTokenRequest : IRequest<TokenDto>
    {
        private string _userName;
        private string _password;
        public CreateTokenRequest(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public string UserName { get { return _userName; } }

        public string Password { get { return _password; } }
    }
}
