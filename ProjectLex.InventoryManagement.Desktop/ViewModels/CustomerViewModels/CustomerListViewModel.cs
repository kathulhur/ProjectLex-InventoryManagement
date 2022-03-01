using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class CustomerListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        public CustomerListViewHelper CustomerListViewHelper { get; }

        private readonly ObservableCollection<CustomerViewModel> _customers;
        public ObservableCollection<CustomerViewModel> Customers { get; }

        public RelayCommand CreateCustomerCommand { get; }
        public RelayCommand LoadCustomersCommand { get; }
        public RelayCommand<CustomerViewModel> RemoveCustomerCommand { get; }
        public RelayCommand<CustomerViewModel> EditCustomerCommand { get; }

        public CustomerListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _customers = new ObservableCollection<CustomerViewModel>();
            Customers = new ObservableCollection<CustomerViewModel>();

            CustomerListViewHelper = new CustomerListViewHelper(_customers, Customers);

            LoadCustomersCommand = new RelayCommand(LoadCustomers);
            RemoveCustomerCommand = new RelayCommand<CustomerViewModel>(RemoveCustomer);
            EditCustomerCommand = new RelayCommand<CustomerViewModel>(EditCustomer);
            CreateCustomerCommand = new RelayCommand(CreateCustomer);

        }


        private void RemoveCustomer(CustomerViewModel customerViewModel)
        {
            var result = MessageBox.Show("Do you really want to remove this item?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _unitOfWork.CustomerRepository.Delete(customerViewModel.Customer);
                _unitOfWork.Save();
                _customers.Remove(customerViewModel);
                CustomerListViewHelper.RefreshCollection();
                MessageBox.Show("Successful");
            }
        }

        private void EditCustomer(CustomerViewModel customerViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditCustomerViewModel.LoadViewModel(_navigationStore, _unitOfWork, customerViewModel.Customer, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CreateCustomer()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateCustomerViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            LoadCustomersCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }


        private void LoadCustomers()
        {
            _customers.Clear();
            foreach (Customer u in _unitOfWork.CustomerRepository.Get(includeProperties: "Staff"))
            {
                _customers.Add(new CustomerViewModel(u));
            }
            CustomerListViewHelper.RefreshCollection();
        }

        public static CustomerListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            CustomerListViewModel viewModel = new CustomerListViewModel(navigationStore);
            viewModel.LoadCustomersCommand.Execute(null);

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
                    _dialogViewModel?.Dispose();
                    CustomerListViewHelper?.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
