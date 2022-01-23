using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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

        public string _productName;
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(4, ErrorMessage = "Name should be at least 4 characters")]
        [MaxLength(100, ErrorMessage = "Name should be at most 100 characters")]
        public string ProductName
        {
            get => _productName;
            set
            {
                SetProperty(ref _productName, value);
            }
        }

        public string _productSKU;
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(4, ErrorMessage = "Name should be at least 4 characters")]
        [MaxLength(100, ErrorMessage = "Name should be at most 100 characters")]
        public string ProductSKU
        {
            get => _productSKU;
            set
            {
                SetProperty(ref _productSKU, value);
            }
        }

        public string _productQuantity;
        [Required(ErrorMessage = "Quantity is Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Input")]
        public string ProductQuantity
        {
            get => _productQuantity;
            set
            {
                SetProperty(ref _productQuantity, value);
            }
        }


        public string _productUnit;
        [Required(ErrorMessage = "Unit is Required")]
        public string ProductUnit
        {
            get => _productUnit;
            set
            {
                SetProperty(ref _productUnit, value);
            }
        }

        public string _productPrice;
        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression("^[+-]?([0-9]+\\.?[0-9]*|\\.[0-9]+)$", ErrorMessage = "Invalid Input, only decimals are allowed")]
        public string ProductPrice
        {
            get => _productPrice;
            set
            {
                SetProperty(ref _productPrice, value);
            }
        }

        public string _productAvailability;
        [Required(ErrorMessage = "Availability is Required")]
        public string ProductAvailability
        {
            get => _productAvailability;
            set
            {
                SetProperty(ref _productAvailability, value);
            }
        }

        public string _supplierID;
        [Required(ErrorMessage = "Supplier is Required")]
        public string SupplierID
        {
            get => _supplierID;
            set
            {
                SetProperty(ref _supplierID, value);
            }
        }

        public string _categoryID;
        [Required(ErrorMessage = "Category is Required")]
        public string CategoryID
        {
            get => _categoryID;
            set
            {
                SetProperty(ref _categoryID, value);
            }
        }

        public string _locationID;
        [Required(ErrorMessage = "Location is Required")]
        public string LocationID
        {
            get => _locationID;
            set
            {
                SetProperty(ref _locationID, value);
            }
        }

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;
        private readonly Action _closeDialogCallback;

        private readonly ObservableCollection<CategoryViewModel> _categories;
        private readonly ObservableCollection<SupplierViewModel> _suppliers;
        private readonly ObservableCollection<LocationViewModel> _locations;

        public IEnumerable<SupplierViewModel> Suppliers => _suppliers;
        public IEnumerable<LocationViewModel> Locations => _locations;
        public IEnumerable<CategoryViewModel> Categories => _categories;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand LoadSuppliersCommand { get; }
        public RelayCommand LoadLocationsCommand { get; }
        public RelayCommand LoadCategoriesCommand { get; }


        public EditProductViewModel(NavigationStore navigationStore, Product product, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _closeDialogCallback = closeDialogCallback;
            _product = product;

            SetInitialValues(_product);

            _suppliers = new ObservableCollection<SupplierViewModel>();
            _categories = new ObservableCollection<CategoryViewModel>();
            _locations = new ObservableCollection<LocationViewModel>();

            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadSuppliersCommand = new RelayCommand(LoadSuppliers);
            LoadLocationsCommand = new RelayCommand(LoadLocations);
            LoadCategoriesCommand = new RelayCommand(LoadCategories);
        }

        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            _product.ProductName = _productName;
            _product.ProductSKU = _productSKU;
            _product.ProductQuantity = Convert.ToInt32(_productQuantity);
            _product.ProductUnit = _productUnit;
            _product.ProductPrice = Convert.ToDecimal(_productPrice);
            _product.ProductAvailability = _productAvailability;
            _product.CategoryID = new Guid(_categoryID);
            _product.SupplierID = new Guid(_supplierID);
            _product.LocationID = new Guid(_locationID);
            _product.Category = null;
            _product.Location = null;
            _product.Supplier = null;

            _unitOfWork.ProductRepository.Update(_product);
            _unitOfWork.Save();

            _closeDialogCallback();
        }

        private void Cancel()
        {
            _closeDialogCallback();
        }

        private void LoadCategories()
        {
            _categories.Clear();
            foreach (Category c in _unitOfWork.CategoryRepository.Get())
            {
                _categories.Add(new CategoryViewModel(c));
            }
        }

        private void LoadLocations()
        {
            _locations.Clear();
            foreach (Location l in _unitOfWork.LocationRepository.Get())
            {
                _locations.Add(new LocationViewModel(l));
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

        public static EditProductViewModel LoadViewModel(NavigationStore navigationStore, Product product, Action closeDialogCallback)
        {
            EditProductViewModel viewModel = new EditProductViewModel(navigationStore, product, closeDialogCallback);
            viewModel.LoadLocationsCommand.Execute(null);
            viewModel.LoadSuppliersCommand.Execute(null);
            viewModel.LoadCategoriesCommand.Execute(null);
            return viewModel;
            
        }

        private void SetInitialValues(Product product)
        {
            _productName = product.ProductName;
            _productSKU = product.ProductSKU;
            _productQuantity = product.ProductQuantity.ToString();
            _productUnit = product.ProductUnit.ToString();
            _productPrice = product.ProductPrice.ToString();
            _productAvailability = product.ProductAvailability;
            _supplierID = product.SupplierID.ToString();
            _locationID = product.LocationID.ToString();
            _categoryID = product.CategoryID.ToString();
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
