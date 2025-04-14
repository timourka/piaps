using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
public static class SessionStore
{
    // sid -> workerId
    public static Dictionary<string, int> Sessions = new();
}
public class AuthorizeWithSidAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var sid = context.HttpContext.Request.Headers["sid"].FirstOrDefault();

        if (string.IsNullOrEmpty(sid) || !SessionStore.Sessions.ContainsKey(sid))
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                error = "Unauthorized: Invalid or missing SID"
            });
        }
    }
}
