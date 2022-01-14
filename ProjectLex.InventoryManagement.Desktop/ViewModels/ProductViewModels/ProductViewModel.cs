using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly Product _product;
        public Product Product => _product;
        public string ProductID => _product.ProductID.ToString();
        public string StoreID => _product.StoreID.ToString();
        public string SupplierID => _product.SupplierID.ToString();
        public SupplierViewModel Supplier => new SupplierViewModel(_product.Supplier);
        public StoreViewModel Store => new StoreViewModel(_product.Store);
        public string ProductName => _product.ProductName;
        public string ProductSKU => _product.ProductSKU;
        public string ProductPrice => _product.ProductPrice.ToString();
        public string ProductQuantity => _product.ProductQuantity.ToString();
        public string ProductAvailability => _product.ProductAvailability;
        public List<CategoryViewModel> Categories => _product.ProductCategories.Select(pc => new CategoryViewModel(pc.Category)).ToList();

        //public List<ProductCategoryViewModel> ProductCategories { get; }


        public ProductViewModel(Product product)
        {
            _product = product;
        }

    }
}
