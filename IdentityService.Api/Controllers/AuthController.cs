using Domain.Dto.IdentityService;
using Domain.Infrastructure.IdentityService;
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
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        [Route("createtoken")]
        public async Task<TokenDto> CreateToken(LoginDto dto)
        {
            return await  _authService.GetToken(dto);
        }
    }
}
