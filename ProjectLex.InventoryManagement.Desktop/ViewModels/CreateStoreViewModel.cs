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

        private readonly IDataCollection<Store> _dataCollection;

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateStoreViewModel(IDataCollection<Store> storeCollection)
        {
            SubmitCommand = new CreateDataCommand<Store>(storeCollection, CreateStore, CanCreateStore);
            _dataCollection = storeCollection;
        }
        public Store CreateStore(object obj)
        {
            return new Store((CreateStoreViewModel)obj);
        }

        public static CreateStoreViewModel LoadViewModel(IDataCollection<Store> collection)
        {
            return new CreateStoreViewModel(collection);
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
