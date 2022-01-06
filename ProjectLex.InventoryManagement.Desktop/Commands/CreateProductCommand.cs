using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    public class CreateProductCommand : AsyncCommandBase
    {
        private readonly ProductCollection _productCollection;

        private readonly CreateProductViewModel _createProductViewModel;

        private readonly NavigationService<ProductListViewModel> _navigationService;


        public CreateProductCommand(
            CreateProductViewModel createProductViewModel,
            ProductCollection productCollection, 
            NavigationService<ProductListViewModel> navigationService)
        {
            _createProductViewModel = createProductViewModel;
            _navigationService = navigationService;
            _productCollection = productCollection;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            Product newProduct = ModelConverters.CreateProductViewModelToProduct(_createProductViewModel);

            try
            {
                await _productCollection.Create(newProduct);
                MessageBox.Show("Success!");
                _navigationService.Navigate();
            }
            catch
            {
                MessageBox.Show("Error creating category");

            }
        }

    }
}
