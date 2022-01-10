using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Modifiers;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using ProjectLex.InventoryManagement.Desktop.Services.Removers;
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
        private readonly ICreator<Category> _categoryCreator;
        private readonly IRemover<Category> _categoryRemover;
        private readonly IController<Category> _categoryController;
        private readonly IModifier<Category> _categoryModifier;
        private readonly CategoryCollection _categoryCollection;

        private readonly IProvider<Brand> _brandProvider;
        private readonly ICreator<Brand> _brandCreator;
        private readonly IController<Brand> _brandController;
        private readonly IRemover<Brand> _brandRemover;
        private readonly IModifier<Brand> _brandModifier;
        private readonly BrandCollection _brandCollection;

        private readonly IProvider<Role> _roleProvider;
        private readonly ICreator<Role> _roleCreator;
        private readonly IController<Role> _roleController;
        private readonly IRemover<Role> _roleRemover;
        private readonly IModifier<Role> _roleModifier;
        private readonly RoleCollection _roleCollection;

        private readonly IProvider<User> _userProvider;
        private readonly ICreator<User> _userCreator;
        private readonly IController<User> _userController;
        private readonly IRemover<User> _userRemover;
        private readonly IModifier<User> _userModifier;
        private readonly UserCollection _userCollection;

        private readonly IProvider<Store> _storeProvider;
        private readonly ICreator<Store> _storeCreator;
        private readonly IController<Store> _storeController;
        private readonly IRemover<Store> _storeRemover;
        private readonly IModifier<Store> _storeModifier;
        private readonly StoreCollection _storeCollection;

        private readonly IProvider<Models.Attribute> _attributeProvider;
        private readonly ICreator<Models.Attribute> _attributeCreator;
        private readonly IController<Models.Attribute> _attributeController;
        private readonly IRemover<Models.Attribute> _attributeRemover;
        private readonly IModifier<Models.Attribute> _attributeModifier;
        private readonly AttributeCollection _attributeCollection;

        //private readonly IProvider<Product> _productProvider;
        //private readonly ICreator<Product> _productCreator;
        //private readonly IController<Product> _productController;
        //private readonly ProductCollection _productCollection;
        private readonly ViewModelService _viewModelService;

        private readonly CollectionStore _collectionStore;

        private readonly ContextFactory _contextFactory;

        private readonly AuthenticationService _authenticationService;
        public App()
        {
            _navigationStore = new NavigationStore();

            _contextFactory = new ContextFactory();
            _categoryProvider = new CategoryProvider(_contextFactory);
            _categoryCreator = new CategoryCreator(_contextFactory);
            _categoryRemover = new CategoryRemover(_contextFactory);
            _categoryModifier = new CategoryModifier(_contextFactory);

            _categoryController = new DataController<Category>(_categoryProvider, _categoryCreator, _categoryRemover, _categoryModifier);
            _categoryCollection = new CategoryCollection(_categoryController);


            _brandProvider = new BrandProvider(_contextFactory);
            _brandCreator = new BrandCreator(_contextFactory);
            _brandRemover = new BrandRemover(_contextFactory);
            _brandModifier = new BrandModifier(_contextFactory);
            _brandController = new DataController<Brand>(_brandProvider, _brandCreator, _brandRemover, _brandModifier);
            _brandCollection = new BrandCollection(_brandController);

            _roleProvider = new RoleProvider(_contextFactory);
            _roleCreator = new RoleCreator(_contextFactory);
            _roleRemover = new RoleRemover(_contextFactory);
            _roleModifier = new RoleModifier(_contextFactory);
            _roleController = new DataController<Role>(_roleProvider, _roleCreator, _roleRemover, _roleModifier);
            _roleCollection = new RoleCollection(_roleController);

            _userProvider = new UserProvider(_contextFactory);
            _userCreator = new UserCreator(_contextFactory);
            _userRemover = new UserRemover(_contextFactory);
            _userModifier = new UserModifier(_contextFactory);
            _userController = new DataController<User>(_userProvider, _userCreator, _userRemover, _userModifier);
            _userCollection = new UserCollection(_userController);

            _authenticationService = AuthenticationService.LoadAuthenticationService(_userCollection);

            _storeProvider = new StoreProvider(_contextFactory);
            _storeCreator = new StoreCreator(_contextFactory);
            _storeRemover = new StoreRemover(_contextFactory);
            _storeModifier = new StoreModifier(_contextFactory);
            _storeController = new DataController<Store>(_storeProvider, _storeCreator, _storeRemover, _storeModifier);
            _storeCollection = new StoreCollection(_storeController);


            _attributeProvider = new AttributeProvider(_contextFactory);
            _attributeCreator = new AttributeCreator(_contextFactory);
            _attributeRemover = new AttributeRemover(_contextFactory);
            _attributeModifier = new AttributeModifier(_contextFactory);
            _attributeController = new DataController<Models.Attribute>(_attributeProvider, _attributeCreator, _attributeRemover, _attributeModifier);
            _attributeCollection = new AttributeCollection(_attributeController);
            //_productProvider = new ProductProvider(_contextFactory);
            //_productCreator = new ProductCreator(_contextFactory);
            //_productController = new ProductController(_productProvider, _productCreator);
            //_productCollection = new ProductCollection(_productController);

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
