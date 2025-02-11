using System.Security.Claims;
using System.Security.Principal;
using HoneyShop.Core.Contracts.Http;

namespace HoneyShop.Infrastructure.Context;

public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    private IIdentity UserIdentity => _contextAccessor.HttpContext?.User.Identity;

    public int? GetCurrentUserId()
    {
        var nameIdentifierClaim = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(nameIdentifierClaim, out var userId) ? userId : (int?)null;
    }

    public string GetCurrentUserRole()
    {
        var roleIdentifierClaim = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
        return roleIdentifierClaim;
    }
}
