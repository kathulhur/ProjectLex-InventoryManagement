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
    public class DeclareDefectiveProductViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private ProductLocation _productLocation;

        public ProductViewModel Product => new ProductViewModel(_productLocation.Product);


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

        public DeclareDefectiveProductViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, ProductLocation productLocation, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _closeDialogCallback = closeDialogCallback;
            _productLocation = productLocation;
            _products = new ObservableCollection<ProductViewModel>();


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
            LoadProductsCommand = new RelayCommand(LoadProducts);
        }

        private void Submit()
        {
            ValidateAllProperties();

            int tmpQuantity;
            
            if (HasErrors)
            {
                return;
            }
            else if (!int.TryParse(_quantity, out tmpQuantity))
            {
                MessageBox.Show("Invalid Input");
                return;
            } else if (tmpQuantity > _productLocation.ProductQuantity)
            {
                MessageBox.Show($"Quantity exceeded the number of stocks! {_productLocation.ProductQuantity}");
                return;
            }

            Defective newDefective = new Defective()
            {
                DefectiveID = Guid.NewGuid(),
                ProductID = _productLocation.ProductID,
                Quantity = Convert.ToInt32(_quantity),
                DateDeclared = DateTime.Now
            };

            _unitOfWork.DefectiveRepository.Insert(newDefective);
            _productLocation.ProductQuantity -= Convert.ToInt32(_quantity);
            _productLocation.Product.ProductQuantity -= Convert.ToInt32(_quantity);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.STORAGES, ActionType.DELARE_AS_DEFECTIVE, $"Product declared as defective; ProductID: {_productLocation.ProductID}, Quantity: {_quantity}"));
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

        public static DeclareDefectiveProductViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, ProductLocation productLocation, Action closeDialogCallback)
        {
            DeclareDefectiveProductViewModel viewModel = new DeclareDefectiveProductViewModel(navigationStore, unitOfWork, productLocation, closeDialogCallback);
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
