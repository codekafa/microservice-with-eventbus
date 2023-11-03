using Domain.Dto.IdentityService;

namespace Domain.Infrastructure.IdentityService
{
    public interface IAuthService
    {

        Task<TokenDto> GetToken(LoginDto login);

    }
}