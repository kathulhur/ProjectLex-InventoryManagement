using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        public ProductListViewHelper ProductListViewHelper { get; }


        private readonly ObservableCollection<ProductViewModel> _products;
        public ObservableCollection<ProductViewModel> Products { get; }

        public RelayCommand CreateProductCommand { get; }
        public RelayCommand LoadProductsCommand { get; }
        public RelayCommand<ProductViewModel> RemoveProductCommand { get; }
        public RelayCommand<ProductViewModel> EditProductCommand { get; }
        public RelayCommand NavigateToCreateProductCommand { get; }

        public ProductListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _products = new ObservableCollection<ProductViewModel>();
            Products = new ObservableCollection<ProductViewModel>();

            ProductListViewHelper = new ProductListViewHelper(_products, Products);

            LoadProductsCommand = new RelayCommand(LoadProducts);
            RemoveProductCommand = new RelayCommand<ProductViewModel>(RemoveProduct);
            EditProductCommand = new RelayCommand<ProductViewModel>(EditProduct);
            CreateProductCommand = new RelayCommand(CreateProduct);

        }


        private void RemoveProduct(ProductViewModel productViewModel)
        {
            var result = MessageBox.Show("Do you really want to remove this item?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _unitOfWork.ProductRepository.Delete(productViewModel.Product);
                _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.PRODUCTS, ActionType.DELETE, $"Product deleted; ProductID:{productViewModel.ProductID};"));
                _unitOfWork.Save();
                _products.Remove(productViewModel);
                ProductListViewHelper.RefreshCollection();
                MessageBox.Show("Successful");
            }

        }

        private void EditProduct(ProductViewModel productViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditProductViewModel.LoadViewModel(_navigationStore, _unitOfWork, productViewModel.Product, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CreateProduct()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateProductViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            LoadProductsCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadProducts()
        {
            _products.Clear();
            foreach (Product p in _unitOfWork.ProductRepository.Get(includeProperties: "Supplier,Category"))
            {
                _products.Add(new ProductViewModel(p));
            }
            ProductListViewHelper.RefreshCollection();
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
                    _dialogViewModel?.Dispose();
                    ProductListViewHelper.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
