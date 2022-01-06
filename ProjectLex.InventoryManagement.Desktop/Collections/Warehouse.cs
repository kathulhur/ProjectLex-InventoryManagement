using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectLex.InventoryManagement.Desktop.Models;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.Collections
{
    public class Warehouse
    {
        public string Name { get; }
        private readonly CategoryCollection _categoryCollection;

        private readonly List<Category> _categories;
        public IEnumerable<Category> Categories => _categories;

        public Warehouse(string warehouseName, CategoryCollection categories)
        {
            Name = warehouseName;
            _categoryCollection = categories;
            _categories = new List<Category>();
        }

    }
}
