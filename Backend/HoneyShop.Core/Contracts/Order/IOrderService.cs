using HoneyShop.Model.Models.Order;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Core.Contracts.Order;

public interface IOrderService
{
    public Task<OrderItem> CreateOrder(HonaeyShop.Domain.Model.Order order, int userId);
    
    public Task<PaginationListModel<OrderItem>> GetOrdersByUser(int userId, int page, int pageSize);

    public Task<PaginationListModel<OrderItem>> GetOrdersByProduct(int productId, int page, int pageSize);
    
    public Task<PaginationListModel<OrderItem>> GetOrders(int page, int pageSize);
    
    public Task<OrderItem> ChangeStatus(int orderId, int statusId, int userId);
    
    public Task<bool> DeleteOrder(int orderId);


}