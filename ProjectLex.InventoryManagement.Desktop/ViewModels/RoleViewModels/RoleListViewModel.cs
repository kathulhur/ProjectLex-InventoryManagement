using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
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

        private UnitOfWork _unitOfWork;

        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<RoleViewModel> _roles;
        public IEnumerable<RoleViewModel> Roles => _roles;

        public RelayCommand LoadRolesCommand { get; }
        public RelayCommand NavigateToCreateRoleCommand { get; }
        public RelayCommand<RoleViewModel> NavigateToEditRoleCommand { get; }
        public RelayCommand<RoleViewModel> RemoveRoleCommand { get; }

        public RoleListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _unitOfWork = new UnitOfWork();
            _roles = new ObservableCollection<RoleViewModel>();
            LoadRolesCommand = new RelayCommand(LoadData);
            RemoveRoleCommand = new RelayCommand<RoleViewModel>(RemoveRole);
            NavigateToCreateRoleCommand = new RelayCommand(NavigateToCreateRole);
            NavigateToEditRoleCommand = new RelayCommand<RoleViewModel>(NavigateToEditRole);
        }

        public void NavigateToEditRole(RoleViewModel roleViewModel)
        {
            _navigationStore.CurrentViewModel = EditRoleViewModel.LoadViewModel(_navigationStore, roleViewModel.Role);
        }


        public void NavigateToCreateRole()
        {
            _navigationStore.CurrentViewModel = CreateRoleViewModel.LoadViewModel(_navigationStore);
        }

        public void RemoveRole(RoleViewModel roleViewModel)
        {
            _unitOfWork.RoleRepository.Delete(roleViewModel.Role);
            _unitOfWork.Save();
            _roles.Remove(roleViewModel);

        }

        public void LoadData()
        {
            foreach(Role r in _unitOfWork.RoleRepository.Get())
            {
                _roles.Add(new RoleViewModel(r));
            }
        }


        public static RoleListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            RoleListViewModel viewModel = new RoleListViewModel(navigationStore);
            viewModel.LoadRolesCommand.Execute(null);
            return viewModel;
        }



        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _unitOfWork.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
