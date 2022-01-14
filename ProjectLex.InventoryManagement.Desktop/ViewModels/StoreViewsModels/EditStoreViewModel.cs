using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditStoreViewModel : ViewModelBase
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

        public EditStoreViewModel(NavigationStore navigationStore, Store store)
        {
            _navigationStore = navigationStore;
            _store = store;
            _unitOfWork = new UnitOfWork();
            SubmitCommand = new RelayCommand(EditStore);
            CancelCommand = new RelayCommand(NavigateToStoreList);
        }


        public void EditStore()
        {
            _unitOfWork.StoreRepository.Update(_store);
            _unitOfWork.Save();
        }

        public void NavigateToStoreList()
        {
            _navigationStore.CurrentViewModel = StoreListViewModel.LoadViewModel(_navigationStore);
        }

        public static EditStoreViewModel LoadViewModel(NavigationStore navigationStore, Store store)
        {
            EditStoreViewModel viewModel = new EditStoreViewModel(navigationStore, store);
            return viewModel;
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
