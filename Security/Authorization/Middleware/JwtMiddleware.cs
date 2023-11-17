using BackendXComponent.Security.Authorization.Handlers.Interfaces;
using BackendXComponent.Security.Authorization.Settings;
using BackendXComponent.Security.Domain.Services;
using Microsoft.Extensions.Options;

namespace BackendXComponent.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;
    
    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }
    
    
    public async Task Invoke(HttpContext context, ImplUserService userService, IJwtHandler handler)
    {
        var token =
            context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = handler.ValidateToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = await
                userService.GetByIdAsync(userId.Value);
        }
        await _next(context);
    }
}