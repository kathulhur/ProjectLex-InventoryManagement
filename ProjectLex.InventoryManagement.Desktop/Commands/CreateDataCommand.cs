using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    public class CreateDataCommand<TModel> : AsyncCommandBase
    {
        private readonly IDataCollection<TModel> _collection;
        private readonly Func<object, TModel> _createModel;
        private readonly Predicate<object> _canExecute;

        public CreateDataCommand
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

        public override bool CanExecute(object obj)
        {
            return _canExecute(obj) && base.CanExecute(obj);
        }


        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                TModel newModel = _createModel(parameter);
                await _collection.Create(newModel);
                MessageBox.Show("Success!");
            }
            catch
            {
                MessageBox.Show("Error creating category");
            }
        }
    }
}
