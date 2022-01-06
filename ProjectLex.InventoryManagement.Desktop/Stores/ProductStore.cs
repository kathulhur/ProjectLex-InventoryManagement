using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Stores
{
    class ProductStore
    {
        private readonly ProductController _productController;

        private readonly List<Product> _products;

        public IEnumerable<Product> Products;

        public ProductStore()
        {
        }
    }
}
