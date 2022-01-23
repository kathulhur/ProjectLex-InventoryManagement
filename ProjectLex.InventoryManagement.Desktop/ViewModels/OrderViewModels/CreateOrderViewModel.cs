using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateOrderViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Order _order;


        private string _customerID;

        [Required(ErrorMessage = "Customer is Required")]
        public string CustomerID
        {
            get => _customerID;
            set
            {
                SetProperty(ref _customerID, value);
            }
        }


        private string _orderTotal;


        [Required(ErrorMessage = "OrderTotal is Required")]
        public string OrderTotal
        {
            get { return _orderTotal; }
        }

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;


        private readonly ObservableCollection<OrderDetailViewModel> _orderDetails;
        public IEnumerable<OrderDetailViewModel> OrderDetails => _orderDetails;


        private readonly ObservableCollection<CustomerViewModel> _customers;
        public IEnumerable<CustomerViewModel> Customers => _customers;



        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand NavigateToCreateOrderDetailCommand { get; }
        private RelayCommand LoadCustomersCommand { get; }

        public RelayCommand LoadOrderDetailsCommand { get; }
        public RelayCommand<OrderDetailViewModel> RemoveOrderDetailCommand { get; }
        public RelayCommand<OrderDetailViewModel> EditOrderDetailCommand { get; }
        public RelayCommand CreateOrderDetailCommand { get; }

        public CreateOrderViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _customers = new ObservableCollection<CustomerViewModel>();

            _order = new Order
            {
                OrderID = Guid.NewGuid(),
                OrderDetails = new List<OrderDetail>()
            };

            _orderDetails = new ObservableCollection<OrderDetailViewModel>();
            
            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadCustomersCommand = new RelayCommand(LoadCustomers);
            CreateOrderDetailCommand = new RelayCommand(CreateOrderDetail);

            RemoveOrderDetailCommand = new RelayCommand<OrderDetailViewModel>(RemoveOrderDetail);
            EditOrderDetailCommand = new RelayCommand<OrderDetailViewModel>(EditOrderDetail);
            CreateOrderDetailCommand = new RelayCommand(CreateOrderDetail);
        }



        private void Submit()
        {
            ValidateAllProperties();

            if(HasErrors)
            {
                return;
            } else if (_order.OrderDetails.Count == 0)
            {
                MessageBox.Show("Product order list cannot be empty");
            }

            _order.CustomerID = new Guid(CustomerID);
            _order.OrderTotal = _orderDetails.Sum(od => Convert.ToDecimal(od.OrderDetailAmount));
            _order.OrderDate = DateTime.Now;

            _unitOfWork.OrderRepository.Insert(_order);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
            CancelCommand.Execute(null);
        }

        private void Cancel()
        {
            _navigationStore.CurrentViewModel = OrderListViewModel.LoadViewModel(_navigationStore);
        }

        private void RemoveOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            _orderDetails.Remove(orderDetailViewModel);
        }

        private void EditOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditOrderDetailViewModel.LoadViewModel(_navigationStore, orderDetailViewModel.OrderDetail, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            _orderDetails.Clear();
            foreach(OrderDetail od in _order.OrderDetails)
            {
                od.Product = _unitOfWork.ProductRepository.GetByID(od.ProductID);
                _orderDetails.Add(new OrderDetailViewModel(od));
            }

            _orderTotal = _order.OrderDetails.Sum(od => od.OrderDetailAmount).ToString();
            OnPropertyChanged(nameof(OrderTotal));


            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));


        }

        private void CreateOrderDetail()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateOrderDetailViewModel.LoadViewModel(_navigationStore, _order, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadCustomers()
        {
            _customers.Clear();
            foreach(Customer u in _unitOfWork.CustomerRepository.Get())
            {
                _customers.Add(new CustomerViewModel(u));
            }
        }


        public static CreateOrderViewModel LoadViewModel(NavigationStore navigationStore)
        {
            CreateOrderViewModel viewModel = new CreateOrderViewModel(navigationStore);
            viewModel.LoadCustomersCommand.Execute(null);
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
                    _dialogViewModel?.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
