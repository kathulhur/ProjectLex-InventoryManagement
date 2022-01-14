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
    public class ProductListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<ProductViewModel> _products;
        public IEnumerable<ProductViewModel> Products => _products;
        public RelayCommand ToCreateProductCommand { get; }
        public RelayCommand LoadProductsCommand { get; }
        public RelayCommand<ProductViewModel> RemoveProductCommand { get; }
        public RelayCommand<ProductViewModel> NavigateToModifyProductCommand { get; }
        public RelayCommand NavigateToCreateProductCommand { get; }

        public ProductListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _products = new ObservableCollection<ProductViewModel>();

            LoadProductsCommand = new RelayCommand(LoadProducts);

            RemoveProductCommand = new RelayCommand<ProductViewModel>(RemoveProduct);
            NavigateToModifyProductCommand = new RelayCommand<ProductViewModel>(NavigateToModifyProduct);
            NavigateToCreateProductCommand = new RelayCommand(NavigateToCreateProduct);

        }


        private void RemoveProduct(ProductViewModel productViewModel)
        {
            _unitOfWork.ProductRepository.Delete(productViewModel.Product);
            _unitOfWork.Save();
            _products.Remove(productViewModel);
            MessageBox.Show("Successful");

        }

        private void NavigateToModifyProduct(ProductViewModel productViewModel)
        {
            _navigationStore.CurrentViewModel = EditProductViewModel.LoadViewModel(_navigationStore, productViewModel.Product);
        }

        private void NavigateToCreateProduct()
        {
            _navigationStore.CurrentViewModel = CreateProductViewModel.LoadViewModel(_navigationStore);
        }

        private void LoadProducts()
        {
            _products.Clear();
            foreach (Product p in _unitOfWork.ProductRepository.Get(includeProperties: "Store,Supplier,ProductCategories.Category"))
            {
                _products.Add(new ProductViewModel(p));
            }
        }

        public static ProductListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            ProductListViewModel viewModel = new ProductListViewModel(navigationStore);
            viewModel.LoadProductsCommand.Execute(null);

            return viewModel;
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
