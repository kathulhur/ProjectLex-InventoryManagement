using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    public class CategoryProvider : DatabaseServiceBase, IProvider<Category>
    {

        public CategoryProvider(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            IEnumerable<CategoryDTO> categoryDTOs = await context.Categories.ToListAsync();

            return categoryDTOs.Select(c => new Category(c));
        }


        public async Task Create(Category category)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            CategoryDTO categoryDTO = ModelConverters.CategoryToCategoryDTO(category);
            context.Categories.Add(categoryDTO);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException dbE)
            {
                Debug.WriteLine("CategoryProvider.Create : ");
                Debug.WriteLine(dbE.InnerException);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
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
            if (!(categoryDTO.CategoryName.Equals(category.CategoryName)))
            {
                categoryDTO.CategoryName = category.CategoryName;
            }

            if (!(categoryDTO.CategoryStatus.Equals(category.CategoryStatus)))
            {
                categoryDTO.CategoryStatus = category.CategoryStatus;
            }

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
