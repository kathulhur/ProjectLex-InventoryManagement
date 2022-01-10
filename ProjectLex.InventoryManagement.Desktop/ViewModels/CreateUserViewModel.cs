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
    public class CreateUserViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


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

        private string _roleID => Role.RoleID;
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
       

        private readonly IDataCollection<User> _userCollection;
        private readonly RoleCollection _roleCollection;

        private readonly ObservableCollection<RoleViewModel> _roles;
        public IEnumerable<RoleViewModel> Roles => _roles;
        


        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        private ICommand LoadRolesCommand { get; }

        public CreateUserViewModel(IDataCollection<User> userCollection, RoleCollection roleCollection)
        {
            _userCollection = userCollection;
            _roleCollection = roleCollection;
            _roles = new ObservableCollection<RoleViewModel>();
            SubmitCommand = new CreateDataCommand<User>(userCollection, CreateUser, CanCreateUser);
            LoadRolesCommand = new LoadDataCommand<Role>(_roleCollection, OnDataLoaded);
        }
        public User CreateUser(object obj)
        {
            return new User((CreateUserViewModel)obj);
        }

        private void OnDataLoaded()
        {
            _roles.Clear();

            foreach (Role c in _roleCollection.DataList)
            {
                RoleViewModel roleViewModel = new RoleViewModel(c);
                _roles.Add(roleViewModel);
            }

        }


        public static CreateUserViewModel LoadViewModel(IDataCollection<User> userCollection, RoleCollection roleCollection)
        {
            CreateUserViewModel viewModel = new CreateUserViewModel(userCollection, roleCollection);
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
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }

        public bool CanCreateUser(object obj)
        {
            return true;
        }
    }
}
