using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjectLex.InventoryManagement.Desktop.Views.ListViewHelpers
{
    public abstract class ListViewHelperBase<TViewModel> : ViewModelBase
    {
        private bool _isDisposed = false;

        private int _currentPage = 1;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);
            }
        }

        private int _numberOfPages = 1;

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set
            {
                SetProperty(ref _numberOfPages, value);
            }
        }

        private int _selectedRecordsPerPage = 10;

        public int SelectedRecordsPerPage
        {
            get { return _selectedRecordsPerPage; }
            set
            {
                SetProperty(ref _selectedRecordsPerPage, value);
                UpdateRecordsPerPage();
            }
        }


        private string _filter = string.Empty;

        public string Filter
        {
            get { return _filter; }
            set
            {
                SetProperty(ref _filter, value);
                _collectionView.Refresh();
                RefreshCollection();
            }
        }


        private readonly ObservableCollection<TViewModel> _databaseCollection;
        private ICollectionView _collectionView;
        public readonly ObservableCollection<TViewModel> DisplayCollection;


        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }

        private IEnumerable<int> _recordsPerPage = new List<int> { 10, 20, 30 };
        public IEnumerable<int> RecordsPerPage => _recordsPerPage;

        public ListViewHelperBase(ObservableCollection<TViewModel> databaseCollection, ObservableCollection<TViewModel> displayCollection)
        {
            _databaseCollection = databaseCollection;
            _collectionView = CollectionViewSource.GetDefaultView(databaseCollection);
            _collectionView.Filter = FilterCollection;
            DisplayCollection = displayCollection;

            NextPageCommand = new RelayCommand(NextPage, () => CurrentPage < NumberOfPages);
            PreviousPageCommand = new RelayCommand(PreviousPage, () => CurrentPage > 1);
            FirstPageCommand = new RelayCommand(FirstPage, () => CurrentPage > 1);
            LastPageCommand = new RelayCommand(LastPage, () => CurrentPage < NumberOfPages);
        }

        protected abstract bool FilterCollection(object obj);


        private void UpdateRecordsPerPage()
        {
            NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_collectionView.Cast<TViewModel>().Count()) / SelectedRecordsPerPage));
            NumberOfPages = NumberOfPages == 0 ? 1 : NumberOfPages;
            FirstPage();
        }

        private void UpdateCollection(IEnumerable<TViewModel> collection)
        {
            DisplayCollection.Clear();
            foreach (TViewModel tvm in collection)
            {
                DisplayCollection.Add(tvm);
            }
        }

        private void UpdateButtonEnableStates()
        {
            NextPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged();
            FirstPageCommand.NotifyCanExecuteChanged();
            LastPageCommand.NotifyCanExecuteChanged();
        }

        private void NextPage()
        {
            CurrentPage++;
            int offset = (CurrentPage - 1) * SelectedRecordsPerPage;
            UpdateCollection(_collectionView.Cast<TViewModel>().Skip(offset).Take(SelectedRecordsPerPage));
            UpdateButtonEnableStates();
        }

        private void PreviousPage()
        {
            CurrentPage--;
            int offset = (CurrentPage - 1) * SelectedRecordsPerPage;
            UpdateCollection(_collectionView.Cast<TViewModel>().Skip(offset).Take(SelectedRecordsPerPage));
            UpdateButtonEnableStates();
        }

        private void FirstPage()
        {
            UpdateCollection(_collectionView.Cast<TViewModel>().Take(SelectedRecordsPerPage));
            CurrentPage = 1;
            UpdateButtonEnableStates();
        }

        private void LastPage()
        {
            int offset = (NumberOfPages - 1) * SelectedRecordsPerPage;
            UpdateCollection(_collectionView.Cast<TViewModel>().Skip(offset).Take(SelectedRecordsPerPage));
            CurrentPage = NumberOfPages;
            UpdateButtonEnableStates();
        }

        public void RefreshCollection()
        {
            UpdateRecordsPerPage();
            int offset = (CurrentPage - 1) * SelectedRecordsPerPage;
            UpdateCollection(_collectionView.Cast<TViewModel>().Skip(offset).Take(SelectedRecordsPerPage));
            UpdateButtonEnableStates();
        }



        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
