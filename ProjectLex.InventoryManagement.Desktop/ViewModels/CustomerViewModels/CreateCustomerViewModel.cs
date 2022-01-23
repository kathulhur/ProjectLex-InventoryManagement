using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class CreateCustomerViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        private string _staffID;

        [Required(ErrorMessage = "Staff is Required")]
        public string StaffID
        {
            get => _staffID;
            set
            {
                SetProperty(ref _staffID, value);
            }
        }

        private string _customerFirstName;

        [Required(ErrorMessage = "Firstname is Required")]
        [MinLength(2, ErrorMessage = "Firstname must be at least 2 characters")]
        public string CustomerFirstName
        {
            get => _customerFirstName;
            set
            {
                SetProperty(ref _customerFirstName, value);
            }
        }


        private string _customerLastName;

        [Required(ErrorMessage = "Lastname is Required")]
        [MinLength(2, ErrorMessage = "Lastname must be at least 2 characters")]
        public string CustomerLastName
        {
            get => _customerLastName;
            set
            {
                SetProperty(ref _customerLastName, value);
            }
        }

        private string _customerAddress;

        [Required(ErrorMessage = "Address is Required")]
        public string CustomerAddress
        {
            get => _customerAddress;
            set
            {
                SetProperty(ref _customerAddress, value);
            }
        }

        private string _customerPhone;

        [Required(ErrorMessage = "Phone number is Required")]
        public string CustomerPhone
        {
            get => _customerPhone;
            set
            {
                SetProperty(ref _customerPhone, value);
            }
        }

        private string _customerEmail;

        [Required(ErrorMessage = "Email is Required")]
        public string CustomerEmail
        {
            get => _customerEmail;
            set
            {
                SetProperty(ref _customerEmail, value);
            }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;
        private readonly Action _closeDialogCallback;

        private readonly ObservableCollection<StaffViewModel> _staffs;
        public IEnumerable<StaffViewModel> Staffs => _staffs;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadStaffsCommand { get; }

        public CreateCustomerViewModel(NavigationStore navigationStore, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _staffs = new ObservableCollection<StaffViewModel>();
            _closeDialogCallback = closeDialogCallback;


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadStaffsCommand = new RelayCommand(LoadStaffs);
        }

        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Customer newCustomer = new Customer()
            {
                CustomerID = Guid.NewGuid(),
                StaffID = new Guid(this.StaffID),
                CustomerFirstname = CustomerFirstName,
                CustomerLastname = CustomerLastName,
                CustomerAddress = CustomerAddress,
                CustomerPhone = CustomerPhone,
                CustomerEmail = CustomerEmail,
            };

            _unitOfWork.CustomerRepository.Insert(newCustomer);
            _unitOfWork.Save();

            _closeDialogCallback();
        }


        private void Cancel()
        {
            _closeDialogCallback();
        }

        private void LoadStaffs()
        {
            _staffs.Clear();
            foreach (Staff r in _unitOfWork.StaffRepository.Get())
            {
                _staffs.Add(new StaffViewModel(r));
            }
        }

        public static CreateCustomerViewModel LoadViewModel(NavigationStore navigationStore, Action closeDialogCallback)
        {
            CreateCustomerViewModel viewModel = new CreateCustomerViewModel(navigationStore, closeDialogCallback);
            viewModel.LoadStaffsCommand.Execute(null);
            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
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
