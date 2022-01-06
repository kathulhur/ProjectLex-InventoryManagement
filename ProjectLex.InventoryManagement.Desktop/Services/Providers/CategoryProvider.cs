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
    public class CategoryProvider : IProvider<Category>
    {
        private readonly ContextFactory _dbContextFactory;

        public CategoryProvider(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using InventoryContext context = _dbContextFactory.GetDbContext();
            IEnumerable<CategoryDTO> categoryDTOs = await context.Categories.ToListAsync();

            return categoryDTOs.Select(ModelConverters.CategoryDTOToCategory);
        }

    }
}
