namespace HoneyShop.Infrastructure.Middlewares
{
    public class XssProtectionMiddleware
    {
        private readonly RequestDelegate _next;

        public XssProtectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-Xss-Protection", "1");
            await _next(context);
        }
    }
}