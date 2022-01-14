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
    public class CreateProductViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Product _product;

        public string StoreID
        {
            get 
            { 
                if(_product.StoreID == Guid.Empty)
                {
                    return null;
                }
                return _product.StoreID.ToString();
            }
            set
            {
                _product.StoreID = new Guid(value);
                OnPropertyChanged(nameof(StoreID));
            }

        }


        public string SupplierID
        {
            get
            {
                if (_product.SupplierID == Guid.Empty)
                {
                    return null;
                }
                return _product.SupplierID.ToString();
            }
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

        private readonly ObservableCollection<StoreViewModel> _stores;
        public IEnumerable<StoreViewModel> Stores => _stores;

        private readonly ObservableCollection<SupplierViewModel> _suppliers;
        public IEnumerable<SupplierViewModel> Suppliers => _suppliers;


        private readonly ObservableCollection<ProductCategory> _productCategories;



        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadSuppliersCommand { get; }
        private RelayCommand LoadProductCategoriesCommand { get; }
        private RelayCommand LoadStoresCommand { get; }

        public CreateProductViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _product = new Product()
            {
                ProductID = Guid.NewGuid()
            };

            _suppliers = new ObservableCollection<SupplierViewModel>();
            _stores = new ObservableCollection<StoreViewModel>();
            _productCategories = new ObservableCollection<ProductCategory>();


            SubmitCommand = new RelayCommand(CreateProduct);
            CancelCommand = new RelayCommand(NavigateToProductList);

            LoadSuppliersCommand = new RelayCommand(LoadSuppliers);
            LoadStoresCommand = new RelayCommand(LoadStores);
            LoadProductCategoriesCommand = new RelayCommand(LoadProductCategories);
        }

        private void CreateProduct()
        {
            _unitOfWork.ProductRepository.Insert(_product);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }


        private void NavigateToProductList()
        {
            _navigationStore.CurrentViewModel = ProductListViewModel.LoadViewModel(_navigationStore);
        }

        private void LoadSuppliers()
        {
            _suppliers.Clear();
            foreach (Supplier s in _unitOfWork.SupplierRepository.Get())
            {
                _suppliers.Add(new SupplierViewModel(s));
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

        private void LoadProductCategories()
        {
            _productCategories.Clear();
            foreach(ProductCategory pc in _unitOfWork.ProductCategoryRepository.Get(includeProperties: "Category"))
            {
                _productCategories.Add(pc);
            }

        }

        public static CreateProductViewModel LoadViewModel(NavigationStore navigationStore)
        {
            CreateProductViewModel viewModel = new CreateProductViewModel(navigationStore);
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

        public bool CanCreateProduct(object obj)
        {
            return true;
        }
    }
}
