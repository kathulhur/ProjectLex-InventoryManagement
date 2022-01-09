using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
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
    public class CreateBrandViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private string _brandID;
        public string BrandID
        {
            get { return _brandID; }
            set
            {
                _brandID = value;
                OnPropertyChanged(nameof(BrandID));
            }
        }

        private string _brandName;
        public string BrandName
        {
            get { return _brandName; }
            set
            {
                _brandName = value;
                OnPropertyChanged(nameof(BrandName));
            }
        }

        private string _brandStatus;
        public string BrandStatus
        {
            get { return _brandStatus; }
            set
            {
                _brandStatus = value;
                OnPropertyChanged(nameof(BrandStatus));
            }
        }
       

        private readonly IDataCollection<Brand> _brandCollection;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateBrandViewModel(IDataCollection<Brand> brandCollection)
        {
            _brandCollection = brandCollection;
            SubmitCommand = new CreateDataCommand<Brand>(brandCollection, CanCreateBrand);
        }

        public static CreateBrandViewModel LoadViewModel(IDataCollection<Brand> brandCollection)
        {
            return new CreateBrandViewModel(brandCollection);
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

        public bool CanCreateBrand(object obj)
        {
            return true;
        }
    }
}
