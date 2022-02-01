using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }
        public Guid SupplierID { get; set; }
        public Guid CategoryID { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public string ProductUnit { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductAvailability { get; set; }
        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductLocation> ProductLocations { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
