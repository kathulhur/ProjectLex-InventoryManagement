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
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class SupplierListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;
        
        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        private readonly ObservableCollection<SupplierViewModel> _suppliers;
        public ObservableCollection<SupplierViewModel> Suppliers { get; }

        public SupplierListViewHelper SupplierListViewHelper { get; }

        public ICommand ToCreateSupplierCommand { get; }
        public RelayCommand LoadSuppliersCommand { get; }
        public RelayCommand<SupplierViewModel> RemoveSupplierCommand { get; }
        public RelayCommand<SupplierViewModel> EditSupplierCommand { get; }
        public RelayCommand CreateSupplierCommand { get; }

        public SupplierListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _suppliers = new ObservableCollection<SupplierViewModel>();
            Suppliers = new ObservableCollection<SupplierViewModel>();
            SupplierListViewHelper = new SupplierListViewHelper(_suppliers, Suppliers);

            LoadSuppliersCommand = new RelayCommand(LoadSuppliers);
            RemoveSupplierCommand = new RelayCommand<SupplierViewModel>(RemoveSupplier);
            EditSupplierCommand = new RelayCommand<SupplierViewModel>(EditSupplier);
            CreateSupplierCommand = new RelayCommand(CreateSupplier);
        }

        private void RemoveSupplier(SupplierViewModel supplierViewModel)
        {
            var result = MessageBox.Show("Do you really want to remove this item?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _unitOfWork.SupplierRepository.Delete(supplierViewModel.Supplier);
                _unitOfWork.Save();
                _suppliers.Remove(supplierViewModel);
                SupplierListViewHelper.RefreshCollection();
                MessageBox.Show("Successful");
            }
        }


        private void CreateSupplier()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateSupplierViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }


        private void EditSupplier(SupplierViewModel supplierViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditSupplierViewModel.LoadViewModel(_navigationStore, _unitOfWork, supplierViewModel.Supplier, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }


        private void CloseDialogCallback()
        {
            LoadSuppliersCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadSuppliers()
        {
            _suppliers.Clear();
            foreach(Supplier s in _unitOfWork.SupplierRepository.Get())
            {
                _suppliers.Add(new SupplierViewModel(s));
            }
            SupplierListViewHelper.RefreshCollection();
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
                    _dialogViewModel?.Dispose();
                    SupplierListViewHelper.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
