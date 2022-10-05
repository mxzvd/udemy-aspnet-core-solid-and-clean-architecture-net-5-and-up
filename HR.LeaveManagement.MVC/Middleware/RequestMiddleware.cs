using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;

namespace HR.LeaveManagement.MVC.Middleware;

public class RequestMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILocalStorageService localStorageService;

    public RequestMiddleware(RequestDelegate next, ILocalStorageService localStorageService)
    {
        this.next = next;
        this.localStorageService = localStorageService;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            var ep = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            var authAttr = ep?.Metadata?.GetMetadata<AuthorizeAttribute>();

            if (authAttr != null)
            {
                var tokenExists = localStorageService.Exists("token");
                var tokenIsValid = true;

                if (tokenExists)
                {
                    var token = localStorageService.GetStorageValue<string>("token");
                    JwtSecurityTokenHandler tokenHandler = new();
                    var tokenContent = tokenHandler.ReadJwtToken(token);
                    var expiry = tokenContent.ValidTo;
                    if (expiry < DateTime.UtcNow)
                    {
                        tokenIsValid = false;
                    }
                }

                if (tokenIsValid == false || tokenExists == false)
                {
                    await SignOutAndRedirect(httpContext);
                    return;
                }

                if (authAttr.Roles != null)
                {
                    var userRole = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                    if (authAttr.Roles.Contains(userRole) == false)
                    {
                        var path = $"/home/notauthorized";
                        httpContext.Response.Redirect(path);
                        return;
                    }
                }
            }
            await next(httpContext);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case ApiException apiException:
                await SignOutAndRedirect(context);
                break;
            default:
                var path = $"/Home/Error";
                context.Response.Redirect(path);
                break;
        }
    }

    private static async Task SignOutAndRedirect(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var path = $"/users/login";
        httpContext.Response.Redirect(path);
    } 
}
