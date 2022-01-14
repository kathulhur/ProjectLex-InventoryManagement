using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditProductViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Product _product;

        public string StoreID
        {
            get { return _product.StoreID.ToString(); }
            set
            {
                _product.StoreID = new Guid(value);
                OnPropertyChanged(nameof(StoreID));
            }

        }

        public string SupplierID
        {
            get { return _product.SupplierID.ToString(); }
            set
            {
                _product.SupplierID = new Guid(value);
                OnPropertyChanged(nameof(SupplierID));
            }

        }

        public string ProductName
        {
            get { return _product.ProductName; }
            set
            {
                _product.ProductName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public string ProductSKU
        {
            get { return _product.ProductSKU; }
            set
            {
                _product.ProductSKU = value;
                OnPropertyChanged(nameof(ProductSKU));
            }
        }

        public string ProductPrice
        {
            get { return _product.ProductPrice.ToString(); }
            set
            {
                _product.ProductPrice = Convert.ToDecimal(value);
                OnPropertyChanged(nameof(ProductPrice));
            }
        }

        public string ProductQuantity
        {
            get { return _product.ProductPrice.ToString(); }
            set
            {
                _product.ProductPrice = Convert.ToInt32(value);
                OnPropertyChanged(nameof(ProductQuantity));
            }
        }

        public string ProductAvailability
        {
            get { return _product.ProductAvailability; }
            set
            {
                _product.ProductAvailability = value;
                OnPropertyChanged(nameof(ProductAvailability));
            }
        }

        public IEnumerable<CategoryViewModel> Categories
        {
            get { return _productCategories.Select(pc => new CategoryViewModel(pc.Category)); }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<ProductCategory> _productCategories;
        private readonly ObservableCollection<SupplierViewModel> _suppliers;
        private readonly ObservableCollection<StoreViewModel> _stores;

        public IEnumerable<SupplierViewModel> Suppliers => _suppliers;
        public IEnumerable<StoreViewModel> Stores => _stores;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand LoadSuppliersCommand { get; }
        public RelayCommand LoadStoresCommand { get; }
        public RelayCommand LoadProductCategoriesCommand { get; }


        public EditProductViewModel(NavigationStore navigationStore, Product product)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _product = product;

            _suppliers = new ObservableCollection<SupplierViewModel>();
            _productCategories = new ObservableCollection<ProductCategory>();
            _stores = new ObservableCollection<StoreViewModel>();

            SubmitCommand = new RelayCommand(EditProduct);
            CancelCommand = new RelayCommand(NavigateToProductList);
            LoadSuppliersCommand = new RelayCommand(LoadSuppliers);
            LoadStoresCommand = new RelayCommand(LoadStores);
            LoadProductCategoriesCommand = new RelayCommand(LoadProductCategories);
        }

        private void EditProduct()
        {
            _unitOfWork.ProductRepository.Update(_product);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }

        private void NavigateToProductList()
        {
            _navigationStore.CurrentViewModel = ProductListViewModel.LoadViewModel(_navigationStore);
        }

        private void LoadProductCategories()
        {
            _productCategories.Clear();
            foreach (ProductCategory pc in _unitOfWork.ProductCategoryRepository.Get())
            {
                _productCategories.Add(pc);
            }
        }

        private void LoadStores()
        {
            _stores.Clear();
            foreach (Store s in _unitOfWork.StoreRepository.Get())
            {
                _stores.Add(new StoreViewModel(s));
            }
        }

        private void LoadSuppliers()
        {
            _suppliers.Clear();
            foreach(Supplier s in _unitOfWork.SupplierRepository.Get())
            {
                _suppliers.Add(new SupplierViewModel(s));
            }
        }

        public static EditProductViewModel LoadViewModel(NavigationStore navigationStore, Product product)
        {
            EditProductViewModel viewModel = new EditProductViewModel(navigationStore, product);
            viewModel.LoadSuppliersCommand.Execute(null);
            viewModel.LoadStoresCommand.Execute(null);
            viewModel.LoadProductCategoriesCommand.Execute(null);
            return viewModel;
            
        }


        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                    _unitOfWork.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }


    }
}
