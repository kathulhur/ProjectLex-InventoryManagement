using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ProductLocationViewModel
    {
        private readonly ProductLocation _productLocation;

        public ProductLocationViewModel(ProductLocation productLocation)
        {
            _productLocation = productLocation;
        }

        public ProductLocation ProductLocation => _productLocation;
        public string ProductID => _productLocation.ProductID.ToString();
        public string LocationID => _productLocation.LocationID.ToString();
        public string ProductQuantity => _productLocation.ProductQuantity.ToString();

        public ProductViewModel Product
        {
            get
            {
                if (_productLocation.Product != null)
                {
                    return new ProductViewModel(_productLocation.Product);
                }
                return null;
            }
        }

        public LocationViewModel Location
        {
            get
            {
                if (_productLocation.Location != null)
                {
                    return new LocationViewModel(_productLocation.Location);
                }
                return null;
            }
        }



    }
}
