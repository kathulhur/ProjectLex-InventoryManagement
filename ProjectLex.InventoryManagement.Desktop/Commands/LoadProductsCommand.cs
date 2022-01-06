using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    class LoadProductsCommand : AsyncCommandBase
    {
        private readonly IUpdatable<Product> _viewModel;
        private readonly ILoadable<Product> _productCollection;

        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                await _productCollection.Load();
                _viewModel.Update(_productCollection.DataList);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public LoadProductsCommand(IUpdatable<Product> viewModel, ILoadable<Product> productCollection)
        {
            _viewModel = viewModel;
            _productCollection = productCollection;
        }
    }
}
