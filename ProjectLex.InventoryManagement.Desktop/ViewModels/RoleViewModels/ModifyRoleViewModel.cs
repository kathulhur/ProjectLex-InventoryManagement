using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ModifyRoleViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private RoleViewModel _roleViewModel;

        private string _roleID;
        public string RoleID
        {
            get { return _roleID; }
            set
            {
                _roleID = value;
                OnPropertyChanged(nameof(RoleID));
            }
        }

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
       

        private readonly RoleCollection _roleCollection;
        private readonly NavigationStore _navigationStore;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ModifyRoleViewModel(NavigationStore navigationStore, RoleCollection roleCollection, RoleViewModel roleViewModel)
        {
            _navigationStore = navigationStore;
            _roleCollection = roleCollection;
            _roleViewModel = roleViewModel;
            _roleID = roleViewModel.RoleID;
            _roleName = roleViewModel.RoleName;
            _roleStatus = roleViewModel.RoleStatus;
            SubmitCommand = new ModifyDataCommand<Role>(roleCollection, CreateRole, CanModifyRole);
            CancelCommand = new NavigateCommand(NavigateToRoleList);
        }

        public static ModifyRoleViewModel LoadViewModel(NavigationStore navigationStore, RoleCollection roleCollection, RoleViewModel roleViewModel)
        {
            ModifyRoleViewModel viewModel = new ModifyRoleViewModel(navigationStore, roleCollection, roleViewModel);
            return viewModel;
        }

        public void NavigateToRoleList(object obj)
        {
            _navigationStore.CurrentViewModel = RoleListViewModel.LoadViewModel(_navigationStore, _roleCollection);
        }

        private Role CreateRole(object obj)
        {
            return new Role((ModifyRoleViewModel)obj);
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


        public bool CanModifyRole(object obj)
        {
            return true;
        }
    }
}
