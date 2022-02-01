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
                SetProperty(ref _staffID, value, true);
            }
        }

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
        [StringLength(11, ErrorMessage = "Phone number should be 11 characters long")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone should only contain numbers")]
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
        private RelayCommand LoadStaffsCommand { get; }

        public CreateCustomerViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
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

        public static CreateCustomerViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            CreateCustomerViewModel viewModel = new CreateCustomerViewModel(navigationStore, unitOfWork, closeDialogCallback);
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
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
