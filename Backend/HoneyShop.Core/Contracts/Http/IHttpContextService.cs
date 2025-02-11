namespace HoneyShop.Core.Contracts.Http;

public interface IHttpContextService
{
    int? GetCurrentUserId();
    string? GetCurrentUserRole();
}