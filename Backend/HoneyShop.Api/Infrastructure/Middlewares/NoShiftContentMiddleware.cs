namespace HoneyShop.Infrastructure.Middlewares
{
    public class NoShiftContentMiddleware
    {
        private readonly RequestDelegate _next;

        public NoShiftContentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            await _next(context);
        }
    }
}