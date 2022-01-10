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

        private readonly UserCollection _userCollection;
        private readonly RoleCollection _roleCollection;

        public ICommand ToCreateUserCommand { get; }
        public ICommand LoadUsersCommand { get; }
        public ICommand LoadRolesCommand { get; }
        public ICommand RemoveUserCommand { get; }
        public ICommand NavigateToModifyUserCommand { get; }

        public UserListViewModel
            (
                UserCollection userCollection, 
                RoleCollection roleCollection, 
                NavigationStore navigationStore
            )
        {
            _navigationStore = navigationStore;
            _userCollection = userCollection;
            _roleCollection = roleCollection;
            _userCollection.UserRemoved += OnUserRemoved;
            _users = new ObservableCollection<UserViewModel>();
            LoadUsersCommand = new LoadDataCommand<User>(_userCollection, OnUserLoaded);
            LoadRolesCommand = new LoadDataCommand<Role>(_roleCollection, OnUserLoaded);
            RemoveUserCommand = new RemoveDataCommand<User>(_userCollection, CreateUser, CanRemoveUser);
            NavigateToModifyUserCommand = new ModifyDataNavigateCommand(NavigateToModifyUser);

        }

        public User CreateUser(object obj)
        {
            return new User((UserViewModel)obj);
        }

        public void NavigateToModifyUser(object obj)
        {
            UserViewModel userViewModel = (UserViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyUserViewModel.LoadViewModel(_userCollection, _roleCollection, userViewModel);

        }

        public static UserListViewModel LoadViewModel
            (
                UserCollection userCollection, 
                RoleCollection roleCollection, 
                NavigationStore navigationStore
            )
        {
            UserListViewModel viewModel = new UserListViewModel(userCollection, roleCollection, navigationStore);
            viewModel.LoadUsersCommand.Execute(null);
            viewModel.LoadRolesCommand.Execute(null);

            return viewModel;
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

        private void OnUserLoaded()
        {
            _users.Clear();

            foreach (User u in _userCollection.DataList)
            {
                Role role = _roleCollection.DataList.Where(r => r.RoleID == u.RoleID).FirstOrDefault();
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
