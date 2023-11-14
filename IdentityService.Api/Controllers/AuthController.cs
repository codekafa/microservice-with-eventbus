using Domain.CQRS.Identity.Commands.Request;
using Domain.CQRS.Identity.Commands.Response;
using Domain.CQRS.Identity.Queries.Request;
using Domain.Dto.IdentityService;
using IdentityService.Api.Core.Filters;
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
        public async Task<TokenDto> CreateToken(LoginDto dto)
        {
            return await _mediator.Send<TokenDto>(new CreateTokenRequest( dto.UserName, dto.Password));
        }

       
        [AuthFilter]
        [Route("getuser")]
        public async Task<UserDto> GetUser()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            return await _mediator.Send<UserDto>(new GetCurrentUserQuery(token));
        }
    }
}
