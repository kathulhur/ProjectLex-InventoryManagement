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
    class LoadSuppliersCommand : AsyncCommandBase
    {
        private readonly IUpdatable<Supplier> _viewModel;
        private readonly ILoadable<Supplier> _supplierCollection;

        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                await _supplierCollection.Load();
                _viewModel.Update(_supplierCollection.DataList);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public LoadSuppliersCommand(IUpdatable<Supplier> viewModel, ILoadable<Supplier> supplierCollection)
        {
            _viewModel = viewModel;
            _supplierCollection = supplierCollection;
        }

        
    }
}
