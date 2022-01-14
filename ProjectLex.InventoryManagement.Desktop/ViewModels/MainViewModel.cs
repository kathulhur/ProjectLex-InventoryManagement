using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public RelayCommand NavigateToRoleListCommand { get; }
        public RelayCommand NavigateToCategoryListCommand { get; }
        public RelayCommand NavigateToStoreListCommand { get; }
        public RelayCommand NavigateToSupplierListCommand { get; }
        public RelayCommand NavigateToUserListCommand { get; }
        public RelayCommand NavigateToProductListCommand { get; }
        public RelayCommand NavigateToOrderListCommand { get; }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModel = RoleListViewModel.LoadViewModel(_navigationStore);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateToRoleListCommand = new RelayCommand(NavigateToRoleList);
            NavigateToCategoryListCommand = new RelayCommand(NavigateToCategoryList);
            NavigateToStoreListCommand = new RelayCommand(NavigateToStoreList);
            NavigateToSupplierListCommand = new RelayCommand(NavigateToSupplierList);
            NavigateToUserListCommand = new RelayCommand(NavigateToUserList);
            NavigateToProductListCommand = new RelayCommand(NavigateToProductList);
            NavigateToOrderListCommand = new RelayCommand(NavigateToOrderList);

        }

        public void NavigateToRoleList()
        {
            _navigationStore.CurrentViewModel = RoleListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToCategoryList()
        {
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToStoreList()
        {
            _navigationStore.CurrentViewModel = StoreListViewModel.LoadViewModel(_navigationStore);
        }


        public void NavigateToSupplierList()
        {
            _navigationStore.CurrentViewModel = SupplierListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToUserList()
        {
            _navigationStore.CurrentViewModel = UserListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToProductList()
        {
            _navigationStore.CurrentViewModel = ProductListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToOrderList()
        {
            _navigationStore.CurrentViewModel = OrderListViewModel.LoadViewModel(_navigationStore);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


    }
}
