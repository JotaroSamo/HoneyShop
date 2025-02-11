using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.User;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Profile.GetProfile;

public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, UserItem>
{
    private readonly IHttpContextService _httpContextService;
    private readonly IUserService _userService;
    private readonly ILogger<GetProfileQueryHandler> _logger;

    public GetProfileQueryHandler(IHttpContextService httpContextService, 
        IUserService userService,
        ILogger<GetProfileQueryHandler> logger)
    {
        _httpContextService = httpContextService;
        _userService = userService;
        _logger = logger;
    }
    public async Task<UserItem> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _httpContextService.GetCurrentUserId();
        if (currentUserId == null)
        {
            _logger.LogError("Недостаточно прав для получения данных ");
            throw new HoneyException("Недостаточно прав", 403);
        }

        return await _userService.GetUserById(currentUserId.Value);
    }
}