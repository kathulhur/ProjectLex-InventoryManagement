using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class RoleListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        public RoleListViewHelper RoleListViewHelper { get; }


        private readonly ObservableCollection<RoleViewModel> _roles;
        public ObservableCollection<RoleViewModel> Roles { get; }

        public RelayCommand LoadRolesCommand { get; }
        public RelayCommand CreateRoleCommand { get; }
        public RelayCommand<RoleViewModel> EditRoleCommand { get; }
        public RelayCommand<RoleViewModel> RemoveRoleCommand { get; }

        public RoleListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _roles = new ObservableCollection<RoleViewModel>();
            Roles = new ObservableCollection<RoleViewModel>();

            RoleListViewHelper = new RoleListViewHelper(_roles, Roles);

            LoadRolesCommand = new RelayCommand(LoadData);
            RemoveRoleCommand = new RelayCommand<RoleViewModel>(RemoveRole);
            CreateRoleCommand = new RelayCommand(CreateRole);
            EditRoleCommand = new RelayCommand<RoleViewModel>(EditRole);
        }

        public void EditRole(RoleViewModel roleViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditRoleViewModel.LoadViewModel(_navigationStore, _unitOfWork, roleViewModel.Role, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));


            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }


        public void CreateRole()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateRoleViewModel.LoadViewModel(_navigationStore,_unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        public void RemoveRole(RoleViewModel roleViewModel)
        {
            _unitOfWork.RoleRepository.Delete(roleViewModel.Role);
            _unitOfWork.Save();
            _roles.Remove(roleViewModel);
            RoleListViewHelper.RefreshCollection();
            MessageBox.Show("Successful");
        }

        public void LoadData()
        {
            _roles.Clear();
            foreach(Role r in _unitOfWork.RoleRepository.Get())
            {
                _roles.Add(new RoleViewModel(r));
            }
            RoleListViewHelper.RefreshCollection();
        }

        private void CloseDialogCallback()
        {
            LoadRolesCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
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
                    _dialogViewModel?.Dispose();
                    RoleListViewHelper.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
