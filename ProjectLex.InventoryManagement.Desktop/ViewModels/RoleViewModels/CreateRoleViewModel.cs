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
    public class CreateRoleViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        public string _roleName;

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string RoleName
        {
            get => _roleName;
            set
            {
                SetProperty(ref _roleName, value);
            }

        }


        private string _roleDescription;

        [Required(ErrorMessage = "Description is Required")]
        [MinLength(10, ErrorMessage = "Description should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Description longer than 50 characters is Not Allowed")]
        public string RoleDescription
        {
            get => _roleDescription;
            set
            {
                SetProperty(ref _roleDescription, value);
            }
        }


        private string _roleStatus;

        [Required(ErrorMessage = "Status is Required")]
        public string RoleStatus
        {
            get { return _roleStatus; }
            set
            {
                SetProperty(ref _roleStatus, value);
            }
        }


        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly Action _closeDialogCallback;
        
        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public CreateRoleViewModel(NavigationStore navigationStore, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _closeDialogCallback = closeDialogCallback;
            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }


        public void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Role newRole = new Role
            {
                RoleID = Guid.NewGuid(),
                RoleName = this.RoleName,
                RoleDescription = this.RoleDescription,
                RoleStatus = this.RoleStatus
            };

            _unitOfWork.RoleRepository.Insert(newRole);
            _unitOfWork.Save();

            _closeDialogCallback();
        }

        public void Cancel()
        {
            _closeDialogCallback();
        }


        public static CreateRoleViewModel LoadViewModel(NavigationStore navigationStore, Action closeDialogCallback)
        {
            return new CreateRoleViewModel(navigationStore, closeDialogCallback);
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

        public bool CanCreateRole(object obj)
        {
            return true;
        }
    }
}
