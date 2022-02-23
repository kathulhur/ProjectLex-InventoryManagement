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

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class StorageDetailViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private UnitOfWork _unitOfWork;

        private readonly Location _location;

        public LocationViewModel Location => new LocationViewModel(_location);

        private readonly NavigationStore _navigationStore;

        public StorageDetailListViewHelper StorageDetailListViewHelper { get; }

        private readonly ObservableCollection<ProductLocationViewModel> _productLocations;
        public ObservableCollection<ProductLocationViewModel> ProductLocations { get; }

        public RelayCommand LoadProductLocationsCommand { get; }
        public RelayCommand AddProductCommand { get; }
        public RelayCommand<ProductLocationViewModel> DisposeProductCommand { get; }
        public RelayCommand<ProductLocationViewModel> GetProductCommand { get; }
        public RelayCommand<ProductLocationViewModel> DeclareDefectiveProductCommand { get; }
        public RelayCommand<ProductLocationViewModel> MoveProductCommand { get; }

        public StorageDetailViewModel(NavigationStore navigationStore, Guid locationID)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _productLocations = new ObservableCollection<ProductLocationViewModel>();
            ProductLocations = new ObservableCollection<ProductLocationViewModel>();
            _location = _unitOfWork.LocationRepository.Get(l => l.LocationID == locationID, includeProperties: "ProductLocations,ProductLocations.Product").SingleOrDefault();

            StorageDetailListViewHelper = new StorageDetailListViewHelper(_productLocations, ProductLocations);

            LoadProductLocationsCommand = new RelayCommand(LoadProductLocations);
            AddProductCommand = new RelayCommand(AddProduct);
            DisposeProductCommand = new RelayCommand<ProductLocationViewModel>(DisposeProduct);
            GetProductCommand = new RelayCommand<ProductLocationViewModel>(GetProduct);
            DeclareDefectiveProductCommand = new RelayCommand<ProductLocationViewModel>(DeclareDefectiveProduct);
            MoveProductCommand = new RelayCommand<ProductLocationViewModel>(MoveProduct);
        }

        public void AddProduct()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateProductLocationViewModel.LoadViewModel(_navigationStore, _unitOfWork, _location, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        public void DisposeProduct(ProductLocationViewModel productLocationViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = DisposeProductViewModel.LoadViewModel(_navigationStore, _unitOfWork, productLocationViewModel.ProductLocation, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        public void MoveProduct(ProductLocationViewModel productLocationViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = MoveProductViewModel.LoadViewModel(_navigationStore, _unitOfWork, productLocationViewModel.ProductLocation, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        public void DeclareDefectiveProduct(ProductLocationViewModel productLocationViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = DeclareDefectiveProductViewModel.LoadViewModel(_navigationStore, _unitOfWork, productLocationViewModel.ProductLocation, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        public void GetProduct(ProductLocationViewModel productLocationViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = GetProductViewModel.LoadViewModel(_navigationStore, _unitOfWork, productLocationViewModel.ProductLocation, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        public void CloseDialogCallback()
        {
            LoadProductLocationsCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        public void LoadProductLocations()
        {
            _productLocations.Clear();
            foreach (ProductLocation pl in _location.ProductLocations)
            {
                _productLocations.Add(new ProductLocationViewModel(pl));
            }
            StorageDetailListViewHelper.RefreshCollection();
        }


        public static StorageDetailViewModel LoadViewModel(NavigationStore navigationStore, Guid locationID)
        {
            StorageDetailViewModel viewModel = new StorageDetailViewModel(navigationStore, locationID);
            viewModel.LoadProductLocationsCommand.Execute(null);
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
                    StorageDetailListViewHelper.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
