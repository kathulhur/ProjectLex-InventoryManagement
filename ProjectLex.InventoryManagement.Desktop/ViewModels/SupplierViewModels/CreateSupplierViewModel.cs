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
    public class CreateSupplierViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Supplier _supplier;


        public string SupplierName
        {
            get { return _supplier.SupplierName; }
            set
            {
                _supplier.SupplierName = value;
                OnPropertyChanged(nameof(SupplierName));
            }
        }

        private string _supplierStatus;
        public string SupplierStatus
        {
            get { return _supplier.SupplierStatus; }
            set
            {
                _supplier.SupplierStatus = value;
                OnPropertyChanged(nameof(SupplierStatus));
            }
        }
       

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public CreateSupplierViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            SubmitCommand = new RelayCommand(CreateSupplier, CanCreateSupplier);
            CancelCommand = new RelayCommand(NavigateToSupplierList);
            _supplier = new Supplier()
            {
                SupplierID = Guid.NewGuid()
            };
        }


        private void CreateSupplier()
        {
            _unitOfWork.SupplierRepository.Insert(_supplier);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }

        private bool CanCreateSupplier()
        {
            return true;
        }

        private void NavigateToSupplierList()
        {
            _navigationStore.CurrentViewModel = SupplierListViewModel.LoadViewModel(_navigationStore);
        }


        public static CreateSupplierViewModel LoadViewModel(NavigationStore navigationStore)
        {
            return new CreateSupplierViewModel(navigationStore);
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

        public bool CanCreateSupplier(object obj)
        {
            return true;
        }
    }
}
