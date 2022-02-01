using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditDefectiveViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Defective _defective;

        private ProductViewModel _product;

        public ProductViewModel Product => _product;

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

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public EditDefectiveViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Defective defective, Action closeDialogCallback)
        {
            _defective = defective;
            _unitOfWork = unitOfWork;
            _navigationStore = navigationStore;
            _closeDialogCallback = closeDialogCallback;

            SetInitialValues(_defective);
            

            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SetInitialValues(Defective defective)
        {
            _product = new ProductViewModel(defective.Product);
            _quantity = defective.Quantity.ToString();
        }

        private void Submit()
        {
            ValidateAllProperties();
            int previousQuantity = _defective.Quantity;

            if (HasErrors)
            {
                return;
            }
            else if (Convert.ToInt32(_quantity) - previousQuantity  > _product.Product.ProductQuantity)
            {
                string quantity = _product.ProductQuantity;
                MessageBox.Show($"Quantity exceeded the number of stocks! {quantity}");
                return;
            }
            _defective.Quantity = Convert.ToInt32(_quantity);
            _defective.Product.ProductQuantity -= (_defective.Quantity - previousQuantity);
            _unitOfWork.Save();

            _closeDialogCallback();
        }



        private void Cancel()
        {
            _closeDialogCallback();
        }

        public static EditDefectiveViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Defective defective, Action closeDialogCallback)
        {
            EditDefectiveViewModel viewModel = new EditDefectiveViewModel(navigationStore, unitOfWork, defective, closeDialogCallback);
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
