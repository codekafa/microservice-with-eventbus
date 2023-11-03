using Domain.Dto.IdentityService;
using Domain.Infrastructure.IdentityService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Business.IdentityService
{
    public class AuthService : IAuthService
    {

        public IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TokenDto> GetToken(LoginDto getLogin)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", getLogin.UserName),
                        new Claim("UserID", new Random().Next(9999,99999).ToString()),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signIn);

            var tokenDto = new TokenDto();
            tokenDto.Token = new JwtSecurityTokenHandler().WriteToken(token);
            tokenDto.ExpireDate = DateTime.UtcNow.AddMinutes(30);

            return tokenDto;
        }

    }
}
