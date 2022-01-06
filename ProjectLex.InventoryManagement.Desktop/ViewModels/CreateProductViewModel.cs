using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateProductViewModel : ViewModelBase, IUpdatable<Category>, IUpdatable<Supplier>
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

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }


        private string _price;
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
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

        private string _supplierId;
        public string SupplierId
        {
            get { return _supplierId; }
            set
            {
                _supplierId = value;
                OnPropertyChanged(nameof(SupplierId));
            }
        }

        private string _categoryId;
        public string CategoryId
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }



        private readonly ProductCollection _productCollection;
        private readonly CategoryCollection _categoryCollection;
        private readonly SupplierCollection _supplierCollection;

        private readonly ObservableCollection<ProductViewModel> _products;
        private readonly ObservableCollection<CategoryViewModel> _categories;
        private readonly ObservableCollection<SupplierViewModel> _suppliers;

        public IEnumerable<ProductViewModel> Products => _products;
        public IEnumerable<CategoryViewModel> Categories => _categories;
        public IEnumerable<SupplierViewModel> Suppliers => _suppliers;


        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LoadCategoriesCommand { get; }
        public ICommand LoadSuppliersCommand { get; }

        
        public CreateProductViewModel
            (
                ProductCollection productCollection,
                CategoryCollection categoryCollection,
                SupplierCollection supplierCollection,
                NavigationService<ProductListViewModel> navigationService
            )
        {
            _productCollection = productCollection;
            _categoryCollection = categoryCollection;
            _supplierCollection = supplierCollection;

            _products = new ObservableCollection<ProductViewModel>();
            _categories = new ObservableCollection<CategoryViewModel>();
            _suppliers = new ObservableCollection<SupplierViewModel>();

            SubmitCommand = new CreateProductCommand(this, productCollection, navigationService);
            CancelCommand = new NavigateCommand<ProductListViewModel>(navigationService);

            LoadCategoriesCommand = new LoadCategoriesCommand(this, categoryCollection);
            LoadSuppliersCommand = new LoadSuppliersCommand(this, supplierCollection);
        }

        public static CreateProductViewModel LoadViewModel
            (
                ProductCollection productCollection,
                CategoryCollection categoryCollection,
                SupplierCollection supplierCollection,
                NavigationService<ProductListViewModel> navigationService
            )
        {

            CreateProductViewModel viewModel = new CreateProductViewModel
                (
                    productCollection,
                    categoryCollection,
                    supplierCollection,
                    navigationService
                );

            viewModel.LoadCategoriesCommand.Execute(null);
            viewModel.LoadSuppliersCommand.Execute(null);

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

        public void Update(IEnumerable<Category> categories)
        {
            _categories.Clear();

            foreach (Category c in categories)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel(c);
                _categories.Add(categoryViewModel);
            }

        }

        public void Update(IEnumerable<Supplier> suppliers)
        {

            _suppliers.Clear();

            foreach (Supplier s in suppliers)
            {
                SupplierViewModel supplierViewModel = new SupplierViewModel(s);
                _suppliers.Add(supplierViewModel);
            }
        }

    }
}
