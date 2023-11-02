using Domain.Dto.IdentityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.IdentityService
{
    public class AuthService
    {

        public async Task<TokenDto> GetToken(LoginDto getLogin)
        {
            return new TokenDto();
        }

    }
}
