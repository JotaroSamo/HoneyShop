using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Models.User;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Application.Users.GetUsersAdmin;

public class GetUsersAdminQuery : IQuery<PaginationListModel<UserItem>>
{
    public int Page { get; }
    public int PageSize { get; }

    public GetUsersAdminQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}