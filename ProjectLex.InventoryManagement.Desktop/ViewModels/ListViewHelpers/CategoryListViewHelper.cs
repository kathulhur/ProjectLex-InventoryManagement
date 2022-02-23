using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers
{
    public class CategoryListViewHelper : ListViewHelperBase<CategoryViewModel>
    {
        

        

        public CategoryListViewHelper(ObservableCollection<CategoryViewModel> databaseCollection, ObservableCollection<CategoryViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is CategoryViewModel viewModel)
            {
                return viewModel.CategoryName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
