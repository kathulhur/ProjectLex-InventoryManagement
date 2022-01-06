using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
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
        private readonly CategoryCollection _collection;

        private readonly CreateCategoryViewModel _createCategoryViewModel;

        private readonly NavigationService<CategoryListViewModel> _dataListNavigationService;


        public CreateCategoryCommand(CreateCategoryViewModel createDataViewModel, CategoryCollection collection, NavigationService<CategoryListViewModel> modelListViewNavigationService)
        {
            _createCategoryViewModel = createDataViewModel;
            _dataListNavigationService = modelListViewNavigationService;
            _collection = collection;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            Category newData = CreateNewModel(_createCategoryViewModel);

            try
            {
                await _collection.Create(newData);
                MessageBox.Show("Success!");
                _dataListNavigationService.Navigate();
            }
            catch
            {
                MessageBox.Show("Error creating category");

            }
        }


        public Category CreateNewModel(CreateCategoryViewModel newViewModel)
        {
            return new Category(
                newViewModel.CategoryId,
                newViewModel.CategoryName,
                newViewModel.Description
                );
        }
    }
}
