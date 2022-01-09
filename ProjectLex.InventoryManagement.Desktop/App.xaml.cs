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

        //private readonly IProvider<Product> _productProvider;
        //private readonly ICreator<Product> _productCreator;
        //private readonly IController<Product> _productController;
        //private readonly ProductCollection _productCollection;
        private readonly ViewModelService _viewModelService;

        private readonly CollectionStore _collectionStore;

        private readonly ContextFactory _contextFactory; 
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

            //_productProvider = new ProductProvider(_contextFactory);
            //_productCreator = new ProductCreator(_contextFactory);
            //_productController = new ProductController(_productProvider, _productCreator);
            //_productCollection = new ProductCollection(_productController);

            _collectionStore = new CollectionStore
            (
                _categoryCollection,
                _brandCollection
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
