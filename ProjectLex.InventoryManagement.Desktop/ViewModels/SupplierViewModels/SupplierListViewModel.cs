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
    public class SupplierListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;


        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<SupplierViewModel> _suppliers;
        public IEnumerable<SupplierViewModel> Suppliers => _suppliers;


        public ICommand ToCreateSupplierCommand { get; }
        public RelayCommand LoadSuppliersCommand { get; }
        public RelayCommand<SupplierViewModel> RemoveSupplierCommand { get; }
        public RelayCommand<SupplierViewModel> NavigateToEditSupplierCommand { get; }
        public RelayCommand NavigateToCreateSupplierCommand { get; }

        public SupplierListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _suppliers = new ObservableCollection<SupplierViewModel>();

            LoadSuppliersCommand = new RelayCommand(LoadSuppliers);
            RemoveSupplierCommand = new RelayCommand<SupplierViewModel>(RemoveSupplier, CanRemoveSupplier);
            NavigateToEditSupplierCommand = new RelayCommand<SupplierViewModel>(NavigateToEditSupplier);
            NavigateToCreateSupplierCommand = new RelayCommand(NavigateToCreateSupplier);
        }

        private void RemoveSupplier(SupplierViewModel supplierViewModel)
        {
            _unitOfWork.SupplierRepository.Delete(supplierViewModel.Supplier);
            _unitOfWork.Save();
            _suppliers.Remove(supplierViewModel);
            MessageBox.Show("Successful");
        }

        private bool CanRemoveSupplier(SupplierViewModel supplierViweModel)
        {
            return true;
        }

        private void NavigateToCreateSupplier()
        {
            _navigationStore.CurrentViewModel = CreateSupplierViewModel.LoadViewModel(_navigationStore);
        }


        private void NavigateToEditSupplier(SupplierViewModel supplierViewModel)
        {
            _navigationStore.CurrentViewModel = EditSupplierViewModel.LoadViewModel(_navigationStore, supplierViewModel.Supplier);
        }


        private void LoadSuppliers()
        {
            _suppliers.Clear();
            foreach(Supplier s in _unitOfWork.SupplierRepository.Get())
            {
                _suppliers.Add(new SupplierViewModel(s));
            }
        }

        public static SupplierListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            SupplierListViewModel viewModel = new SupplierListViewModel(navigationStore);
            viewModel.LoadSuppliersCommand.Execute(null);
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
