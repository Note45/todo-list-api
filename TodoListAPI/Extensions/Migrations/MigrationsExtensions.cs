using Microsoft.EntityFrameworkCore;
using TodoListAPI.Infra.Database.Config;

namespace TodoListAPI.Extensions.Migrations;

public static class MigrationsExtensions
{
    public static IApplicationBuilder UseStartupDbMigrations(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            db.Database.Migrate();
            
            Console.WriteLine("Application data base created!");
        }

        return builder;
    }
}