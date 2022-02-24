using MaterialDesignThemes.Wpf;
using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class LoginViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        public string text { get; set; }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();


        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }


        private readonly UnitOfWork _unitOfWork;

        private readonly NavigationStore _navigationStore;
        private readonly AuthenticationStore _authenticationStore;
        public RelayCommand ToggleThemeCommand { get; }
        public RelayCommand HelpCommand { get; }
        public RelayCommand ExitAppCommand { get; }
        public RelayCommand<object> LoginUserCommand { get; }

        public LoginViewModel(NavigationStore navigationStore, AuthenticationStore authenticationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _authenticationStore = authenticationStore;
            ToggleThemeCommand = new RelayCommand(ToggleTheme);
            HelpCommand = new RelayCommand(Help);
            LoginUserCommand = new RelayCommand<object>(LoginUser);
            ExitAppCommand = new RelayCommand(ExitApp);
        }

        public void Help()
        {
            MessageBox.Show(Application.Current.MainWindow,"Contact Us\n\n" +
                "Email: support.projectlexlabs@outlook.com\n" +
                "Facebook Page: ProjectLex Software Lab", "Help");
        }

        public void ToggleTheme()
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
        }

        public void ExitApp()
        {
            Application.Current.Shutdown();

        }


        private void LoginUser(object obj)
        {
            PasswordBox passwordBox = obj as PasswordBox;

            string username = _username;
            string password = passwordBox.Password;


            Staff storedStaff = _unitOfWork.StaffRepository.Get(s => s.StaffUsername == username && s.StaffPassword == password, includeProperties: "Role").SingleOrDefault();

            if(storedStaff == null)
            {
                MessageBox.Show("Invalid Credentials. Please try again.");
                return;
            }

            _authenticationStore.CurrentStaff = storedStaff;
            _authenticationStore.IsLoggedIn = true;

            _navigationStore.CurrentViewModel = DashboardViewModel.LoadViewModel(_navigationStore);
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
