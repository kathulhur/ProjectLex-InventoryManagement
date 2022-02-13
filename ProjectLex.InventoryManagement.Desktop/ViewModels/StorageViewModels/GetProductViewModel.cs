using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class GetProductViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private ProductLocation _productLocation;

        private bool _isForOrder = false;
        public bool IsForOrder 
        {
            get => _isForOrder;
            set
            {
                SetProperty(ref _isForOrder, value);
            }
        }

        public ProductViewModel Product => new ProductViewModel(_productLocation.Product);

        public string _productQuantity;

        [Required(ErrorMessage = "Quantity is Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Input")]
        public string ProductQuantity
        {
            get { return _productQuantity; }
            set { SetProperty(ref _productQuantity, value, true); }
        }

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;


        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private Action _closeDialogCallback;

        public GetProductViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, ProductLocation productLocation, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _productLocation = productLocation;
            _closeDialogCallback = closeDialogCallback;

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
            else if (Convert.ToInt32(_productQuantity) > _productLocation.ProductQuantity)
            {
                MessageBox.Show($"Quantity Exceeded. There are only {_productLocation.ProductQuantity} units in stock.");
                return;
            }

            _productLocation.ProductQuantity -= Convert.ToInt32(_productQuantity);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.STORAGES, ActionType.GET, $"Product taken; ProductID: {_productLocation.ProductID}; Quantity: {_productQuantity};"));

            if(!_isForOrder)
            {
                _productLocation.Product.ProductQuantity -= Convert.ToInt32(_productQuantity);
            }

            _unitOfWork.Save();
            MessageBox.Show("Successful");
            _closeDialogCallback();
        }

        private void Cancel()
        {
            _closeDialogCallback();
        }


        public static GetProductViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, ProductLocation productLocation, Action closeDialogCallback)
        {
            GetProductViewModel viewModel = new GetProductViewModel(navigationStore, unitOfWork, productLocation, closeDialogCallback);
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
