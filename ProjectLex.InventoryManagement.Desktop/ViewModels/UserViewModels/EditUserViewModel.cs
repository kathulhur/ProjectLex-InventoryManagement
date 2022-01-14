using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditUserViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private User _user;


        public RoleViewModel Role
        {
            get { return _roles.Single(r => r.RoleID == _user.Role.RoleID.ToString()); }
            set
            {
                _user.Role = value.Role;
                OnPropertyChanged(nameof(Role));
            }
        }

        public string UserUsername
        {
            get { return _user.UserUsername; }
            set
            {
                _user.UserUsername = value;
                OnPropertyChanged(nameof(UserUsername));
            }
        }

        public string UserPassword
        {
            get { return _user.UserPassword; }
            set
            {
                _user.UserPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }
       
        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<RoleViewModel> _roles;
        public IEnumerable<RoleViewModel> Roles => _roles;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand LoadRolesCommand { get; }

        public EditUserViewModel(NavigationStore navigationStore, User user)
        {
            _navigationStore = navigationStore;
            _user = user;
            _unitOfWork = new UnitOfWork();

            _roles = new ObservableCollection<RoleViewModel>();

            SubmitCommand = new RelayCommand(EditUser, CanEditUser);
            CancelCommand = new RelayCommand(NavigateToUserList);
            LoadRolesCommand = new RelayCommand(LoadRoles);
        }

        private void EditUser()
        {
            _unitOfWork.UserRepository.Update(_user);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }

        private bool CanEditUser()
        {
            return true;
        }

        private void NavigateToUserList()
        {
            _navigationStore.CurrentViewModel = UserListViewModel.LoadViewModel(_navigationStore);
        }

        private void LoadRoles()
        {
            _roles.Clear();
            foreach(Role r in _unitOfWork.RoleRepository.Get())
            {
                _roles.Add(new RoleViewModel(r));
            }

        }

        public static EditUserViewModel LoadViewModel(NavigationStore navigationStore, User user)
        {
            EditUserViewModel viewModel = new EditUserViewModel(navigationStore, user);
            viewModel.LoadRolesCommand.Execute(null);
            return viewModel;
            
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
