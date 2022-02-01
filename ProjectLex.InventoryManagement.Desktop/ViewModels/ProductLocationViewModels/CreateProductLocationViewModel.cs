﻿using Microsoft.Toolkit.Mvvm.Input;
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

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class CreateProductLocationViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Location _location;


        private string _productID;
        [Required(ErrorMessage = "Product is Required")]
        public string ProductID
        {
            get { return _productID; }
            set
            {
                SetProperty(ref _productID, value, true);
            }
        }

        private string _productQuantity;

        [Required(ErrorMessage = "Quantity is Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Input")]
        public string ProductQuantity
        {
            get { return _productQuantity; }
            set { SetProperty(ref _productQuantity, value, true); }
        }



        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<ProductViewModel> _products;
        public IEnumerable<ProductViewModel> Products => _products;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadProductsCommand { get; }
        private Action _closeDialogCallback;

        public CreateProductLocationViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Location location, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _location = location;
            _closeDialogCallback = closeDialogCallback;
            _products = new ObservableCollection<ProductViewModel>();
            LoadProducts(_products);

            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Submit()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                return;
            }

            ProductLocation productLocation = _location.ProductLocations.SingleOrDefault(od => od.ProductID.ToString() == _productID);
            if (productLocation == null)
            {
                ProductLocation newProductLocation = new ProductLocation()
                {
                    LocationID = _location.LocationID,
                    ProductID = new Guid(_productID),
                    ProductQuantity = Convert.ToInt32(_productQuantity)
                };
                _location.ProductLocations.Add(newProductLocation);
                _products.SingleOrDefault(p => p.ProductID == _productID).Product.ProductQuantity += Convert.ToInt32(_productQuantity);
            }
            else
            {
                productLocation.ProductQuantity += Convert.ToInt32(_productQuantity);
            }
            _unitOfWork.Save();

            MessageBox.Show("Successful");
            _closeDialogCallback();
        }

        private void Cancel()
        {
            _closeDialogCallback();
        }

        private void LoadProducts(ObservableCollection<ProductViewModel> products)
        {
            products.Clear();
            foreach (Product p in _unitOfWork.ProductRepository.Get())
            {
                products.Add(new ProductViewModel(p));
            }
        }


        public static CreateProductLocationViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Location location, Action closeDialogCallback)
        {
            CreateProductLocationViewModel viewModel = new CreateProductLocationViewModel(navigationStore, unitOfWork, location, closeDialogCallback);
            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}