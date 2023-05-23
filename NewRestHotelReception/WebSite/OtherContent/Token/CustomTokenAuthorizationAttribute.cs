using Microsoft.AspNetCore.Mvc.Filters;

namespace WebSite.OtherContent.Token
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CustomTokenAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            // Create an instance of the custom token authorization filter
            var tokenAuthorizationFilter = new TokenAuthorizationFilter();

            // Invoke the custom token authorization filter
            tokenAuthorizationFilter.OnAuthorization(filterContext);
        }
    }
}
