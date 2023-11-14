using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CatalogService.Api.Core.Filters
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult(string.Empty);
                return;
            }

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

                token = token.Replace("Bearer ", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwt = (JwtSecurityToken)validatedToken;

                return;
            }
            catch (SecurityTokenValidationException ex)
            {
                var str = "";
            }
        }
    }
}
