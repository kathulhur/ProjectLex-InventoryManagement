using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateRoleViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private string _roleName;
        public string RoleName
        {
            get { return _roleName; }
            set
            {
                _roleName = value;
                OnPropertyChanged(nameof(RoleName));
            }
        }

        private string _roleStatus;
        public string RoleStatus
        {
            get { return _roleStatus; }
            set
            {
                _roleStatus = value;
                OnPropertyChanged(nameof(RoleStatus));
            }
        }
       

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public CreateRoleViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            SubmitCommand = new RelayCommand(CreateRole);
            CancelCommand = new RelayCommand(NavigateToRoleList);
            _unitOfWork = new UnitOfWork();
        }


        public void CreateRole()
        {
            Role newRole = new Role
            {
                RoleID = Guid.NewGuid(),
                RoleName = this.RoleName,
                RoleStatus = this.RoleStatus
            };
            _unitOfWork.RoleRepository.Insert(newRole);
            _unitOfWork.Save();

        }

        public void NavigateToRoleList()
        {
            _navigationStore.CurrentViewModel = RoleListViewModel.LoadViewModel(_navigationStore);
        }


        public static CreateRoleViewModel LoadViewModel(NavigationStore navigationStore)
        {
            return new CreateRoleViewModel(navigationStore);
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
