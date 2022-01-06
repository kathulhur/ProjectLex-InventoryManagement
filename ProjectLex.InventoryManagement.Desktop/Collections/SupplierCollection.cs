using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Collections
{
    public class SupplierCollection : DataCollectionBase<Supplier>
    {
        public SupplierCollection(IController<Supplier> supplierController) : base(supplierController)
        {

        }
    }
}
