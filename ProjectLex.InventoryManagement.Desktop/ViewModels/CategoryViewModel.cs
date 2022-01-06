using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private readonly Category _category;
        public string CategoryId => _category.CategoryId;
        public string CategoryName => _category.CategoryName;
        public string Description => _category.Description;

        public CategoryViewModel(Category category)
        {
            _category = category;
        }
    }
}
