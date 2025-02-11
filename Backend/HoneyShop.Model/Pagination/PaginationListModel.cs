namespace HoneyShop.Model.Pagination;

public class PaginationListModel<T>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public long Total { get; set; }
    public List<T> Models { get; set; }
}