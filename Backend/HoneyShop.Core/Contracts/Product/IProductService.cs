using HoneyShop.Model.Models.Product;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Core.Contracts.Product;

public interface IProductService
{
    public Task<ProductItem> Create(HonaeyShop.Domain.Model.Product product, List<int> fileIds);
    
    public Task<PaginationListModel<ProductItem>> GetProductPage(int page, int pageSize);
    
    public Task<ProductItem> ChangeStatus(int id, int statusId, int updateUserId);
    
    public Task<ProductItem> Update(UpdateProduct updatedProduct, int updateUserId);
    
    public Task<bool> Delete(int id, int updateUserId);
    
    public Task<ProductItem> GetProductById(int id);
}