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
    public class CreateDefectiveViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        private string _productID;

        [Required(ErrorMessage = "Product is Required")]
        public string ProductID
        {
            get => _productID;
            set
            {
                SetProperty(ref _productID, value, true);
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

        private readonly ObservableCollection<ProductViewModel> _products;
        public IEnumerable<ProductViewModel> Products => _products;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadProductsCommand { get; }

        public CreateDefectiveViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _closeDialogCallback = closeDialogCallback;
            _products = new ObservableCollection<ProductViewModel>();


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadProductsCommand = new RelayCommand(LoadProducts);
        }

        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            } else if(_productID != null && Convert.ToInt32(_quantity) > _products.SingleOrDefault(p => p.ProductID == _productID).Product.ProductQuantity)
            {
                string quantity = _products.SingleOrDefault(p => p.ProductID == _productID).ProductQuantity;
                MessageBox.Show($"Quantity exceeded the number of stocks! {quantity}");
                return;
            }

            Defective newDefective = new Defective()
            {
                DefectiveID = Guid.NewGuid(),
                ProductID = new Guid(_productID),
                Product = Products.SingleOrDefault(p => p.ProductID == _productID).Product,
                Quantity = Convert.ToInt32(_quantity),
                DateDeclared = DateTime.Now
            };

            _unitOfWork.DefectiveRepository.Insert(newDefective);
            newDefective.Product.ProductQuantity -= Convert.ToInt32(_quantity);
            _unitOfWork.Save();

            _closeDialogCallback();
        }



        private void Cancel()
        {
            _closeDialogCallback();
        }

        private void LoadProducts()
        {
            _products.Clear();
            foreach (Product r in _unitOfWork.ProductRepository.Get())
            {
                _products.Add(new ProductViewModel(r));
            }
        }

        public static CreateDefectiveViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            CreateDefectiveViewModel viewModel = new CreateDefectiveViewModel(navigationStore, unitOfWork, closeDialogCallback);
            viewModel.LoadProductsCommand.Execute(null);
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
