using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Model.Models.User;
using HoneyShop.Model.Pagination;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Users.GetUsersAdmin;

public class GetUsersQueryAdminHandler : IQueryHandler<GetUsersAdminQuery, PaginationListModel<UserItem>>
{
    private readonly IUserService _userService;
    private readonly ILogger<GetUsersQueryAdminHandler> _logger;

    public GetUsersQueryAdminHandler(IUserService userService, ILogger<GetUsersQueryAdminHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task<PaginationListModel<UserItem>> Handle(GetUsersAdminQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение страницы пользователей: страница {Page}, размер страницы {PageSize}", request.Page, request.PageSize);
        var result = await _userService.GetUsersAdmin(request.Page, request.PageSize);
        _logger.LogInformation("Страница пользователей успешно получена: страница {Page}, размер страницы {PageSize}", request.Page, request.PageSize);
        return result;
    }
}