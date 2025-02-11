using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Core.Contracts.Cart;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.Cart;
using HoneyShop.Model.Pagination;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Carts.GetCartItemsPage;

public class GetCartItemsPageQueryHandler : IQueryHandler<GetCartItemsPageQuery, PaginationListModel<CartItem>>
{
    private readonly IHttpContextService _httpContextService;
    private readonly ICartService _cartService;
    private readonly ILogger<GetCartItemsPageQueryHandler> _logger;

    public GetCartItemsPageQueryHandler(IHttpContextService httpContextService, ICartService cartService, ILogger<GetCartItemsPageQueryHandler> logger)
    {
        _httpContextService = httpContextService;
        _cartService = cartService;
        _logger = logger;
    }

    public async Task<PaginationListModel<CartItem>> Handle(GetCartItemsPageQuery request, CancellationToken cancellationToken)
    {
        var idUser = _httpContextService.GetCurrentUserId();
        if (idUser == default)
        {
            _logger.LogError("Пользователь не авторизован");
            throw new HoneyException("Пользователь не авторизован");
        }

        _logger.LogInformation("Получение элементов корзины для пользователя {UserId}", idUser);
        var result = await _cartService.GetCartItems(idUser.Value, request.PageSize, request.PageSize);
        _logger.LogInformation("Элементы корзины успешно получены для пользователя {UserId}", idUser);

        return result;
    }
}