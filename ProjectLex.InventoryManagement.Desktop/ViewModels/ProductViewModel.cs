using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ProductViewModel
    {
        private readonly Product _product;
        public string Id
        {
            get
            {
                return Convert.ToString(_product.ProductId);
            }
        }

        public string Name => _product.ProductName;
        public string Address => _product.ProductDescription;
        public string Phone => _product.ProductUnit;
        public string Price => Convert.ToString(_product.ProductPrice);
        public string Quantity => Convert.ToString(_product.ProductQuantity);
        public string Status => Convert.ToString(_product.ProductStatus);
        public string OtherDetails => _product.OtherDetails;
        public string SupplierId => Convert.ToString(_product.SupplierId);
        public string CategoryId => Convert.ToString(_product.CategoryId);

        public ProductViewModel(Product product)
        {
            _product = product;
        }
    }
}
