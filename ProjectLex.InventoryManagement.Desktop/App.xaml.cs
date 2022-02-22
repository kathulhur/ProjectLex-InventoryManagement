using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly NavigationStore _navigationStore;
        private readonly AuthenticationStore _authenticationStore;
        public App()
        {
            SplashScreen splashScreen = new SplashScreen(@"./Assets/SplashScreen.png");
            splashScreen.Show(true);
            _navigationStore = new NavigationStore();
            _authenticationStore = new AuthenticationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore,_authenticationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            


            return services.BuildServiceProvider();
        }

        





    }
}
