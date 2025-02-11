using HoneyShop.Infrastructure.Middlewares;

namespace HoneyShop.Infrastructure.Manager;

public static class MiddlewareManager
{
    public static IApplicationBuilder UseDenyFrameMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DenyFrameMiddleware>();
    }

    public static IApplicationBuilder UseNoShiftContentMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<NoShiftContentMiddleware>();
    }

    public static IApplicationBuilder UseXssProtectionMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<XssProtectionMiddleware>();
    }
}