using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ModifyUserViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private UserViewModel _userViewModel;

        private string _userID;
        public string UserID
        {
            get { return _userID; }
            set
            {
                _userID = value;
                OnPropertyChanged(nameof(UserID));
            }
        }

        private string _roleID => _role.RoleID;
        public string RoleID => _roleID;


        private RoleViewModel _role;
        public RoleViewModel Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        private string _userUsername;
        public string UserUsername
        {
            get { return _userUsername; }
            set
            {
                _userUsername = value;
                OnPropertyChanged(nameof(UserUsername));
            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }
       

        private readonly UserCollection _userCollection;
        private readonly RoleCollection _roleCollection;
        private readonly NavigationStore _navigationStore;

        private readonly ObservableCollection<RoleViewModel> _roles;
        public IEnumerable<RoleViewModel> Roles => _roles;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LoadRolesCommand { get; }

        public ModifyUserViewModel(NavigationStore navigationStore, UserCollection userCollection, RoleCollection roleCollection, UserViewModel userViewModel)
        {
            _navigationStore = navigationStore;
            _userCollection = userCollection;
            _roleCollection = roleCollection;

            _roles = new ObservableCollection<RoleViewModel>();
            _userViewModel = userViewModel;
            _role = userViewModel.Role;
            _userID = userViewModel.UserID;
            _userUsername = userViewModel.UserUsername;
            _userPassword = userViewModel.UserPassword;
            _userCollection = userCollection;
            SubmitCommand = new ModifyDataCommand<User>(userCollection, CreateUser, CanModifyUser);
            CancelCommand = new NavigateCommand(NavigateToUserList);
            LoadRolesCommand = new LoadDataCommand<Role>(_roleCollection, OnDataLoaded);
        }

        public static ModifyUserViewModel LoadViewModel(NavigationStore navigationStore, UserCollection userCollection, RoleCollection roleCollection, UserViewModel userViewModel)
        {
            ModifyUserViewModel viewModel = new ModifyUserViewModel(navigationStore, userCollection, roleCollection, userViewModel);
            viewModel.LoadRolesCommand.Execute(null);
            return viewModel;
            
        }

        public void NavigateToUserList(object obj)
        {
            _navigationStore.CurrentViewModel = UserListViewModel.LoadViewModel(_navigationStore, _userCollection, _roleCollection);
        }



        public void OnDataLoaded()
        {
            _roles.Clear();

            foreach (Role r in _roleCollection.DataList)
            {
                RoleViewModel roleViewModel = new RoleViewModel(r);
                _roles.Add(roleViewModel);
                if(_userViewModel.RoleID == roleViewModel.RoleID)
                {
                    Role = roleViewModel;
                }
            }
        }

        private User CreateUser(object obj)
        {
            return new User((ModifyUserViewModel)obj);
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


        public bool CanModifyUser(object obj)
        {
            return true;
        }
    }
}
