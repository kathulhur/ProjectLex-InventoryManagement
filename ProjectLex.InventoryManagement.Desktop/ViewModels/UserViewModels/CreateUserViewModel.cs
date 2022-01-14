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
    public class CreateUserViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private User _user;


        public RoleViewModel Role
        {
            get { return _roles.Single(r => r.RoleID == _user.RoleID.ToString()); }
            set
            {
                _user.RoleID = new Guid(value.RoleID);
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
        private RelayCommand LoadRolesCommand { get; }

        public CreateUserViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _roles = new ObservableCollection<RoleViewModel>();

            _user = new User()
            {
                UserID = Guid.NewGuid()
            };

            SubmitCommand = new RelayCommand(CreateUser, CanCreateUser);
            CancelCommand = new RelayCommand(NavigateToUserList);
            LoadRolesCommand = new RelayCommand(LoadRoles);
        }

        private void CreateUser()
        {
            //Debug.WriteLine("Role ID" + _user.Role.RoleID);
            _unitOfWork.UserRepository.Insert(_user);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }
        private bool CanCreateUser()
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
            _user.RoleID = new Guid(_roles[0].RoleID);
        }

        public static CreateUserViewModel LoadViewModel(NavigationStore navigationStore)
        {
            CreateUserViewModel viewModel = new CreateUserViewModel(navigationStore);
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
