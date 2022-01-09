using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Data
{
    public class InventoryManagementContext : DbContext
    {
        public DbSet<AttributeDTO> Attributes { get; set; }
        public DbSet<AttributeValueDTO> AttributeValues { get; set; }
        public DbSet<BrandDTO> Brands { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<CompanyDTO> Companies { get; set; }
        public DbSet<OrderDetailDTO> OrderDetails { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<ProductBrandDTO> ProductBrands { get; set; }
        public DbSet<ProductAttributeDTO> ProductAttributes { get; set; }
        public DbSet<ProductCategoryDTO> ProductCategories { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<StoreDTO> Stores { get; set; }
        public DbSet<UserDTO> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=InventoryA;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProductDTO>()
                .HasKey(p => new { p.ProductID, p.StoreID });

            modelBuilder
                .Entity<ProductBrandDTO>()
                .HasKey(pb => new { pb.ProductID, pb.StoreID, pb.BrandID });

            modelBuilder
                .Entity<ProductCategoryDTO>()
                .HasKey(pc => new { pc.ProductID, pc.StoreID, pc.CategoryID });

            modelBuilder
                .Entity<ProductAttributeDTO>()
                .HasKey(pa => new { pa.ProductID, pa.StoreID, pa.AttributeID });

            modelBuilder
                .Entity<OrderDetailDTO>()
                .HasKey(pa => new { pa.ProductID, pa.StoreID, pa.OrderID });
        }

    }
}
