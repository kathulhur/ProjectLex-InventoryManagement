using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Data
{
    public class InventoryContext : DbContext
    {
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<OrderDetailDTO> OrderDetails { get; set; }
        public DbSet<PaymentDTO> Payments { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<StaffDTO> Staffs { get; set; }
        public DbSet<SupplierDTO> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=InventoryManagement;Trusted_Connection=True;");
        }
    }
}
