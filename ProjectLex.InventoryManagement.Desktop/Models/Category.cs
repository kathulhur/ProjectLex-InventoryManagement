using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Category
    {
        public string CategoryID { get; }
        public string CategoryName { get; }
        public string CategoryStatus { get; }

        public Category(CategoryDTO categoryDTO)
        {
            CategoryID = categoryDTO.CategoryID.ToString();
            CategoryName = categoryDTO.CategoryName;
            CategoryStatus = categoryDTO.CategoryStatus;
        }

        public Category(CreateCategoryViewModel createCategoryViewModel)
        {
            CategoryID = Guid.NewGuid().ToString();
            CategoryName = createCategoryViewModel.CategoryName;
            CategoryStatus = createCategoryViewModel.CategoryStatus;
        }

        public Category(ModifyCategoryViewModel modifyCategoryViewModel)
        {
            CategoryID = modifyCategoryViewModel.CategoryId;
            CategoryName = modifyCategoryViewModel.CategoryName;
            CategoryStatus = modifyCategoryViewModel.CategoryStatus;
        }

        public Category(CategoryViewModel categoryViewModel)
        {
            CategoryID = categoryViewModel.CategoryID;
            CategoryName = categoryViewModel.CategoryName;
            CategoryStatus = categoryViewModel.CategoryStatus;
        }


    }
}
