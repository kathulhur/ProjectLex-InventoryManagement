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
        private readonly Func<object, TModel> _createModel;
        private readonly Predicate<object> _canExecute;
        public RemoveDataCommand
            (
                IDataCollection<TModel> collection,
                Func<object, TModel> createModel,
                Predicate<object> canExecute
            )
        {
            _collection = collection;
            _createModel = createModel;
            _canExecute = canExecute;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                TModel removedModel = _createModel(parameter);
                await _collection.Remove(removedModel);
                MessageBox.Show("Success!");
            }
            catch
            {
                MessageBox.Show("Error Removing Role");
            }
        }
    }
}
