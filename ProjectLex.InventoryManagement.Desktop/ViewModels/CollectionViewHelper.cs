using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CollectionViewHelper : PaginationHelper<CategoryViewModel>
    {

        private string _filter = string.Empty;

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (SetProperty(ref _filter, value))
                {
                    _collectionView.Refresh();
                    base.RefreshCollection();
                    var a = _collectionView.Cast<CategoryViewModel>();
                }
            }
        }

        private ICollectionView _collectionView;

        public CollectionViewHelper(ObservableCollection<CategoryViewModel> databaseCollection, ObservableCollection<CategoryViewModel> displayCollection)
            : base(databaseCollection, displayCollection)
        {
            _collectionView = CollectionViewSource.GetDefaultView(databaseCollection);
            _collectionView.Filter = FilterCollection;
            
        }

        private bool FilterCollection(object obj)
        {
            if(obj is CategoryViewModel viewModel)
            {
                return viewModel.CategoryName.Contains(Filter);
            }

            return false;
        }


    }
}
