using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using POSWebClient.Models;

namespace POSWebClient.Extensions
{
    public sealed class AuthorizeUserAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var sessionInfo = context.HttpContext.Session.GetObject<UserSessionInfo>("UserSessionInfo");

                if (sessionInfo == null)
                {
                    context.Result = new RedirectResult("/");
                }
            }
        }
    }
}