using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers
{
    public class LogListViewHelper : ListViewHelperBase<LogViewModel>
    {
        

        

        public LogListViewHelper(ObservableCollection<LogViewModel> databaseCollection, ObservableCollection<LogViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is LogViewModel viewModel)
            {
                return viewModel.StaffName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase) ||
                    viewModel.LogCategory.Contains(Filter, StringComparison.InvariantCultureIgnoreCase) ||
                    viewModel.ActionType.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
