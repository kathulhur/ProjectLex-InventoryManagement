using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class MoveProductViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private ProductLocation _productLocation;

        public ProductViewModel Product => new ProductViewModel(_productLocation.Product);

        private string _oldLocationID;

        private string _locationID;

        [Required(ErrorMessage = "Location is Required")]
        public string LocationID
        {
            get { return _locationID; }
            set 
            {
                SetProperty(ref _locationID, value, true);
            }
        }

        private string _quantity;

        [Required(ErrorMessage = "Quantity is Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Quantity should only contain numbers")]
        public string Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value, true);
            }
        }

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;
        private readonly Action _closeDialogCallback;

        private readonly ObservableCollection<LocationViewModel> _locations;
        public IEnumerable<LocationViewModel> Locations => _locations;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadLocationsCommand { get; }

        public MoveProductViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, ProductLocation productLocation, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _closeDialogCallback = closeDialogCallback;
            _productLocation = productLocation;
            _locations = new ObservableCollection<LocationViewModel>();

            _oldLocationID = _productLocation.LocationID.ToString();


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadLocationsCommand = new RelayCommand(LoadLocations);
        }

        private void SetInitialValues(ProductLocation productLocation)
        {
            _locationID = _productLocation.LocationID.ToString();
            _quantity = "0";
        }

        private void Submit()
        {
            ValidateAllProperties();

            int tmpQuantity;

            if (HasErrors)
            {
                return;
            }
            else if (Convert.ToInt32(_quantity) < 1)
            {
                MessageBox.Show("Only quantities greater than 0 is allowed");
                return;
            }
            else if (!int.TryParse(_quantity, out tmpQuantity))
            {
                MessageBox.Show("Invalid Input");
                return;
            }
            else if (tmpQuantity > _productLocation.ProductQuantity)
            {
                MessageBox.Show($"Quantity exceeded the number of stocks! {_productLocation.ProductQuantity}");
                return;
            }


            ProductLocation storedProductLocation = _unitOfWork.ProductLocationRepository.Get(pl => pl.ProductID == _productLocation.ProductID && pl.LocationID == new Guid(_locationID)).SingleOrDefault();
            if (storedProductLocation == null) // if the product does not exist, create a new instance
            {
                ProductLocation newProductLocation = new ProductLocation()
                {
                    ProductID = _productLocation.ProductID,
                    LocationID = new Guid(_locationID),
                    ProductQuantity = Convert.ToInt32(_quantity)
                };
                _unitOfWork.ProductLocationRepository.Insert(newProductLocation);

            } else // if a product is already existing, add the quantity
            {
                storedProductLocation.ProductQuantity += Convert.ToInt32(_quantity);
            }

            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.STORAGES, ActionType.MOVE, $"Product moved; ProductID: {_productLocation.ProductID}, From LocationID {_oldLocationID} to {_locationID}; Quantity: {_quantity};"));
            _productLocation.ProductQuantity -= Convert.ToInt32(_quantity);
            _unitOfWork.Save();

            _closeDialogCallback();
        }



        private void Cancel()
        {
            _closeDialogCallback();
        }

        private void LoadLocations()
        {
            _locations.Clear();
            foreach (Location l in _unitOfWork.LocationRepository.Get(l => l.LocationID != _productLocation.LocationID))
            {
                _locations.Add(new LocationViewModel(l));
            }
        }

        public static MoveProductViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, ProductLocation productLocation, Action closeDialogCallback)
        {
            MoveProductViewModel viewModel = new MoveProductViewModel(navigationStore, unitOfWork, productLocation, closeDialogCallback);
            viewModel.LoadLocationsCommand.Execute(null);
            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
