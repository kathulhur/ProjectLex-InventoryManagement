using ProjectLex.InventoryManagement.Database.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class ProductCategory
    {
        public string ProductID { get; set; }
        public string StoreID { get; set; }
        public string CategoryID { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }

        public ProductCategory(ProductCategoryDTO productCategoryDTO)
        {
            ProductID = productCategoryDTO.ProductID.ToString();
            StoreID = productCategoryDTO.StoreID.ToString();
            CategoryID = productCategoryDTO.CategoryID.ToString();
            Product = new Product(productCategoryDTO.Product);
            Category = new Category(productCategoryDTO.Category);
        }

        public ProductCategory(CreateProductCategoryViewModel createProductCategoryViewModel)
        {
            ProductID = createProductCategoryViewModel.ProductID;
            StoreID = createProductCategoryViewModel.StoreID;
            CategoryID = createProductCategoryViewModel.CategoryID;
        }
        public ProductCategory(ModifyProductCategoryViewModel modifyProductCategoryViewModel)
        {
            ProductCategoryID = modifyProductCategoryViewModel.ProductCategoryID;
            ProductCategoryName = modifyProductCategoryViewModel.ProductCategoryName;
            ProductCategoryStatus = modifyProductCategoryViewModel.ProductCategoryStatus;
        }

        public ProductCategory(ProductCategoryViewModel productCategoryViewModel)
        {
            ProductCategoryID = productCategoryViewModel.ProductCategoryID;
            ProductCategoryName = productCategoryViewModel.ProductCategoryName;
            ProductCategoryStatus = productCategoryViewModel.ProductCategoryStatus;
        }
    }
}
