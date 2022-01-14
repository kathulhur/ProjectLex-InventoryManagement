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
    public class EditSupplierViewModel : ViewModelBase
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

        public EditSupplierViewModel(NavigationStore navigationStore, Supplier supplier)
        {
            _navigationStore = navigationStore;
            _supplier = supplier;
            _unitOfWork = new UnitOfWork();

            SubmitCommand = new RelayCommand(EditSupplier, CanModifySupplier);
            CancelCommand = new RelayCommand(NavigateToSupplierList);
        }


        private void EditSupplier()
        {
            _unitOfWork.SupplierRepository.Update(_supplier);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }

        private bool CanModifySupplier()
        {
            return true;
        }

        private void NavigateToSupplierList()
        {
            _navigationStore.CurrentViewModel = SupplierListViewModel.LoadViewModel(_navigationStore);
        }

        public static EditSupplierViewModel LoadViewModel(NavigationStore navigationStore, Supplier supplier)
        {
            EditSupplierViewModel viewModel = new EditSupplierViewModel(navigationStore, supplier);
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


        public bool CanModifySupplier(object obj)
        {
            return true;
        }
    }
}
