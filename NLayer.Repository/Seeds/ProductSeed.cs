using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product() { Id = 1, CategoryId = 1, Name = "Kurşun Kalem", Price = 10, Stock = 20, CreatedDate = DateTime.Now },
                new Product() { Id = 2, CategoryId = 1, Name = "Uçlu Kalem", Price = 50, Stock = 30, CreatedDate = DateTime.Now },
                new Product() { Id = 3, CategoryId = 1, Name = "Pilot Kalem", Price = 70, Stock = 15, CreatedDate = DateTime.Now },


                new Product() { Id = 4, CategoryId = 2, Name = "Oscar Wilde", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product() { Id = 5, CategoryId = 2, Name = "Cesur Yeni Dünya", Price = 200, Stock = 30, CreatedDate = DateTime.Now },
                new Product() { Id = 6, CategoryId = 2, Name = "Kızıl Veba", Price = 350, Stock = 15, CreatedDate = DateTime.Now },
                new Product() { Id = 7, CategoryId = 2, Name = "Simyacı", Price = 300, Stock = 15, CreatedDate = DateTime.Now },


                new Product() { Id = 8, CategoryId = 3, Name = "Kareli Defter", Price = 50, Stock = 50, CreatedDate = DateTime.Now },
                new Product() { Id = 9, CategoryId = 3, Name = "Çizgili Defter" , Price = 40, Stock = 40, CreatedDate = DateTime.Now },
                new Product() { Id = 10, CategoryId =3, Name = "Çizgisiz Defter", Price = 60, Stock = 30, CreatedDate = DateTime.Now }
                );
        }
    }
}
