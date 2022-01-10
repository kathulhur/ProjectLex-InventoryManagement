using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class StoreListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly ObservableCollection<StoreViewModel> _stores;
        public IEnumerable<StoreViewModel> Stores => _stores;

        private readonly StoreCollection _storeCollection;
        private readonly NavigationStore _navigationStore;
        public ICommand LoadStoresCommand { get; }
        public ICommand RemoveStoreCommand { get; }
        public ICommand NavigateToModifyStoreCommand { get; }
        public ICommand NavigateToCreateStoreCommand { get; }

        public StoreListViewModel(NavigationStore navigationStore, StoreCollection storeCollection)
        {
            _navigationStore = navigationStore;
            _storeCollection = storeCollection;
            _storeCollection.StoreRemoved += OnStoreRemoved;
            _stores = new ObservableCollection<StoreViewModel>();
            LoadStoresCommand = new LoadDataCommand<Store>(_storeCollection, OnDataLoaded);
            RemoveStoreCommand = new RemoveDataCommand<Store>(_storeCollection, CreateStore, CanDelete);
            NavigateToModifyStoreCommand = new NavigateCommand(NavigateToModifyStore);
            NavigateToCreateStoreCommand = new NavigateCommand(NavigateToCreateStore);
        }

        public static StoreListViewModel LoadViewModel(NavigationStore navigationStore, StoreCollection storeCollection)
        {
            StoreListViewModel viewModel = new StoreListViewModel(navigationStore, storeCollection);
            viewModel.LoadStoresCommand.Execute(null);

            return viewModel;
        }

        public void NavigateToModifyStore(object obj)
        {
            StoreViewModel storeViewModel = (StoreViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyStoreViewModel.LoadViewModel(_navigationStore, _storeCollection, storeViewModel);
        }

        public void NavigateToCreateStore(object obj)
        {
            _navigationStore.CurrentViewModel = CreateStoreViewModel.LoadViewModel(_navigationStore, _storeCollection);
        }

        public Store CreateStore(object obj)
        {
            return new Store((StoreViewModel)obj);
        }


        private void OnDataLoaded()
        {
            _stores.Clear();

            foreach (Store c in _storeCollection.DataList)
            {
                StoreViewModel newStoreViewModel = new StoreViewModel(c);
                _stores.Add(newStoreViewModel);
            }

        }

        private void OnStoreRemoved(Store store)
        {
            StoreViewModel storeViewModel = _stores.Where(c => c.StoreID == store.StoreID).First();
            _stores.Remove(storeViewModel);
        }

        private bool CanDelete(object obj)
        {
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _storeCollection.StoreRemoved -= OnStoreRemoved;
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

        

    }
}
