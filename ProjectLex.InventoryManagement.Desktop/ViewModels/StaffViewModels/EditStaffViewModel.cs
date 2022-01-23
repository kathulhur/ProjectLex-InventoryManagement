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
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditStaffViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;


        private Staff _staff;


        private string _roleID;

        [Required(ErrorMessage = "Role is Required")]
        public string RoleID
        {
            get => _roleID;
            set
            {
                SetProperty(ref _roleID, value);
            }
        }


        private string _staffFirstName;

        [Required(ErrorMessage = "Role is Required")]
        [MinLength(2, ErrorMessage = "Role must be at least 2 characters")]
        public string StaffFirstName
        {
            get => _staffFirstName;
            set
            {
                SetProperty(ref _staffFirstName, value);
            }
        }


        private string _staffLastName;

        [Required(ErrorMessage = "Role is Required")]
        public string StaffLastName
        {
            get => _staffLastName;
            set
            {
                SetProperty(ref _staffLastName, value);
            }
        }

        private string _staffAddress;

        [Required(ErrorMessage = "Role is Required")]
        public string StaffAddress
        {
            get => _staffAddress;
            set
            {
                SetProperty(ref _staffAddress, value);
            }
        }

        private string _staffPhone;

        [Required(ErrorMessage = "Role is Required")]
        public string StaffPhone
        {
            get => _staffPhone;
            set
            {
                SetProperty(ref _staffPhone, value);
            }
        }

        private string _staffEmail;

        [Required(ErrorMessage = "Role is Required")]
        public string StaffEmail
        {
            get => _staffEmail;
            set
            {
                SetProperty(ref _staffEmail, value);
            }
        }


        private string _staffUsername;

        [Required(ErrorMessage = "Role is Required")]
        public string StaffUsername
        {
            get => _staffUsername;
            set
            {
                SetProperty(ref _staffUsername, value);
            }
        }


        private string _staffPassword;

        [Required(ErrorMessage = "Role is Required")]
        public string StaffPassword
        {
            get => _staffPassword;
            set
            {
                SetProperty(ref _staffPassword, value);
            }
        }

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;
        private readonly Action _closeDialogCallback;

        private readonly ObservableCollection<RoleViewModel> _roles;
        public IEnumerable<RoleViewModel> Roles => _roles;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand LoadRolesCommand { get; }

        public EditStaffViewModel(NavigationStore navigationStore, Staff staff, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _staff = staff;
            _unitOfWork = new UnitOfWork();
            _closeDialogCallback = closeDialogCallback;
            _roles = new ObservableCollection<RoleViewModel>();

            SetInitialValues(_staff);

            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadRolesCommand = new RelayCommand(LoadRoles);
        }

        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }
            _staff.RoleID = new Guid(this.RoleID);
            _staff.StaffFirstName = StaffFirstName;
            _staff.StaffLastName = StaffLastName;
            _staff.StaffAddress = StaffAddress;
            _staff.StaffPhone = StaffPhone;
            _staff.StaffEmail = StaffEmail;
            _staff.StaffUsername = StaffUsername;
            _staff.StaffPassword = StaffPassword;


            _unitOfWork.StaffRepository.Update(_staff);
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

        public static EditStaffViewModel LoadViewModel(NavigationStore navigationStore, Staff staff, Action closeDialogCallback)
        {
            EditStaffViewModel viewModel = new EditStaffViewModel(navigationStore, staff, closeDialogCallback);
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
            StaffPassword = staff.StaffPassword;
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
