using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class EditCustomerViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Customer _customer;


        private string _customerFirstName;

        [Required(ErrorMessage = "Firstname is Required")]
        [MinLength(2, ErrorMessage = "Firstname should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Firstname longer than 50 characters is Not Allowed")]
        public string CustomerFirstName
        {
            get => _customerFirstName;
            set
            {
                SetProperty(ref _customerFirstName, value, true);
            }
        }


        private string _customerLastName;

        [Required(ErrorMessage = "Lastname is Required")]
        [MinLength(2, ErrorMessage = "Lastname should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Lastname longer than 50 characters is Not Allowed")]
        public string CustomerLastName
        {
            get => _customerLastName;
            set
            {
                SetProperty(ref _customerLastName, value, true);
            }
        }

        private string _customerAddress;

        [Required(ErrorMessage = "Address is Required")]
        [MinLength(20, ErrorMessage = "Address should be at least 20 characters long")]
        [MaxLength(300, ErrorMessage = "Address longer than 300 characters is not Allowed")]
        public string CustomerAddress
        {
            get => _customerAddress;
            set
            {
                SetProperty(ref _customerAddress, value, true);
            }
        }

        private string _customerPhone;

        [Required(ErrorMessage = "Phone number is Required")]
        public string CustomerPhone
        {
            get => _customerPhone;
            set
            {
                SetProperty(ref _customerPhone, value, true);
            }
        }

        private string _customerEmail;

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        public string CustomerEmail
        {
            get => _customerEmail;
            set
            {
                SetProperty(ref _customerEmail, value, true);
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

        public EditCustomerViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Customer customer, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _customer = customer;
            _unitOfWork = unitOfWork;
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
            _customer.CustomerFirstname = CustomerFirstName;
            _customer.CustomerLastname = CustomerLastName;
            _customer.CustomerAddress = CustomerAddress;
            _customer.CustomerPhone = CustomerPhone;
            _customer.CustomerEmail = CustomerEmail;

            _unitOfWork.CustomerRepository.Update(_customer);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.CUSTOMERS, ActionType.UPDATE, $"Customer updated; CustomerID:{_customer.CustomerID};"));
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

        public static EditCustomerViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Customer customer, Action closeDialogCallback)
        {
            EditCustomerViewModel viewModel = new EditCustomerViewModel(navigationStore, unitOfWork, customer, closeDialogCallback);
            viewModel.LoadStaffsCommand.Execute(null);
            return viewModel;

        }

        private void SetInitialValues(Customer customer)
        {
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
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
