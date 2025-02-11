using HoneyShop.Core.Excpetions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало обработки запроса {RequestName} с данными: {@Request}", typeof(TRequest).Name, request);
        try
        {
            var response = await next();
            _logger.LogInformation("Запрос {RequestName} успешно обработан", typeof(TRequest).Name);
            return response;
        }
        catch (HoneyException exception)
        {
            _logger.LogError(exception, "Ошибка при обработке запроса {RequestName}", typeof(TRequest).Name);
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Необработанная ошибка при обработке запроса {RequestName}", typeof(TRequest).Name);
            throw;
        }
    }
}
