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

        private readonly IDataCollection<Store> _dataCollection;

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ModifyStoreViewModel(IDataCollection<Store> storeCollection, StoreViewModel storeViewModel)
        {
            _dataCollection = storeCollection;
            _storeViewModel = storeViewModel;
            _storeID = storeViewModel.StoreID;
            _storeName = storeViewModel.StoreName;
            _storeStatus = storeViewModel.StoreStatus;
            SubmitCommand = new ModifyDataCommand<Store>(_dataCollection, CreateStore, CanModifyStore);
        }



        private Store CreateStore(object obj)
        {
            return new Store((ModifyStoreViewModel)obj);
        }

        private bool CanModifyStore(object obj)
        {
            return true;
        }

        public static ModifyStoreViewModel LoadViewModel(IDataCollection<Store> collection, StoreViewModel storeViewModel)
        {
            ModifyStoreViewModel viewModel = new ModifyStoreViewModel(collection, storeViewModel);
            return viewModel;
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
