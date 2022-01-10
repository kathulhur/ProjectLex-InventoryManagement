using MaterialDesignThemes.Wpf;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectLex.InventoryManagement.Desktop
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ViewModelService _viewModelService;
        private readonly AuthenticationService _authenticationService;
        public LoginWindow(AuthenticationService authenticationService, ViewModelService viewModelService)
        {
            _authenticationService = authenticationService;
            _viewModelService = viewModelService;
            InitializeComponent();
        }

        public string text { get; set; }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();


        private void toggleTheme(object sender, RoutedEventArgs e)
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

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void LoginUser(object sender, RoutedEventArgs e)
        {
            User user = _authenticationService.Authenticate(txtUsername.Text, txtPassword.Password);
            if(user == null)
            {
                MessageBox.Show("Username or Password is incorrect");
            } else
            {
                Application.Current.MainWindow = new MainWindow()
                {
                    DataContext = new MainViewModel(user,_viewModelService)
                };

                Application.Current.MainWindow.Show();
                this.Close();
            }

        }
    }
}
