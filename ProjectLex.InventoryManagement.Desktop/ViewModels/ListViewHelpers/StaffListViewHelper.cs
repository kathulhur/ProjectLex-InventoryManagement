using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers
{
    public class StaffListViewHelper : ListViewHelperBase<StaffViewModel>
    {
        

        

        public StaffListViewHelper(ObservableCollection<StaffViewModel> databaseCollection, ObservableCollection<StaffViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is StaffViewModel viewModel)
            {
                return viewModel.StaffFullname.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
