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
        private readonly Predicate<object> _canExecute;
        public ModifyDataCommand
            (
                IDataCollection<TModel> collection,
                Predicate<object> canExecute
            )
        {
            _collection = collection;
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
                await _collection.Modify(parameter);
                MessageBox.Show("Success!");
            }
            catch
            {
                MessageBox.Show("Error creating category");
            }
        }
    }
}
