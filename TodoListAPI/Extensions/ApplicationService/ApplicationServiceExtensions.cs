using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Domain.Services;
using TodoListAPI.Infra;
using TodoListAPI.Domain.Helpers;
using TodoListAPI.Domain.Interfaces;
using TodoListAPI.Infra.Database.Config;
using TodoListAPI.Infra.Repositories;

namespace TodoListAPI.Extensions.ApplicationService;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<ITodoListService, TodoListService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IAuthService, AuthService>();

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<DataContext, DataContext>();
        services.AddSingleton<ITodoListRepository, TodoListRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();

        return services;
    }
    
    public static IServiceCollection AddProviderServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.Configure<HashingOptions>(
            configuration.GetSection("HashingOptions"));
        
        return services;
    }
}