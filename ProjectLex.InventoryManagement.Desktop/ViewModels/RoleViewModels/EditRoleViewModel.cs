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
    public class EditRoleViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Role _role;

        public string RoleName
        {
            get { return _role.RoleName; }
            set
            {
                _role.RoleName = value;
                OnPropertyChanged(nameof(RoleName));
            }
        }

        public string RoleStatus
        {
            get { return _role.RoleStatus; }
            set
            {
                _role.RoleStatus = value;
                OnPropertyChanged(nameof(RoleStatus));
            }
        }

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public EditRoleViewModel(NavigationStore navigationStore, Role role)
        {
            _unitOfWork = new UnitOfWork();
            _navigationStore = navigationStore;
            _role = role;
            SubmitCommand = new RelayCommand(EditRole);
            CancelCommand = new RelayCommand(NavigateToRoleList);
        }
        private void EditRole()
        {
            _unitOfWork.RoleRepository.Update(_role);
            _unitOfWork.Save();
        }

        public static EditRoleViewModel LoadViewModel(NavigationStore navigationStore, Role role)
        {
            EditRoleViewModel viewModel = new EditRoleViewModel(navigationStore, role);
            return viewModel;
        }

        public void NavigateToRoleList()
        {
            _navigationStore.CurrentViewModel = RoleListViewModel.LoadViewModel(_navigationStore);
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


        public bool CanModifyRole(object obj)
        {
            return true;
        }
    }
}
