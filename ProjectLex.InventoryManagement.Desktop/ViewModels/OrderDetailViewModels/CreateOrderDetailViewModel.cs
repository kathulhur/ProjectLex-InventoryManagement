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
                if(_productID != null && !GetErrors(nameof(OrderDetailQuantity)).Any())
                {
                    _product = Products.Where(p => p.ProductID == _productID).SingleOrDefault();

                    string newAmount = (_product.Product.ProductPrice * Convert.ToInt32(OrderDetailQuantity)).ToString();
                    SetProperty(ref _orderDetailAmount, newAmount, true, nameof(OrderDetailAmount));
                }
            }
        }

        private ProductViewModel _product;
        public ProductViewModel Product
        {
            get { return _product; }
            set
            {
                SetProperty(ref _product, value);
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
                    var newAmount = (_product.Product.ProductPrice * Convert.ToInt32(_orderDetailQuantity)).ToString();
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

        private readonly ObservableCollection<ProductViewModel> _products;
        public IEnumerable<ProductViewModel> Products => _products;

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

            OrderDetail storedOrderDetail = _order.OrderDetails.SingleOrDefault(od => od.ProductID == this._product.Product.ProductID);
            if (storedOrderDetail == null)
            {
                OrderDetail newOrderDetail = new OrderDetail()
                {
                    OrderID = _order.OrderID,
                    Product = _product.Product,
                    ProductID = new Guid(this._productID),
                    OrderDetailQuantity = Convert.ToInt32(this._orderDetailQuantity),
                    OrderDetailAmount = Convert.ToDecimal(this._orderDetailAmount)
                };
                

                Product storedProduct = _unitOfWork.ProductRepository.GetByID(newOrderDetail.ProductID);
                if(Convert.ToInt32(_orderDetailQuantity) > storedProduct.ProductQuantity)
                {
                    MessageBox.Show("Insufficient stocks!");
                    return;
                } else
                {
                    _order.OrderDetails.Add(newOrderDetail);
                    storedProduct.ProductQuantity -= newOrderDetail.OrderDetailQuantity;
                }
            } else
            {
                if(Convert.ToInt32(_orderDetailQuantity) > storedOrderDetail.Product.ProductQuantity)
                {
                    MessageBox.Show("Insufficient stocks!");
                    return;
                } else
                {
                    storedOrderDetail.OrderDetailQuantity += Convert.ToInt32(_orderDetailQuantity);
                    storedOrderDetail.OrderDetailAmount = storedOrderDetail.OrderDetailQuantity * storedOrderDetail.Product.ProductPrice;
                    storedOrderDetail.Product.ProductQuantity -= Convert.ToInt32(_orderDetailQuantity);
                }
            }

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
            foreach(Product p in _unitOfWork.ProductRepository.Get())
            {
                products.Add(new ProductViewModel(p));
            }
        }


        public static CreateOrderDetailViewModel LoadViewModel(NavigationStore navigationStore, Order order, Action closeDialogCallback)
        {
            CreateOrderDetailViewModel viewModel = new CreateOrderDetailViewModel(navigationStore, order, closeDialogCallback);
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
