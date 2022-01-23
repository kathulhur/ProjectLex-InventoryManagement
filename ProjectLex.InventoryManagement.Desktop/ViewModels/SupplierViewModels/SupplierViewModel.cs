using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class SupplierViewModel : ViewModelBase
    {
        private readonly Supplier _supplier;
        public Supplier Supplier => _supplier;

        public string SupplierID => _supplier.SupplierID.ToString();
        public string SupplierName => _supplier.SupplierName;
        public string SupplierAddress => _supplier.SupplierAddress;
        public string SupplierPhone => _supplier.SupplierPhone;
        public string SupplierEmail => _supplier.SupplierEmail;
        public string SupplierStatus => _supplier.SupplierStatus;

        public SupplierViewModel(Supplier supplier)
        {
            _supplier = supplier;
        }
    }
}
