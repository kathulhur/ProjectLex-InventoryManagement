using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class ProductDTO
    {
        public Guid ProductID { get; set; }

        public Guid StoreID { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public string ProductPrice { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductAvailability { get; set; }

        public StoreDTO Store { get; set; }
        public ICollection<ProductCategoryDTO> ProductCategories { get; set; }
        public ICollection<ProductAttributeDTO> ProductAttributes { get; set; }
        public ICollection<ProductBrandDTO> ProductBrand { get; set; }
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }

    }
}
