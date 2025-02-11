using HoneyShop.Model.Models.Cart;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Core.Contracts.Cart;

public interface ICartService
{
    public Task<CartItem> Create(CreateCart cart, int userId);
    
    public Task<PaginationListModel<CartItem>> GetCartItems(int userId, int page, int pageSize);
    
    public Task<bool> DeleteCartItem(int Id, int updateUserId);


}