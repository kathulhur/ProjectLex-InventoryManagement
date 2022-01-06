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
    public class CreateSupplierCommand : AsyncCommandBase
    {
        private readonly SupplierCollection _supplierCollection;

        private readonly CreateSupplierViewModel _createSupplierViewModel;

        private readonly NavigationService<SupplierListViewModel> _navigationService;


        public CreateSupplierCommand(
            CreateSupplierViewModel createSupplierViewModel,
            SupplierCollection supplierCollection, 
            NavigationService<SupplierListViewModel> navigationService)
        {
            _createSupplierViewModel = createSupplierViewModel;
            _navigationService = navigationService;
            _supplierCollection = supplierCollection;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            Supplier newData = ModelConverters.CreateSupplierViewModelToSupplier(_createSupplierViewModel);

            try
            {
                await _supplierCollection.Create(newData);
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
