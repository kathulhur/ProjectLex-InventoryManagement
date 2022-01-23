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
    class EditCustomerViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Customer _customer;


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
        [MinLength(2, ErrorMessage = "Staff must be at least 2 characters")]
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
        public RelayCommand LoadStaffsCommand { get; }

        public EditCustomerViewModel(NavigationStore navigationStore, Customer customer, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _customer = customer;
            _unitOfWork = new UnitOfWork();
            _staffs = new ObservableCollection<StaffViewModel>();
            _closeDialogCallback = closeDialogCallback;

            SetInitialValues(_customer);

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
            _customer.StaffID = new Guid(this.StaffID);
            _customer.CustomerFirstname = CustomerFirstName;
            _customer.CustomerLastname = CustomerLastName;
            _customer.CustomerAddress = CustomerAddress;
            _customer.CustomerPhone = CustomerPhone;
            _customer.CustomerEmail = CustomerEmail;

            _unitOfWork.CustomerRepository.Update(_customer);
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

        public static EditCustomerViewModel LoadViewModel(NavigationStore navigationStore, Customer customer, Action closeDialogCallback)
        {
            EditCustomerViewModel viewModel = new EditCustomerViewModel(navigationStore, customer, closeDialogCallback);
            viewModel.LoadStaffsCommand.Execute(null);
            return viewModel;

        }

        private void SetInitialValues(Customer customer)
        {
            StaffID = customer.StaffID.ToString();
            CustomerFirstName = customer.CustomerFirstname;
            CustomerLastName = customer.CustomerLastname;
            CustomerAddress = customer.CustomerAddress;
            CustomerPhone = customer.CustomerPhone;
            CustomerEmail = customer.CustomerEmail;
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
