using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class PrintInvoiceViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private OrderViewModel _order;
        public OrderViewModel Order => _order;

        private DateTime _currentDateTime = DateTime.Now;
        public DateTime CurrentDateTime => _currentDateTime;

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<OrderDetailViewModel> _orderDetails;
        public IEnumerable<OrderDetailViewModel> OrderDetails => _orderDetails;


        public RelayCommand LoadOrderDetailsCommand { get; }
        public RelayCommand<UserControl> PrintCommand { get; }

        public PrintInvoiceViewModel(NavigationStore navigationStore, Guid orderID)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _order = new OrderViewModel(_unitOfWork.OrderRepository.Get(filter: o => o.OrderID == orderID, includeProperties: "Customer,OrderDetails,OrderDetails.Product").SingleOrDefault());

            _orderDetails = new ObservableCollection<OrderDetailViewModel>();


            LoadOrderDetailsCommand = new RelayCommand(LoadOrderDetails);
            PrintCommand = new RelayCommand<UserControl>(Print);
        }

        private void Print(UserControl invoiceDocument)
        {

            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(invoiceDocument, "Invoice Printing.");
            }
        }



        private void LoadOrderDetails()
        {
            _orderDetails.Clear();
            foreach (OrderDetail s in _order.Order.OrderDetails)
            {
                _orderDetails.Add(new OrderDetailViewModel(s));
            }
        }

        public static PrintInvoiceViewModel LoadViewModel(NavigationStore navigationStore, Guid orderID)
        {
            PrintInvoiceViewModel viewModel = new PrintInvoiceViewModel(navigationStore, orderID);
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
