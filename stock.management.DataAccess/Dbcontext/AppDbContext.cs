using Microsoft.EntityFrameworkCore;
using stock.management.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.management.DataAccess
{
   public  class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.ToTable("ProductDetail");
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId)
                      .ValueGeneratedNever(); // Ensure ProductId is not auto-incremented
                entity.Property(e => e.ProductName)
                      .IsRequired();
                entity.Property(e => e.Quantity)
                      .IsRequired();
                entity.Property(e => e.Priceperunit)
                     .IsRequired();
            });
        }
    }
}
