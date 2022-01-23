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

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class CreateOrderDetailViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Order _order;


        private string _productID;
        [Required(ErrorMessage = "Product is Required")]
        public string ProductID
        {
            get { return _productID; }
            set
            {
                SetProperty(ref _productID, value, true);
                if(!(GetErrors(nameof(OrderDetailQuantity)).Any()))
                {
                    var newAmount = (_products.Where(p => p.ProductID.ToString() == _productID).SingleOrDefault().Product.ProductPrice * Convert.ToInt32(OrderDetailQuantity)).ToString();
                    SetProperty(ref _orderDetailAmount, newAmount, true, nameof(OrderDetailAmount));
                }
            }
        }


        private string _orderDetailQuantity = "0";

        [Required(ErrorMessage = "Quantity is Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Input")]
        public string OrderDetailQuantity
        {
            get { return _orderDetailQuantity; }
            set
            {
                SetProperty(ref _orderDetailQuantity, value, true);
                
                int tempQuantity;
                if(int.TryParse(_orderDetailQuantity, out tempQuantity) && _productID!= null)
                {
                    var newAmount = (_products.Where(p => p.ProductID.ToString() == _productID).SingleOrDefault().Product.ProductPrice * Convert.ToInt32(OrderDetailQuantity)).ToString();
                    SetProperty(ref _orderDetailAmount, newAmount, true, nameof(OrderDetailAmount));
                } else
                {
                    SetProperty(ref _orderDetailAmount, "NaN", true, nameof(OrderDetailAmount));
                }
                
            }
        }

        private string _orderDetailAmount;

        [Required(ErrorMessage = "Amount is Required")]
        [RegularExpression("^[+-]?([0-9]+\\.?[0-9]*|\\.[0-9]+)$", ErrorMessage = "Invalid Input, only decimals are allowed")]
        public string OrderDetailAmount
        {
            get { return _orderDetailAmount; }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private static readonly ObservableCollection<ProductViewModel> _products;
        public static IEnumerable<ProductViewModel> Products => _products;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadProductsCommand { get; }
        private Action _closeDialogCallback;

        public CreateOrderDetailViewModel(NavigationStore navigationStore, Order order, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _order = order;
            _closeDialogCallback = closeDialogCallback;
            _products = new ObservableCollection<ProductViewModel>();


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadProductsCommand = new RelayCommand(LoadProducts);
        }

        private void Submit()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                return;
            }

            OrderDetail newOrderDetail = new OrderDetail()
            {
                OrderID = _order.OrderID,
                ProductID = new Guid(this.ProductID),
                OrderDetailQuantity = Convert.ToInt32(this.OrderDetailQuantity),
                OrderDetailAmount = Convert.ToDecimal(this.OrderDetailAmount)
            };

            _order.OrderDetails.Add(newOrderDetail);
            MessageBox.Show("Successful");
            _closeDialogCallback();
        }

        private void Cancel()
        {
            _closeDialogCallback();
        }

        private void LoadProducts()
        {
            _products.Clear();
            foreach(Product p in _unitOfWork.ProductRepository.Get())
            {
                _products.Add(new ProductViewModel(p));
            }
        }


        public static CreateOrderDetailViewModel LoadViewModel(NavigationStore navigationStore, Order order, Action closeDialogCallback)
        {
            CreateOrderDetailViewModel viewModel = new CreateOrderDetailViewModel(navigationStore, order, closeDialogCallback);
            viewModel.LoadProductsCommand.Execute(null);
            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
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
