using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Data
{
    public class InventoryManagementContext : DbContext
    {
        public DbSet<Models.Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=InventoryA;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasKey(p => new { p.ProductID, p.StoreID });


            modelBuilder
                .Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductID, pc.StoreID, pc.CategoryID });

            modelBuilder
                .Entity<ProductAttributeValue>()
                .HasKey(pav => new { pav.ProductID, pav.StoreID, pav.AttributeValueID, pav.AttributeID });

            modelBuilder
                .Entity<OrderDetail>()
                .HasKey(od => new { od.ProductID, od.StoreID, od.OrderID });

            modelBuilder
                .Entity<AttributeValue>()
                .HasKey(av => new { av.AttributeValueID, av.AttributeID });
        }

    }
}
