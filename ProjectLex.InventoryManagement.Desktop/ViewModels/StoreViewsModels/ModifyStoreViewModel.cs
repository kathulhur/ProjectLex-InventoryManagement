using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ModifyStoreViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly StoreCollection _storeCollection;
        private readonly NavigationStore _navigationStore;

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ModifyStoreViewModel(NavigationStore navigationStore, StoreCollection storeCollection, StoreViewModel storeViewModel)
        {
            _navigationStore = navigationStore;
            _storeCollection = storeCollection;
            _storeViewModel = storeViewModel;
            _storeID = storeViewModel.StoreID;
            _storeName = storeViewModel.StoreName;
            _storeStatus = storeViewModel.StoreStatus;
            SubmitCommand = new ModifyDataCommand<Store>(_storeCollection, CreateStore, CanModifyStore);
            CancelCommand = new NavigateCommand(NavigateToStoreList);
        }
        public static ModifyStoreViewModel LoadViewModel(NavigationStore navigationStore, StoreCollection storeCollection, StoreViewModel storeViewModel)
        {
            ModifyStoreViewModel viewModel = new ModifyStoreViewModel(navigationStore, storeCollection, storeViewModel);
            return viewModel;
        }

        public void NavigateToStoreList(object obj)
        {
            _navigationStore.CurrentViewModel = StoreListViewModel.LoadViewModel(_navigationStore, _storeCollection);
        }



        private Store CreateStore(object obj)
        {
            return new Store((ModifyStoreViewModel)obj);
        }

        private bool CanModifyStore(object obj)
        {
            return true;
        }


        private readonly StoreViewModel _storeViewModel;

        private string _storeID;
        public string StoreID
        {
            get { return _storeID; }
            set
            {
                _storeID = value;
                OnPropertyChanged(nameof(StoreID));
            }
        }

        private string _storeName;

        public string StoreName
        {
            get { return _storeName; }
            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }

        private string _storeStatus;

        public string StoreStatus
        {
            get { return _storeStatus; }
            set
            {
                _storeStatus = value;
                OnPropertyChanged(nameof(StoreStatus));
            }
        }

        
        
        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
