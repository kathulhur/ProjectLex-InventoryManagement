using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateSupplierViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _fax;
        public string Fax
        {
            get { return _fax; }
            set
            {
                _fax = value;
                OnPropertyChanged(nameof(Fax));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _otherDetails;
        public string OtherDetails
        {
            get { return _otherDetails; }
            set
            {
                _otherDetails = value;
                OnPropertyChanged(nameof(OtherDetails));
            }
        }
       

        private readonly SupplierCollection _supplierCollection;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateSupplierViewModel(SupplierCollection supplierCollection, NavigationService<SupplierListViewModel> navigationService)
        {
            _supplierCollection = supplierCollection;
            SubmitCommand = new CreateSupplierCommand(this, supplierCollection, navigationService);
            CancelCommand = new NavigateCommand<SupplierListViewModel>(navigationService);
        }

        public static CreateSupplierViewModel LoadViewModel(SupplierCollection supplierCollection, NavigationService<SupplierListViewModel> navigationService)
        {
            return new CreateSupplierViewModel(supplierCollection, navigationService);
        }

        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
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
