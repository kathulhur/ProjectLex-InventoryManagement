using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string _supplierName;

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string SupplierName
        {
            get => _supplierName;
            set
            {
                SetProperty(ref _supplierName, value, true);
            }

        }

        private string _supplierAddress;

        [Required(ErrorMessage = "Address is Required")]
        [MinLength(10, ErrorMessage = "Address should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Address longer than 50 characters is Not Allowed")]
        public string SupplierAddress
        {
            get => _supplierAddress;
            set
            {
                SetProperty(ref _supplierAddress, value, true);
            }
        }

        private string _supplierPhone;

        [Required(ErrorMessage = "Phone number is Required")]
        [StringLength(11, ErrorMessage = "Phone number should be 11 characters long")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone should only contain numbers")]
        public string SupplierPhone
        {
            get => _supplierPhone;
            set
            {
                SetProperty(ref _supplierPhone, value, true);
            }
        }


        private string _supplierEmail;

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        public string SupplierEmail
        {
            get => _supplierEmail;
            set
            {
                SetProperty(ref _supplierEmail, value, true);
            }
        }


        private string _supplierStatus;

        [Required(ErrorMessage = "Status is Required")]
        public string SupplierStatus
        {
            get { return _supplierStatus; }
            set
            {
                SetProperty(ref _supplierStatus, value, true);
            }
        }


        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly Action _closeDialogCallback;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public CreateSupplierViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _closeDialogCallback = closeDialogCallback;


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }


        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Supplier supplier = new Supplier()
            {
                SupplierID = Guid.NewGuid(),
                SupplierName = SupplierName,
                SupplierAddress = SupplierAddress,
                SupplierPhone = SupplierPhone,
                SupplierEmail = SupplierEmail,
                SupplierStatus = SupplierStatus
            };

            _unitOfWork.SupplierRepository.Insert(supplier);
            _unitOfWork.Save();
            _closeDialogCallback();
        }



        private void Cancel()
        {
            _closeDialogCallback();
        }


        public static CreateSupplierViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            return new CreateSupplierViewModel(navigationStore, unitOfWork, closeDialogCallback);
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

        public bool CanCreateSupplier(object obj)
        {
            return true;
        }
    }
}
