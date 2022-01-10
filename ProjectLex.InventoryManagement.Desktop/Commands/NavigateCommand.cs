using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    class NavigateCommand : CommandBase
    {
        public Action<object> _navigateToView;
        public NavigateCommand(Action<object> navigateToView)
        {
            _navigateToView = navigateToView;
        }

        public override void Execute(object parameter)
        {
            _navigateToView(parameter);
        }
    }
}
