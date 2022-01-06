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
    class LoadCategoriesCommand : AsyncCommandBase
    {
        private readonly IUpdatable<Category> _viewModel;
        private readonly ILoadable<Category> _categoryCollection;

        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                await _categoryCollection.Load();
                _viewModel.Update(_categoryCollection.DataList);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public LoadCategoriesCommand(IUpdatable<Category> viewModel, ILoadable<Category> categoryCollection)
        {
            _viewModel = viewModel;
            _categoryCollection = categoryCollection;
        }

        
    }
}
