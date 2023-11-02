using Domain.Dto.IdentityService;

namespace Domain.Infrastructure.IdentityService
{
    public interface IAuthService
    {

        public Task<TokenDto> GetToken(LoginDto login);

    }
}