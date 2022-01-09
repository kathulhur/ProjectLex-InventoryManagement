using ProjectLex.InventoryManagement.Desktop.Collections;
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
    public class RemoveDataCommand<TModel> : AsyncCommandBase
    {
        private readonly IDataCollection<TModel> _collection;
        private readonly Predicate<object> _canExecute;
        public RemoveDataCommand
            (
                IDataCollection<TModel> collection,
                Predicate<object> canExecute
            )
        {
            _collection = collection;
            _canExecute = canExecute;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                await _collection.Remove(parameter);
                MessageBox.Show("Success!");
            }
            catch
            {
                MessageBox.Show("Error creating category");
            }
        }
    }
}
