using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Controllers
{
    public class ProductController : IController<Product>
    {
        private readonly IProvider<Product> _productProvider;
        private readonly ICreator<Product> _productCreator;

        public ProductController(IProvider<Product> productProvider, ICreator<Product> productCreator)
        {
            _productProvider = productProvider;
            _productCreator = productCreator;
        }

        public async Task Create(Product newData)
        {
            await _productCreator.Create(newData);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productProvider.GetAll();
        }
    }
}
