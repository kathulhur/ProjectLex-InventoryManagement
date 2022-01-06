using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    public class CreateCategoryCommand : AsyncCommandBase
    {
        private readonly CategoryCollection _categoryCollection;
        private readonly CreateCategoryViewModel _createCategoryViewModel;
        private readonly NavigationService<CategoryListViewModel> _categoryListViewNavigationService;

        public CreateCategoryCommand(CreateCategoryViewModel createCategoryViewModel, CategoryCollection categoryCollection, NavigationService<CategoryListViewModel> categoryListViewNavigationService)
        {
            _createCategoryViewModel = createCategoryViewModel;
            _categoryListViewNavigationService = categoryListViewNavigationService;
            _categoryCollection = categoryCollection;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            Category newCategory = new Category(
                _createCategoryViewModel.CategoryId,
                _createCategoryViewModel.CategoryName,
                _createCategoryViewModel.Description
                );

            try
            {
                await _categoryCollection.CreateCategory(newCategory);
                MessageBox.Show("Success!");
                _categoryListViewNavigationService.Navigate();
            } catch
            {
                MessageBox.Show("Error creating category");

            }
        }
    }
}
