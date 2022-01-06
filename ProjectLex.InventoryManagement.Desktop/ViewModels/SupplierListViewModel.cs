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
    public class SupplierListViewModel : ViewModelBase, IUpdatable<Supplier>
    {

        private bool _isDisposed = false;

        private readonly ObservableCollection<SupplierViewModel> _suppliers;
        public IEnumerable<SupplierViewModel> Suppliers => _suppliers;

        private readonly SupplierCollection _supplierCollection;

        public ICommand CreateSupplierCommand { get; }
        public ICommand LoadSuppliersCommand { get; }

        public SupplierListViewModel(SupplierCollection supplierCollection, NavigationService<CreateSupplierViewModel> navigationService)
        {
            _supplierCollection = supplierCollection;
            _suppliers = new ObservableCollection<SupplierViewModel>();
            CreateSupplierCommand = new NavigateCommand<CreateSupplierViewModel>(navigationService);
            LoadSuppliersCommand = new LoadSuppliersCommand(this, _supplierCollection);

            _supplierCollection.DataCreated += OnSupplierCreated;

        }

        private void OnSupplierCreated(Supplier supplier)
        {
            SupplierViewModel newSupplierViewModel = new SupplierViewModel(supplier);
            _suppliers.Add(newSupplierViewModel);

        }

        public static SupplierListViewModel LoadViewModel(SupplierCollection supplierCollection, NavigationService<CreateSupplierViewModel> navigationService)
        {
            SupplierListViewModel viewModel = new SupplierListViewModel(supplierCollection, navigationService);
            viewModel.LoadSuppliersCommand.Execute(null);

            return viewModel;
        }

        public void Update(IEnumerable<Supplier> suppliers)
        {
            _suppliers.Clear();

            foreach (Supplier c in suppliers)
            {
                SupplierViewModel newSupplierViewModel = new SupplierViewModel(c);
                _suppliers.Add(newSupplierViewModel);
            }
        }


        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
