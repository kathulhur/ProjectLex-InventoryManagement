using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Controllers
{

    public class SupplierController : IController<Supplier>
    {
        private readonly IProvider<Supplier> _supplierProvider;
        private readonly ICreator<Supplier> _supplierCreator;

        public SupplierController(IProvider<Supplier> supplierProvider, ICreator<Supplier> supplierCreator)
        {
            _supplierProvider = supplierProvider;
            _supplierCreator = supplierCreator;
        }

        public async Task Create(Supplier newData)
        {
            await _supplierCreator.Create(newData);
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _supplierProvider.GetAll();
        }
    }
}
