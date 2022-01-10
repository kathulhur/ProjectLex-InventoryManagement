using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class RoleListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<RoleViewModel> _roles;
        public IEnumerable<RoleViewModel> Roles => _roles;

        private readonly RoleCollection _roleCollection;

        public ICommand ToCreateRoleCommand { get; }
        public ICommand LoadRolesCommand { get; }
        public ICommand RemoveRoleCommand { get; }
        public ICommand NavigateToModifyRoleCommand { get; }
        public ICommand NavigateToCreateRoleCommand { get; }

        public RoleListViewModel(NavigationStore navigationStore, RoleCollection roleCollection)
        {
            _navigationStore = navigationStore;
            _roleCollection = roleCollection;
            _roleCollection.RoleRemoved += OnRoleRemoved;
            _roles = new ObservableCollection<RoleViewModel>();
            LoadRolesCommand = new LoadDataCommand<Role>(_roleCollection, OnRoleLoaded);
            RemoveRoleCommand = new RemoveDataCommand<Role>(_roleCollection, CreateRole, CanRemoveRole);
            NavigateToModifyRoleCommand = new NavigateCommand(NavigateToModifyRole);
            NavigateToCreateRoleCommand = new NavigateCommand(NavigateToCreateRole);

        }

        public static RoleListViewModel LoadViewModel(NavigationStore navigationStore, RoleCollection roleCollection)
        {
            RoleListViewModel viewModel = new RoleListViewModel(navigationStore, roleCollection);
            viewModel.LoadRolesCommand.Execute(null);

            return viewModel;
        }

        public Role CreateRole(object obj)
        {
            return new Role((RoleViewModel)obj);
        }

        public void NavigateToModifyRole(object obj)
        {
            RoleViewModel roleViewModel = (RoleViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyRoleViewModel.LoadViewModel(_navigationStore, _roleCollection, roleViewModel);

        }

        public void NavigateToCreateRole(object obj)
        {
            _navigationStore.CurrentViewModel = CreateRoleViewModel.LoadViewModel(_navigationStore, _roleCollection);

        }

        public void OnRoleRemoved(Role role)
        {
            RoleViewModel roleViewModel = new RoleViewModel(role);
            RoleViewModel removedRoleViewModel = _roles.First(r => r.RoleID == roleViewModel.RoleID);
            _roles.Remove(removedRoleViewModel);

        }
        
        public bool CanRemoveRole(object obj)
        {
            return true;
        }

        private void OnRoleLoaded()
        {
            _roles.Clear();

            foreach (Role r in _roleCollection.DataList)
            {
                RoleViewModel roleViewModel = new RoleViewModel(r);
                _roles.Add(roleViewModel);
            }

        }

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _roleCollection.RoleRemoved -= OnRoleRemoved;
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
