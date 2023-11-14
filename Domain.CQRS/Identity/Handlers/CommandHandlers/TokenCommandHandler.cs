using Domain.CQRS.Identity.Commands.Request;
using Domain.CQRS.Identity.Commands.Response;
using Domain.Dto.IdentityService;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Identity.Handlers.CommandHandlers
{
    public class TokenQueryHandler : IRequestHandler<CreateTokenRequest, TokenDto>
    {
        IConfiguration _configuration;
        public TokenQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<TokenDto> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
        {
            var claims = new[] {
                        new Claim("UserName", request.UserName),
                        new Claim("UserID", new Random().Next(9999,99999).ToString()),
                    };

            string baseKey = _configuration["Jwt:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(baseKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var expirationTimeStamp = DateTime.Now.AddMinutes(30);

            var token = new JwtSecurityToken(
             issuer: "https://localhost:5004",
             claims: claims,
             expires: expirationTimeStamp,
             signingCredentials: signIn
         );

            var tokenDto = new TokenDto();
            tokenDto.Token = new JwtSecurityTokenHandler().WriteToken(token);
            tokenDto.ExpireDate = DateTime.UtcNow.AddMinutes(30);

            return tokenDto;
        }
    }
}
