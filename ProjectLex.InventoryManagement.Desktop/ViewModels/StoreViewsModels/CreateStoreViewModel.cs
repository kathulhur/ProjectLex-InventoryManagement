using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateStoreViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Store _store;

        public string StoreName
        {
            get { return _store.StoreName; }
            set
            {
                _store.StoreName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }


        public string StoreStatus
        {
            get { return _store.StoreStatus; }
            set
            {
                _store.StoreStatus = value;
                OnPropertyChanged(nameof(StoreStatus));
            }
        }
        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public CreateStoreViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            SubmitCommand = new RelayCommand(CreateStore, CanCreateStore);
            CancelCommand = new RelayCommand(NavigateToStoreList);
            _store = new Store()
            {
                StoreID = Guid.NewGuid()
            };
        }

        private void CreateStore()
        {
            _unitOfWork.StoreRepository.Insert(_store);
            _unitOfWork.Save();
            MessageBox.Show("[Store]Successful");
        }

        private bool CanCreateStore()
        {
            return true;
        }

        private void NavigateToStoreList()
        {
            _navigationStore.CurrentViewModel = StoreListViewModel.LoadViewModel(_navigationStore);
        }


        public static CreateStoreViewModel LoadViewModel(NavigationStore navigationStore)
        {
            return new CreateStoreViewModel(navigationStore);
        }


        
        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                    _unitOfWork.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }

    }
}
