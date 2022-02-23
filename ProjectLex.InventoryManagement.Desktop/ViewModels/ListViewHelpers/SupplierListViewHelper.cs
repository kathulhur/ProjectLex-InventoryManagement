using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers
{
    public class SupplierListViewHelper : ListViewHelperBase<SupplierViewModel>
    {
        

        

        public SupplierListViewHelper(ObservableCollection<SupplierViewModel> databaseCollection, ObservableCollection<SupplierViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is SupplierViewModel viewModel)
            {
                return viewModel.SupplierName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
