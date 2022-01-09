using ProjectLex.InventoryManagement.Desktop.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    public class ModifyDataCommand<TModel> : AsyncCommandBase
    {
        private readonly IDataCollection<TModel> _collection;
        private readonly Func<object, TModel> _createModel;
        private readonly Predicate<object> _canExecute;
        public ModifyDataCommand
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

        public override bool CanExecute(object parameter)
        {
            return _canExecute(parameter) && base.CanExecute(parameter);
        }


        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                TModel modifiedModel = _createModel(parameter);
                await _collection.Modify(modifiedModel);
                MessageBox.Show("Success!");
            }
            catch
            {
                MessageBox.Show("Error creating category");
            }
        }
    }
}
