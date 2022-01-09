using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using ProjectLex.InventoryManagement.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    class ModifyDataNavigateCommand : CommandBase
    {
        private readonly Action<object> _navigateToModifyDataView;

        public ModifyDataNavigateCommand(Action<object> navigateToModifyDataView)
        {
            _navigateToModifyDataView = navigateToModifyDataView;
        }

        public override void Execute(object parameter)
        {
            _navigateToModifyDataView(parameter);
        }
    }
}
