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
        public string SupplierID => _product.SupplierID.ToString();
        public string CategoryID => _product.CategoryID.ToString();
        public string ProductName => _product.ProductName;
        public string ProductSKU => _product.ProductSKU;
        public string ProductUnit => _product.ProductUnit;
        public string ProductPrice => _product.ProductPrice.ToString();
        public string ProductQuantity => _product.ProductQuantity.ToString();
        public string ProductAvailability => _product.ProductAvailability;

        public SupplierViewModel Supplier
        {
            get
            {
                if (_product.Supplier != null)
                {
                    return new SupplierViewModel(_product.Supplier);
                }
                return null;
            }
        }

        public CategoryViewModel Category
        {
            get
            {
                if (_product.Category != null)
                {
                    return new CategoryViewModel(_product.Category);
                }
                return null;
            }
        }


        public ProductViewModel(Product product)
        {
            _product = product;
        }

    }
}
