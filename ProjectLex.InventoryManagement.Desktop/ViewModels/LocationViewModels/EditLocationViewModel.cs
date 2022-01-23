using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class EditLocationViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Location _location;

        public string _locationZone;
        [Required(ErrorMessage = "Zone is Required")]
        public string LocationZone
        {
            get => _locationZone;
            set
            {
                SetProperty(ref _locationZone, value);
            }

        }


        public string _locationAisle;
        [Required(ErrorMessage = "Aisle is Required")]
        public string LocationAisle
        {
            get => _locationAisle;
            set
            {
                SetProperty(ref _locationAisle, value);
            }
        }


        public string _locationBay;
        [Required(ErrorMessage = "Bay is Required")]
        public string LocationBay
        {
            get => _locationBay;
            set
            {
                SetProperty(ref _locationBay, value);
            }
        }


        public string _locationRow;

        [Required(ErrorMessage = "Row is Required")]
        public string LocationRow
        {
            get => _locationRow;
            set
            {
                SetProperty(ref _locationRow, value);
            }
        }


        public string _subLocation;

        [Required(ErrorMessage = "SubLocation is Required")]
        public string SubLocation
        {
            get => _subLocation;
            set
            {
                SetProperty(ref _subLocation, value);
            }
        }


        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;


        public event Action<Location> Submitted;

        protected virtual void OnSubmit(Location location)
        {
            Submitted?.Invoke(location);
        }

        public event Action Cancelled;

        protected virtual void OnCancel()
        {
            Cancelled?.Invoke();
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditLocationViewModel(NavigationStore navigationStore, Location location)
        {
            _unitOfWork = new UnitOfWork();
            _navigationStore = navigationStore;
            _location = location;

            LocationZone = _location.LocationZone;
            LocationAisle = _location.LocationAisle;
            LocationBay = _location.LocationBay;
            LocationRow = _location.LocationRow;
            SubLocation = _location.SubLocation;

            SubmitCommand = new RelayCommand(EditLocation);
            CancelCommand = new RelayCommand(CancelEditLocation);
        }

        public static EditLocationViewModel LoadViewModel(NavigationStore navigationStore, Location location)
        {
            return new EditLocationViewModel(navigationStore, location);
        }

        public void NavigateToLocationList()
        {
            _navigationStore.CurrentViewModel = LocationListViewModel.LoadViewModel(_navigationStore);
        }

        public void EditLocation()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            _location.LocationZone = this.LocationZone;
            _location.LocationAisle = this.LocationAisle;
            _location.LocationBay = this.LocationBay;
            _location.LocationRow = this.LocationRow;
            _location.SubLocation = this.SubLocation;

            _unitOfWork.LocationRepository.Update(_location);
            _unitOfWork.Save();

            MessageBox.Show("Success");
            _navigationStore.CurrentViewModel = LocationListViewModel.LoadViewModel(_navigationStore);
        }

        private void CancelEditLocation()
        {
            _navigationStore.CurrentViewModel = LocationListViewModel.LoadViewModel(_navigationStore);
        }

        private void ResetView()
        {
            LocationZone = "";
            LocationAisle = "";
            LocationBay = "";
            LocationRow = "";
            SubLocation = "";

            ClearErrors();
        }

        private bool CanEditLocation(object obj)
        {
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                    _unitOfWork.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
