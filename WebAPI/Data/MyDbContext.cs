using System;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }

        public DbSet<Categories> Categories { get; set; } // DbSet ánh xạ bảng "Category"

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set relationship 1 Category have many Product
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
