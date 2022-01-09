using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Creators
{
    class ProductCreator : ICreator<Product>
    {
        private readonly ContextFactory _dbContextFactory;

        public ProductCreator(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task Create(Product product)
        {
            using InventoryManagementContext context = _dbContextFactory.GetDbContext();
            ProductDTO newProductDTO = ModelConverters.ProductToProductDTO(product);
            context.Products.Add(newProductDTO);
            await context.SaveChangesAsync();

        }   
    }
}
