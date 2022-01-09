using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    class ProductProvider : IProvider<Product>
    {
        private readonly ContextFactory _dbContextFactory;

        public ProductProvider(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using InventoryManagementContext context = _dbContextFactory.GetDbContext();
            IEnumerable<ProductDTO> productDTOs = await context.Products.ToListAsync();

            return productDTOs.Select(ModelConverters.ProductDTOToProduct);
        }
    }
}
