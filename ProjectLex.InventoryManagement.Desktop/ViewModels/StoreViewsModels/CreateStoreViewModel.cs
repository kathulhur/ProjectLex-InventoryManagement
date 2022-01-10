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
    public class CreateStoreViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly StoreCollection _storeCollection;
        private readonly NavigationStore _navigationStore;

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateStoreViewModel(NavigationStore navigationStore, StoreCollection storeCollection)
        {
            _storeCollection = storeCollection;
            _navigationStore = navigationStore;
            SubmitCommand = new CreateDataCommand<Store>(storeCollection, CreateStore, CanCreateStore);
            CancelCommand = new NavigateCommand(NavigateToStoreList);
        }
        public static CreateStoreViewModel LoadViewModel(NavigationStore navigationStore, StoreCollection storeCollection)
        {
            return new CreateStoreViewModel(navigationStore, storeCollection);
        }

        public void NavigateToStoreList(object obj)
        {
            _navigationStore.CurrentViewModel = StoreListViewModel.LoadViewModel(_navigationStore, _storeCollection);
        }


        public Store CreateStore(object obj)
        {
            return new Store((CreateStoreViewModel)obj);
        }

        
        private string _storeId;

        public string StoreId
        {
            get { return _storeId; }
            set
            {
                _storeId = value;
                OnPropertyChanged(nameof(StoreId));
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

        private bool CanCreateStore(object obj)
        {
            return true;
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
