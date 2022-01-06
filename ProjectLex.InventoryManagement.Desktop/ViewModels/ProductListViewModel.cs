using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ProductListViewModel : ViewModelBase, IUpdatable<Product>, IUpdatable<Category>, IUpdatable<Supplier>
    {

        private bool _isDisposed = false;

        private readonly ObservableCollection<ProductViewModel> _products;
        public IEnumerable<ProductViewModel> Products => _products;


        private readonly ObservableCollection<CategoryViewModel> _categories;
        public IEnumerable<CategoryViewModel> Categories => _categories;


        private readonly ObservableCollection<SupplierViewModel> _suppliers;
        public IEnumerable<SupplierViewModel> Suppliers => _suppliers;


        private readonly ProductCollection _productCollection;
        private readonly CategoryCollection _categoryCollection;
        private readonly SupplierCollection _supplierCollection;

        public ICommand CreateProductCommand { get; }
        public ICommand LoadProductsCommand { get; }
        public ICommand LoadCategoriesCommand { get; }
        public ICommand LoadSuppliersCommand { get; }

        public ProductListViewModel
            (
                ProductCollection productCollection,
                CategoryCollection categoryCollection,
                SupplierCollection supplierCollection,
                NavigationService<CreateProductViewModel> navigationService
            )
        {
            _productCollection = productCollection;
            _categoryCollection = categoryCollection;
            _supplierCollection = supplierCollection;

            _products = new ObservableCollection<ProductViewModel>();

            CreateProductCommand = new NavigateCommand<CreateProductViewModel>(navigationService);
            LoadProductsCommand = new LoadProductsCommand(this, _productCollection);
            LoadCategoriesCommand = new LoadCategoriesCommand(this, _categoryCollection);
            LoadSuppliersCommand = new LoadSuppliersCommand(this, _supplierCollection);

            _productCollection.DataCreated += OnSupplierCreated;

        }

        private void OnSupplierCreated(Product product)
        {
            ProductViewModel newProductViewModel = new ProductViewModel(product);
            _products.Add(newProductViewModel);

        }

        public static ProductListViewModel LoadViewModel
            (
                ProductCollection productCollection,
                CategoryCollection categoryCollection,
                SupplierCollection supplierCollection,
                NavigationService<CreateProductViewModel> navigationService
            )
        {
            ProductListViewModel viewModel = new ProductListViewModel(
                    productCollection, 
                    categoryCollection,
                    supplierCollection,
                    navigationService
                );
            viewModel.LoadProductsCommand.Execute(null);
            viewModel.LoadCategoriesCommand.Execute(null);
            viewModel.LoadSuppliersCommand.Execute(null);

            return viewModel;
        }

        public void Update(IEnumerable<Product> products)
        {
            _products.Clear();

            foreach (Product p in products)
            {
                ProductViewModel newProductViewModel = new ProductViewModel(p);
                _products.Add(newProductViewModel);
            }
        }

        public void Update(IEnumerable<Category> categories)
        {
            _categories.Clear();

            foreach (Category c in categories)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel(c);
                _categories.Add(categoryViewModel);
            }

        }

        public void Update(IEnumerable<Supplier> suppliers)
        {

            _suppliers.Clear();

            foreach (Supplier s in suppliers)
            {
                SupplierViewModel supplierViewModel = new SupplierViewModel(s);
                _suppliers.Add(supplierViewModel);
            }
        }


        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
