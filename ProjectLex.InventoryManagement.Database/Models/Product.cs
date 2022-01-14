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
        [Key]
        public Guid StoreID { get; set; }
        public Guid SupplierID { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductAvailability { get; set; }
        public Store Store { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
