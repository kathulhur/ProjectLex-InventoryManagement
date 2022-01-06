using ProjectLex.InventoryManagement.Desktop.Models;
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
        public string Id
        {
            get
            {
                return Convert.ToString(_supplier.Id);
            }
        }

        public string Name => _supplier.Name;
        public string Address => _supplier.Address;
        public string Phone => _supplier.Phone;
        public string Fax => _supplier.Fax;
        public string Email => _supplier.Email;
        public string OtherDetails => _supplier.OtherDetails;

        public SupplierViewModel(Supplier supplier)
        {
            _supplier = supplier;
        }
    }
}
