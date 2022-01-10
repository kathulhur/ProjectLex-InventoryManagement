using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string StoreID { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public string ProductPrice { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductAvailability { get; set; }
        public StoreDTO Store { get; set; }
        public List<ProductCategoryDTO> ProductCategories { get; set; }
        public List<ProductBrandDTO> ProductBrand { get; set; }

        public Product(ProductDTO productDTO)
        {
            ProductID = productDTO.ProductID.ToString();
            StoreID = productDTO.StoreID.ToString();
            ProductName = productDTO.ProductName;
            ProductSKU = productDTO.ProductSKU;
            ProductPrice = productDTO.ProductPrice;
            ProductQuantity = productDTO.ProductQuantity;
            ProductAvailability = productDTO.ProductAvailability;
            Store = productDTO.Store;
            ProductCategories = productDTO.ProductCategories.ToList();
            ProductBrand = productDTO.ProductBrand.ToList();
        }

    }
}
