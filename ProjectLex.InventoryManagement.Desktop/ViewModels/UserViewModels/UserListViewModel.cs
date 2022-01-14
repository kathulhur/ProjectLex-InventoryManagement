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
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class UserListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;



        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<UserViewModel> _users;
        public IEnumerable<UserViewModel> Users => _users;


        
        public RelayCommand ToCreateUserCommand { get; }
        public RelayCommand LoadUsersCommand { get; }
        public RelayCommand<UserViewModel> RemoveUserCommand { get; }
        public RelayCommand<UserViewModel> NavigateToModifyUserCommand { get; }
        public RelayCommand NavigateToCreateUserCommand { get; }

        public UserListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _users = new ObservableCollection<UserViewModel>();

            LoadUsersCommand = new RelayCommand(LoadUsers);
            RemoveUserCommand = new RelayCommand<UserViewModel>(RemoveUser, CanRemoveUser);
            NavigateToModifyUserCommand = new RelayCommand<UserViewModel>(NavigateToModifyUser);
            NavigateToCreateUserCommand = new RelayCommand(NavigateToCreateUser);

        }


        private void RemoveUser(UserViewModel userViewModel)
        {
            _unitOfWork.UserRepository.Delete(userViewModel.User);
            _unitOfWork.Save();
            _users.Remove(userViewModel);
            MessageBox.Show("Successful");
        }

        private bool CanRemoveUser(UserViewModel userViewModel)
        {
            return true;
        }

        private void NavigateToModifyUser(UserViewModel userViewModel)
        {
            _navigationStore.CurrentViewModel = EditUserViewModel.LoadViewModel(_navigationStore, userViewModel.User);
        }

        private void NavigateToCreateUser()
        {
            _navigationStore.CurrentViewModel = CreateUserViewModel.LoadViewModel(_navigationStore);
        }

        private void LoadUsers()
        {
            foreach(User u in _unitOfWork.UserRepository.Get(includeProperties: "Role"))
            {
                _users.Add(new UserViewModel(u));
            }
        }

        public static UserListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            UserListViewModel viewModel = new UserListViewModel(navigationStore);
            viewModel.LoadUsersCommand.Execute(null);

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
