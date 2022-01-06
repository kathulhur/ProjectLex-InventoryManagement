using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
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
        private readonly CategoryCollection _categoryCollection;
        private readonly IProvider<Category> _categoryProvider;
        private readonly ICreator<Category> _categoryCreator;
        private readonly IController<Category> _categoryController;

        private readonly IProvider<Supplier> _supplierProvider;
        private readonly ICreator<Supplier> _supplierCreator;
        private readonly IController<Supplier> _supplierController;
        private readonly SupplierCollection _supplierCollection;

        private readonly IProvider<Product> _productProvider;
        private readonly ICreator<Product> _productCreator;
        private readonly IController<Product> _productController;
        private readonly ProductCollection _productCollection;
        private readonly ViewModelService _viewModelService;

        private readonly CollectionStore _collectionStore;

        private readonly ContextFactory _contextFactory; 
        public App()
        {
            _navigationStore = new NavigationStore();

            _contextFactory = new ContextFactory();
            _categoryProvider = new CategoryProvider(_contextFactory);
            _categoryCreator = new CategoryCreator(_contextFactory);
            _categoryController = new CategoryController(_categoryProvider, _categoryCreator);
            _categoryCollection = new CategoryCollection(_categoryController);


            _supplierProvider = new SupplierProvider(_contextFactory);
            _supplierCreator = new SupplierCreator(_contextFactory);
            _supplierController = new SupplierController(_supplierProvider, _supplierCreator);
            _supplierCollection = new SupplierCollection(_supplierController);

            _productProvider = new ProductProvider(_contextFactory);
            _productCreator = new ProductCreator(_contextFactory);
            _productController = new ProductController(_productProvider, _productCreator);
            _productCollection = new ProductCollection(_productController);
            _collectionStore = new CollectionStore
            (
                _categoryCollection,
                _productCollection,
                _supplierCollection
            );

            _viewModelService = new ViewModelService
                (
                    _navigationStore,
                    _collectionStore
                );
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new LoginWindow(_viewModelService);
            MainWindow.Show();

            base.OnStartup(e);
        }

        





    }
}
