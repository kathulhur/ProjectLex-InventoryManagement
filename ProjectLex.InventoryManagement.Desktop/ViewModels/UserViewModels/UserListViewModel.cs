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
    public class UserListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<UserViewModel> _users;
        public IEnumerable<UserViewModel> Users => _users;


        private readonly ObservableCollection<RoleViewModel> _roles;
        public IEnumerable<RoleViewModel> Roles => _roles;


        private readonly UserCollection _userCollection;
        private readonly RoleCollection _roleCollection;

        public ICommand ToCreateUserCommand { get; }
        public ICommand LoadUsersCommand { get; }
        public ICommand LoadRolesCommand { get; }
        public ICommand RemoveUserCommand { get; }
        public ICommand NavigateToModifyUserCommand { get; }
        public ICommand NavigateToCreateUserCommand { get; }

        public UserListViewModel(NavigationStore navigationStore, UserCollection userCollection, RoleCollection roleCollection)
        {
            _navigationStore = navigationStore;
            _userCollection = userCollection;
            _roleCollection = roleCollection;

            _userCollection.UserRemoved += OnUserRemoved;

            _users = new ObservableCollection<UserViewModel>();
            _roles = new ObservableCollection<RoleViewModel>();

            LoadRolesCommand = new LoadDataCommand<Role>(_roleCollection, OnRolesLoaded);
            LoadUsersCommand = new LoadDataCommand<User>(_userCollection, OnUserLoaded);

            RemoveUserCommand = new RemoveDataCommand<User>(_userCollection, CreateUser, CanRemoveUser);
            NavigateToModifyUserCommand = new NavigateCommand(NavigateToModifyUser);
            NavigateToCreateUserCommand = new NavigateCommand(NavigateToCreateUser);

        }

        public static UserListViewModel LoadViewModel(NavigationStore navigationStore, UserCollection userCollection, RoleCollection roleCollection)
        {
            UserListViewModel viewModel = new UserListViewModel(navigationStore, userCollection, roleCollection);
            viewModel.LoadRolesCommand.Execute(null);
            viewModel.LoadUsersCommand.Execute(null);

            return viewModel;
        }

        public void NavigateToModifyUser(object obj)
        {
            UserViewModel userViewModel = (UserViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyUserViewModel.LoadViewModel(_navigationStore, _userCollection, _roleCollection, userViewModel);

        }


        public void NavigateToCreateUser(object obj)
        {
            _navigationStore.CurrentViewModel = CreateUserViewModel.LoadViewModel(_navigationStore, _userCollection, _roleCollection);
        }

        public User CreateUser(object obj)
        {
            return new User((UserViewModel)obj);
        }



        public void OnUserRemoved(User user)
        {
            UserViewModel removedUserViewModel = _users.First(r => r.UserID == user.UserID);
            _users.Remove(removedUserViewModel);

        }
        
        public bool CanRemoveUser(object obj)
        {
            return true;
        }


        private void OnRolesLoaded()
        {
            _roles.Clear();

            foreach (Role r in _roleCollection.DataList)
            {
                RoleViewModel roleViewModel = new RoleViewModel(r);
                _roles.Add(roleViewModel);
            }

        }

        private void OnUserLoaded()
        {
            _users.Clear();

            foreach (User u in _userCollection.DataList)
            {
                RoleViewModel role = _roles.Where(r => r.RoleID == u.RoleID).FirstOrDefault();
                UserViewModel userViewModel = new UserViewModel(u, role);
                _users.Add(userViewModel);
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
                    _userCollection.UserRemoved -= OnUserRemoved;
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
