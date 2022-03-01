using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace ProjectLex.InventoryManagement.Database.Data
{
    public class InventoryManagementContext : DbContext
    {

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Defective> Defectives { get; set; }
        public DbSet<ProductLocation> ProductLocations { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("dbconfig.json");

            var configuration = configurationBuilder.Build();
            optionsBuilder.UseSqlServer(configuration["connectionstrings:db"]);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<OrderDetail>()
                .HasKey(od => new { od.ProductID, od.OrderID });

            modelBuilder
                .Entity<ProductLocation>()
                .HasKey(pl => new { pl.ProductID, pl.LocationID });

        }

    }
}
