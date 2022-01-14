using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ProductCategoryViewModel : ViewModelBase
    {
        public string ProductID { get; }
        public string StoreID { get; }
        public string CategoryID { get; }

        public ProductCategoryViewModel(ProductCategory productCategory)
        {
            ProductID = productCategory.ProductID;
            StoreID = productCategory.StoreID;
            CategoryID = productCategory.CategoryID;
        }
    }
}
