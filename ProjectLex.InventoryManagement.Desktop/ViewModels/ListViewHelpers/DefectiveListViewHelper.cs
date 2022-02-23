using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers
{
    public class LocationListViewHelper : ListViewHelperBase<LocationViewModel>
    {
        

        

        public LocationListViewHelper(ObservableCollection<LocationViewModel> databaseCollection, ObservableCollection<LocationViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is LocationViewModel viewModel)
            {
                return viewModel.LocationName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
