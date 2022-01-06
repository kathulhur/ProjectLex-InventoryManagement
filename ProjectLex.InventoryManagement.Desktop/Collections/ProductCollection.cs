using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Collections
{
    public class ProductCollection : DataCollectionBase<Product>
    {
        public ProductCollection(IController<Product> productController) : base(productController)
        {

        }
    }
}
