using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Cart;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.Cart;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Carts.CreateCart;

public class CreateCartCommandHandler : ICommandHandler<CreateCartCommand, CartItem>
{
    private readonly ICartService _cartService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<CreateCartCommandHandler> _logger;

    public CreateCartCommandHandler(ICartService cartService, IHttpContextService httpContextService, ILogger<CreateCartCommandHandler> logger)
    {
        _cartService = cartService;
        _httpContextService = httpContextService;
        _logger = logger;
    }

    public async Task<CartItem> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var idUser = _httpContextService.GetCurrentUserId();
        if (idUser == null)
        {
            _logger.LogError("Пользователь не авторизован");
            throw new HoneyException("Пользователь не авторизован");
        }

        _logger.LogInformation("Создание корзины для пользователя {UserId}", idUser);
        var result = await _cartService.Create(request.Cart, idUser.Value);
        _logger.LogInformation("Корзина создана успешно для пользователя {UserId}", idUser);

        return result;
    }
}