using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace WebSite.OtherContent.Token
{
    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            string token = TokenResponse.Token;
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var jwtHandler = new JwtSecurityTokenHandler();
            // Read the token without validating it
            var jwtToken = jwtHandler.ReadToken(TokenResponse.Token) as JwtSecurityToken;

            if (jwtToken.ValidTo < DateTime.UtcNow) 
            {
                // No token provided, return unauthorized
                context.Result = new UnauthorizedResult();
                return;
            }
            else
            {
                context.Result = null;
            }
            
        }
    }
}
