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
    public class EditOrderViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Order _order;


        public string UserID
        {
            get { return _order.UserID.ToString(); }
            set
            {
                _order.UserID = new Guid(value);
                OnPropertyChanged(nameof(Staff));
            }
        }
        public string CustomerName
        {
            get { return _order.CustomerName; }
            set
            {
                _order.CustomerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }


        public string OrderTotal
        {
            get { return _order.OrderTotal.ToString(); }
            set
            {
                _order.OrderTotal = Convert.ToDecimal(value);
                OnPropertyChanged(nameof(OrderTotal));
            }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly OrderDetailListViewModel _orderDetailListViewModel;
        public OrderDetailListViewModel OrderDetailListViewModel => _orderDetailListViewModel;

        private readonly ObservableCollection<UserViewModel> _users;
        public IEnumerable<UserViewModel> Users => _users;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand LoadUsersCommand { get; }

        public EditOrderViewModel(NavigationStore navigationStore, Order order)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _order = order;
            _users = new ObservableCollection<UserViewModel>();
            _orderDetailListViewModel = OrderDetailListViewModel.LoadViewModel(_navigationStore, _order);
            SubmitCommand = new RelayCommand(EditOrder);
            CancelCommand = new RelayCommand(NavigateToOrderList);
            LoadUsersCommand = new RelayCommand(LoadUsers);
        }

        private void EditOrder()
        {
            _unitOfWork.OrderRepository.Update(_order);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }

        private void NavigateToOrderList()
        {
            _navigationStore.CurrentViewModel = OrderListViewModel.LoadViewModel(_navigationStore);
        }

        private void LoadUsers()
        {
            _users.Clear();
            foreach(Staff u in _unitOfWork.UserRepository.Get())
            {
                _users.Add(new UserViewModel(u));
            }
        }

        public static EditOrderViewModel LoadViewModel(NavigationStore navigationStore, Order order)
        {
            EditOrderViewModel viewModel = new EditOrderViewModel(navigationStore, order);
            viewModel.LoadUsersCommand.Execute(null);
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
                    _orderDetailListViewModel.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }


    }
}
