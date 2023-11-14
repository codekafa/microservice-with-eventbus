using Domain.CQRS.Identity.Commands.Response;
using MediatR;

namespace Domain.CQRS.Identity.Commands.Request
{

    public class CreateTokenRequest : IRequest<CreateTokenResponse>
    {
        private string _userName;
        private string _password;
        public CreateTokenRequest(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }
    }
}
