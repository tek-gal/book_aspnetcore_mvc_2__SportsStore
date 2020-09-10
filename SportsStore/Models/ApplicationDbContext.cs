using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models {

    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }

        // dotnet ef migrations add Orders - add new model
        // dotnet ef database drop --force - delete database
        // dotnet ef database update - create database and apply migrations
        public DbSet<Order> Orders { get; set; }
    }

}