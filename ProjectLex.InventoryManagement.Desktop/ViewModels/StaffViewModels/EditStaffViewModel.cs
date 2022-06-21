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
using System.Windows.Controls;
using System.Windows.Input;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditStaffViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Staff _staff;

        private string _roleID;

        [Required(ErrorMessage = "Role is Required")]
        public string RoleID
        {
            get => _roleID;
            set
            {
                SetProperty(ref _roleID, value, true);
            }
        }


        private string _staffFirstName;

        [Required(ErrorMessage = "Firstname is Required")]
        [MinLength(2, ErrorMessage = "Firstname should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Firstname longer than 50 characters is Not Allowed")]
        public string StaffFirstName
        {
            get => _staffFirstName;
            set
            {
                SetProperty(ref _staffFirstName, value, true);
            }
        }


        private string _staffLastName;

        [Required(ErrorMessage = "Lastname is Required")]
        [MinLength(2, ErrorMessage = "Lastname should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Lastname longer than 50 characters is Not Allowed")]
        public string StaffLastName
        {
            get => _staffLastName;
            set
            {
                SetProperty(ref _staffLastName, value, true);
            }
        }

        private string _staffAddress;

        [Required(ErrorMessage = "Address is Required")]
        [MinLength(20, ErrorMessage = "Address should be at least 20 characters long")]
        [MaxLength(300, ErrorMessage = "Address longer than 300 characters is not Allowed")]
        public string StaffAddress
        {
            get => _staffAddress;
            set
            {
                SetProperty(ref _staffAddress, value, true);
            }
        }

        private string _staffPhone;

        [Required(ErrorMessage = "Phone number is Required")]
        public string StaffPhone
        {
            get => _staffPhone;
            set
            {
                SetProperty(ref _staffPhone, value, true);
            }
        }

        private string _staffEmail;

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        public string StaffEmail
        {
            get => _staffEmail;
            set
            {
                SetProperty(ref _staffEmail, value, true);
            }
        }


        private string _staffUsername;

        [Required(ErrorMessage = "Username is Required")]
        [MinLength(2, ErrorMessage = "Username should be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "Username longer than 50 characters is Not Allowed")]
        public string StaffUsername
        {
            get => _staffUsername;
            set
            {
                SetProperty(ref _staffUsername, value, true);
            }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;
        private readonly Action _closeDialogCallback;

        private readonly ObservableCollection<RoleViewModel> _roles;
        public IEnumerable<RoleViewModel> Roles => _roles;

        public RelayCommand<object> SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand LoadRolesCommand { get; }

        public EditStaffViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Staff staff, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _staff = staff;
            _unitOfWork = unitOfWork;
            _closeDialogCallback = closeDialogCallback;
            _roles = new ObservableCollection<RoleViewModel>();

            SetInitialValues(_staff);

            SubmitCommand = new RelayCommand<object>(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadRolesCommand = new RelayCommand(LoadRoles);
        }

        private void Submit(object obj)
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            PasswordBox passwordBox = obj as PasswordBox;

            _staff.RoleID = new Guid(_roleID);
            _staff.StaffFirstName = StaffFirstName;
            _staff.StaffLastName = StaffLastName;
            _staff.StaffAddress = StaffAddress;
            _staff.StaffPhone = StaffPhone;
            _staff.StaffEmail = StaffEmail;
            _staff.StaffUsername = StaffUsername;
            _staff.StaffPassword = passwordBox.Password;


            _unitOfWork.StaffRepository.Update(_staff);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.STAFFS, ActionType.UPDATE, $"Staff Updated; StaffID:{_staff.StaffID};"));
            _unitOfWork.Save();
            _closeDialogCallback();
        }


        private void Cancel()
        {
            _closeDialogCallback();
        }

        private void LoadRoles()
        {
            _roles.Clear();
            foreach(Role r in _unitOfWork.RoleRepository.Get())
            {
                _roles.Add(new RoleViewModel(r));
            }

        }

        public static EditStaffViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Staff staff, Action closeDialogCallback)
        {
            EditStaffViewModel viewModel = new EditStaffViewModel(navigationStore, unitOfWork, staff, closeDialogCallback);
            viewModel.LoadRolesCommand.Execute(null);
            return viewModel;
            
        }

        private void SetInitialValues(Staff staff)
        {
            RoleID = staff.RoleID.ToString();
            StaffFirstName = staff.StaffFirstName;
            StaffLastName = staff.StaffLastName;
            StaffAddress = staff.StaffAddress;
            StaffPhone = staff.StaffPhone;
            StaffEmail = staff.StaffEmail;
            StaffUsername = staff.StaffUsername;
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
