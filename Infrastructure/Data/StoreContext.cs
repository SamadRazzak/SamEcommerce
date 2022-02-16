using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // modelBuilder.Entity<Product>().HasData(new Product
            // {
            //     Id = 1,
            //     Name = "Product One",            
            // });

            //  modelBuilder.Entity<Product>().HasData(new Product
            // {
            //     Id = 2,
            //     Name = "Product Two",            
            // });

            //  modelBuilder.Entity<Product>().HasData(new Product
            // {
            //     Id = 3,
            //     Name = "Product Three",            
            // });            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
    }
}