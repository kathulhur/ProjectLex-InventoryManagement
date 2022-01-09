using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Removers
{
    public class CategoryRemover : DatabaseServiceBase, IRemover<Category>
    {
        public CategoryRemover(ContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public async Task Remove(Category category)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            CategoryDTO categoryDTO = context.Categories
                .Where(c => c.CategoryID == new Guid(category.CategoryID)).First();

            context.Categories.Remove(categoryDTO);
            await context.SaveChangesAsync();
        }
    }
}
