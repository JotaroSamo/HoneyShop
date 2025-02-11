using HoneyShop.Core.Excpetions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HoneyShop.Infrastructure.Filters;

public class HttpResponseExceptionFilter : IExceptionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is HoneyException exception)
        {
            var objResult = exception.Object ?? new { message = exception.Message };
            context.Result = new ObjectResult(objResult)
            {
                StatusCode = exception.StatusCode
            };
            context.ExceptionHandled = true;
        }
    }
}