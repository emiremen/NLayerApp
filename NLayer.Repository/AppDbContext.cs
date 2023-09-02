using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature() { Id = 1, Color = "Kırmızı", Height = 20, Width = 30, ProductId = 1 },
                new ProductFeature() { Id = 2, Color = "Mavi", Height = 21, Width = 35, ProductId = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
