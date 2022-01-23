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
    class CreateLocationViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


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

        public CreateLocationViewModel(NavigationStore navigationStore)
        {
            _unitOfWork = new UnitOfWork();
            _navigationStore = navigationStore;
            SubmitCommand = new RelayCommand(CreateLocation);
            CancelCommand = new RelayCommand(CancelCreateLocation);
        }

        public static CreateLocationViewModel LoadViewModel(NavigationStore navigationStore)
        {
            return new CreateLocationViewModel(navigationStore);
        }

        public void NavigateToLocationList()
        {
            _navigationStore.CurrentViewModel = LocationListViewModel.LoadViewModel(_navigationStore);
        }

        public void CreateLocation()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Location location = new Location()
            {
                LocationID = Guid.NewGuid(),
                LocationZone = this.LocationZone,
                LocationAisle = this.LocationAisle,
                LocationBay = this.LocationBay,
                LocationRow = this.LocationRow,
                SubLocation = this.SubLocation
            };

            _unitOfWork.LocationRepository.Insert(location);
            _unitOfWork.Save();
            MessageBox.Show("Success");

            _navigationStore.CurrentViewModel = LocationListViewModel.LoadViewModel(_navigationStore);
        }

        private void CancelCreateLocation()
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

        private bool CanCreateLocation(object obj)
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
