using TodoListAPI.Application.Middleware;
using TodoListAPI.Extensions.ApplicationService;
using TodoListAPI.Extensions.Authentication;
using TodoListAPI.Extensions.Swagger;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var configuration = builder.Configuration;

builder.Services
    .AddEndpointsApiExplorer()
    .AddApplicationServices(configuration)
    .AddRepositories()
    .AddValidators()
    .AddProviderServices(configuration)
    .AddAuthenticationJwt(configuration)
    .AddSwagger()
    .AddControllers();

var app = builder.Build();

app
    .UseMiddleware<ExceptionHandlingMiddleware>()
    .UseSwagger()
    .UseSwaggerUI()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();

