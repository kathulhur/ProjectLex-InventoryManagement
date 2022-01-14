﻿using Microsoft.Toolkit.Mvvm.Input;
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
    public class OrderDetailListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly Order _order;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<OrderDetailViewModel> _orderDetails;
        public IEnumerable<OrderDetailViewModel> OrderDetails => _orderDetails;



        public RelayCommand ToCreateOrderDetailCommand { get; }
        public RelayCommand LoadOrderDetailsCommand { get; }
        public RelayCommand<OrderDetailViewModel> RemoveOrderDetailCommand { get; }
        public RelayCommand<OrderDetailViewModel> NavigateToEditOrderDetailCommand { get; }
        public RelayCommand NavigateToCreateOrderDetailCommand { get; }

        public OrderDetailListViewModel(NavigationStore navigationStore, Order order)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _order = order;

            _orderDetails = new ObservableCollection<OrderDetailViewModel>();

            LoadOrderDetailsCommand = new RelayCommand(LoadOrderDetails);
            RemoveOrderDetailCommand = new RelayCommand<OrderDetailViewModel>(RemoveOrderDetail);
            NavigateToEditOrderDetailCommand = new RelayCommand<OrderDetailViewModel>(NavigateToEditOrderDetail);
            NavigateToCreateOrderDetailCommand = new RelayCommand(NavigateToCreateOrderDetail);

        }

        private void RemoveOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            _unitOfWork.OrderDetailRepository.Delete(orderDetailViewModel.OrderDetail);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
            _orderDetails.Remove(orderDetailViewModel);
        }

        private void NavigateToEditOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            _navigationStore.CurrentViewModel = EditOrderDetailViewModel.LoadViewModel(_navigationStore, orderDetailViewModel.OrderDetail);
        }

        private void NavigateToCreateOrderDetail()
        {
            _navigationStore.CurrentViewModel = CreateOrderDetailViewModel.LoadViewModel(_navigationStore, _order);
        }

        private void LoadOrderDetails()
        {
            _orderDetails.Clear();
            foreach (OrderDetail od in _unitOfWork.OrderDetailRepository.Get(filter: o => o.OrderID == _order.OrderID, includeProperties: "Order,Product"))
            {
                _orderDetails.Add(new OrderDetailViewModel(od));
            }
        }

        public static OrderDetailListViewModel LoadViewModel(NavigationStore navigationStore, Order order)
        {
            OrderDetailListViewModel viewModel = new OrderDetailListViewModel(navigationStore, order);
            viewModel.LoadOrderDetailsCommand.Execute(null);

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
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}