using Microsoft.EntityFrameworkCore;
using TodoListAPI.Infra.Database.Models;

namespace TodoListAPI.Infra.Database.Config
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().ToTable("Users");
            modelBuilder.Entity<TodoData>().ToTable("Todos");
        }

        public DbSet<UserData> Users { get; set; }
        public DbSet<TodoData> TodoList { get; set; }
    }
}
