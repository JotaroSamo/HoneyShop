using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Application.Files.DeleteFile;
using HoneyShop.Core.Contracts.Cart;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Excpetions;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Carts.DeleteCart;

public class DeleteCartCommandHandler : ICommandHandler<DeleteCartCommand, bool>
{
    private readonly IHttpContextService _httpContextService;
    private readonly ICartService _cartService;
    private readonly ILogger<DeleteCartCommandHandler> _logger;

    public DeleteCartCommandHandler(IHttpContextService httpContextService, ICartService cartService,
        ILogger<DeleteCartCommandHandler> logger)
    {
        _httpContextService = httpContextService;
        _cartService = cartService;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var updateUserId = _httpContextService.GetCurrentUserId();
        if (updateUserId == null)
        {
            _logger.LogError("Нет прав на удаление корзины");
            throw new HoneyException("Нет прав", 403);
        }

        _logger.LogInformation("Удаление корзины для пользователя {UserId}", updateUserId);
        var result = await _cartService.DeleteCartItem(request.Id, updateUserId.Value);
        _logger.LogInformation("Корзина успешно удалена для пользователя {UserId}", updateUserId);

        return result;
    }
}