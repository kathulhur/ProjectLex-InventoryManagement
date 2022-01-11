using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
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
        private readonly IProvider<Category> _categoryProvider;
        private readonly CategoryCollection _categoryCollection;

        private readonly IProvider<Brand> _brandProvider;
        private readonly BrandCollection _brandCollection;

        private readonly IProvider<Role> _roleProvider;
        private readonly RoleCollection _roleCollection;

        private readonly IProvider<User> _userProvider;
        private readonly UserCollection _userCollection;

        private readonly IProvider<Store> _storeProvider;
        private readonly StoreCollection _storeCollection;

        private readonly IProvider<Models.Attribute> _attributeProvider;
        private readonly AttributeCollection _attributeCollection;


        private readonly ViewModelService _viewModelService;

        private readonly CollectionStore _collectionStore;

        private readonly ContextFactory _contextFactory;

        private readonly AuthenticationService _authenticationService;
        public App()
        {
            _navigationStore = new NavigationStore();

            _contextFactory = new ContextFactory();
            _categoryProvider = new CategoryProvider(_contextFactory);
            _categoryCollection = new CategoryCollection(_categoryProvider);


            _brandProvider = new BrandProvider(_contextFactory);
            _brandCollection = new BrandCollection(_brandProvider);

            _roleProvider = new RoleProvider(_contextFactory);
            _roleCollection = new RoleCollection(_roleProvider);

            _userProvider = new UserProvider(_contextFactory);
            _userCollection = new UserCollection(_userProvider);

            _authenticationService = AuthenticationService.LoadAuthenticationService(_userCollection);

            _storeProvider = new StoreProvider(_contextFactory);
            _storeCollection = new StoreCollection(_storeProvider);


            _attributeProvider = new AttributeProvider(_contextFactory);
            _attributeCollection = new AttributeCollection(_attributeProvider);

            _collectionStore = new CollectionStore
            (
                _categoryCollection,
                _brandCollection,
                _roleCollection,
                _userCollection,
                _storeCollection,
                _attributeCollection
            );

            _viewModelService = new ViewModelService
                (
                    _navigationStore,
                    _collectionStore
                );
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new LoginWindow(_authenticationService, _viewModelService);
            MainWindow.Show();

            base.OnStartup(e);
        }

        





    }
}
