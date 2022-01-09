using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Modifiers
{
    class CategoryModifier : DatabaseServiceBase, IModifier<Category>
    {
        public CategoryModifier(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task Modify(Category category)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            CategoryDTO categoryDTO = context.Categories.Where(c => c.CategoryID == new Guid(category.CategoryID)).First();
            UpdateCategory(categoryDTO, category);
            await context.SaveChangesAsync();
        }

        private void UpdateCategory(CategoryDTO categoryDTO, Category category)
        {
            if(!(categoryDTO.CategoryName.Equals(category.CategoryName)))
            {
                categoryDTO.CategoryName = category.CategoryName;
            }

            if (!(categoryDTO.CategoryStatus.Equals(category.CategoryStatus)))
            {
                categoryDTO.CategoryStatus = category.CategoryStatus;
            }

        }
    }
}
