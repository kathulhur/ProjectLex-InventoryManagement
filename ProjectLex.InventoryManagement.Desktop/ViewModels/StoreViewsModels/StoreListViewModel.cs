using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectLex.InventoryManagement.Desktop.DAL;
using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class StoreListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly ObservableCollection<StoreViewModel> _stores;
        public IEnumerable<StoreViewModel> Stores => _stores;

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        public RelayCommand LoadStoresCommand { get; }
        public RelayCommand<StoreViewModel> RemoveStoreCommand { get; }
        public RelayCommand<StoreViewModel> NavigateToModifyStoreCommand { get; }
        public RelayCommand NavigateToCreateStoreCommand { get; }

        public StoreListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _stores = new ObservableCollection<StoreViewModel>();

            LoadStoresCommand = new RelayCommand(LoadStores);
            RemoveStoreCommand = new RelayCommand<StoreViewModel>(RemoveStore);
            NavigateToModifyStoreCommand = new RelayCommand<StoreViewModel>(NavigateToModifyStore);
            NavigateToCreateStoreCommand = new RelayCommand(NavigateToCreateStore);
        }

        private void NavigateToModifyStore(StoreViewModel storeViewModel)
        {
            _navigationStore.CurrentViewModel = EditStoreViewModel.LoadViewModel(_navigationStore, storeViewModel.Store);
        }
        private void RemoveStore(StoreViewModel storeViewModel)
        {
            _unitOfWork.StoreRepository.Delete(storeViewModel.Store);
            _unitOfWork.Save();
            _stores.Remove(storeViewModel);
            MessageBox.Show("Delete Successful");
        }

        private void LoadStores()
        {
            foreach(Store s in _unitOfWork.StoreRepository.Get())
            {
                _stores.Add(new StoreViewModel(s));
            }
        }

        private void NavigateToCreateStore()
        {
            _navigationStore.CurrentViewModel = CreateStoreViewModel.LoadViewModel(_navigationStore);
        }

        public static StoreListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            StoreListViewModel viewModel = new StoreListViewModel(navigationStore);
            viewModel.LoadStoresCommand.Execute(null);

            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _unitOfWork.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

        

    }
}
