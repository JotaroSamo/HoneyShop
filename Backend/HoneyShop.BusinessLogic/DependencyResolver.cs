using HoneyShop.BusinessLogic.Auth;
using HoneyShop.BusinessLogic.Cart;
using HoneyShop.BusinessLogic.File;
using HoneyShop.BusinessLogic.Order;
using HoneyShop.BusinessLogic.Product;
using HoneyShop.BusinessLogic.Security;
using HoneyShop.BusinessLogic.User;
using HoneyShop.Core.Contracts.Auth;
using HoneyShop.Core.Contracts.Cart;
using HoneyShop.Core.Contracts.File;
using HoneyShop.Core.Contracts.Order;
using HoneyShop.Core.Contracts.Product;
using HoneyShop.Core.Contracts.Security;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace HoneyShop.BusinessLogic;

public static class DependencyResolver
{
    public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddAutoMapper(typeof(UserProfile).Assembly);
        return services;
    }

}