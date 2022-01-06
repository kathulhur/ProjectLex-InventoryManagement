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
    public class CategoryController : IController<Category>
    {
        private readonly IProvider<Category> _categoryProvider;
        private readonly ICreator<Category> _categoryCreator;
        public CategoryController(
            IProvider<Category> categoryProvider,
            ICreator<Category> categoryCreator)
        {
            _categoryProvider = categoryProvider;
            _categoryCreator = categoryCreator;
        }

        public async Task Create(Category newData)
        {
            await _categoryCreator.Create(newData);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryProvider.GetAll();
        }
    }
}
