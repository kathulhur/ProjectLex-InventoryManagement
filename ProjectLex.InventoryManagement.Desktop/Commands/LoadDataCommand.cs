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
    class LoadDataCommand<TModel> : AsyncCommandBase
    {
        private readonly ILoadable<TModel> _collection;
        private readonly Action _loadData;
        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                await _collection.Load();
                _loadData();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public LoadDataCommand(ILoadable<TModel> collection, Action execute)
        {
            _collection = collection;
            _loadData = execute;
        }

        
    }
}
