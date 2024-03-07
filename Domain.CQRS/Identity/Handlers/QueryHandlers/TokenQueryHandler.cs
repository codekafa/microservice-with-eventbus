using Domain.CQRS.Identity.Queries.Request;
using Domain.Dto.IdentityService;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Domain.CQRS.Identity.Handlers.QueryHandlers
{
    public class TokenQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        IConfiguration _configuration;
        public TokenQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tsad234mnsdfg@4532knjsdf_2346325&23412312325fdsvgsdknfbu234"));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = "",
                ValidateAudience = false,
                ValidAudience = "",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            try
            {

                var token = request.Token.Replace("Bearer ", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwt = (JwtSecurityToken)validatedToken;
                string user = jwt.Claims.First(c => c.Type == "UserName").Value;
                string userId = jwt.Claims.First(c => c.Type == "UserID").Value;
                return new UserDto { UserName = user, UserID = userId };

            }
            catch (SecurityTokenValidationException ex)
            {
                return new UserDto { UserName = "", UserID = "" };
            }

            //bugfix1
        }
    }
}
