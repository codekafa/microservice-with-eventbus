using Domain.CQRS.Identity.Commands.Request;
using Domain.CQRS.Identity.Commands.Response;
using Domain.Dto.IdentityService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace IdentityService.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger,IMediator mediator )
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("createtoken")]
        public async Task<CreateTokenResponse> CreateToken(LoginDto dto)
        {
            return await _mediator.Send<CreateTokenResponse>(new CreateTokenRequest( dto.UserName, dto.Password));
        }
    }
}
