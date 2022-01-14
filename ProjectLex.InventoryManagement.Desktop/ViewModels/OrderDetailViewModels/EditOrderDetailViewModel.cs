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

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class EditOrderDetailViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private OrderDetail _orderDetail;


        private int _productIndex;
        public int ProductIndex
        {
            get { return _productIndex; }
            set
            {
                _productIndex = value;
                _orderDetail.ProductID = new Guid(_products[_productIndex].ProductID);
                _orderDetail.StoreID = new Guid(_products[_productIndex].StoreID);

                _orderDetail.OrderDetailAmount = _products[_productIndex].Product.ProductPrice * _orderDetail.OrderDetailQuantity;
                OnPropertyChanged(nameof(ProductIndex));
                OnPropertyChanged(nameof(OrderDetailAmount));
            }
        }


        public string OrderDetailQuantity
        {
            get { return _orderDetail.OrderDetailQuantity.ToString(); }
            set
            {
                _orderDetail.OrderDetailQuantity = Convert.ToInt32(value);
                _orderDetail.OrderDetailAmount = _products[_productIndex].Product.ProductPrice * _orderDetail.OrderDetailQuantity;
                OnPropertyChanged(nameof(OrderDetailQuantity));
                OnPropertyChanged(nameof(OrderDetailAmount));
            }
        }

        public string OrderDetailAmount
        {
            get { return _orderDetail.OrderDetailAmount.ToString(); }
        }

        private decimal CalculateAmount(decimal amount, int quantity)
        {
            return amount * quantity;
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<ProductViewModel> _products;
        public IEnumerable<ProductViewModel> Products => _products;



        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadProductsCommand { get; }

        public EditOrderDetailViewModel(NavigationStore navigationStore, OrderDetail orderDetail)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _orderDetail = orderDetail;
            _products = new ObservableCollection<ProductViewModel>();


            SubmitCommand = new RelayCommand(EditOrderDetail);
            CancelCommand = new RelayCommand(NavigateToEditOrderList);
            LoadProductsCommand = new RelayCommand(LoadProducts);
        }

        private void EditOrderDetail()
        {
            _unitOfWork.OrderDetailRepository.Update(_orderDetail);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }

        private void NavigateToEditOrderList()
        {
            _navigationStore.CurrentViewModel = EditOrderViewModel.LoadViewModel(_navigationStore, _orderDetail.Order);
        }

        private void LoadProducts()
        {
            _products.Clear();
            foreach(Product p in _unitOfWork.ProductRepository.Get())
            {
                _products.Add(new ProductViewModel(p));
            }
        }


        public static EditOrderDetailViewModel LoadViewModel(NavigationStore navigationStore, OrderDetail orderDetail)
        {
            EditOrderDetailViewModel viewModel = new EditOrderDetailViewModel(navigationStore, orderDetail);
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
