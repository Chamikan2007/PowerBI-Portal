using Altria.PowerBIPortal.Domain;
using System.Security.Claims;

namespace Altria.PowerBIPortal.Application.Middleware;

public class RequestContextResolverMiddleware
{
    private readonly RequestDelegate _next;

    public RequestContextResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, RequestContext requestContext)
    {
        var claims = context.User;
        var isSignedIn = claims.Identity!.IsAuthenticated;
        if (isSignedIn)
        {
            requestContext.UserId = Guid.Parse(claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            requestContext.DisplayName = claims.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            requestContext.Email = claims.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        }

        await _next(context);
    }
}
