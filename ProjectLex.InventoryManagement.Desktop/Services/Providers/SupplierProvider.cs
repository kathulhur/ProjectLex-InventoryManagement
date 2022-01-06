using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    public class SupplierProvider : IProvider<Supplier>
    {
        private readonly ContextFactory _dbContextFactory;

        public SupplierProvider(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            using InventoryContext context = _dbContextFactory.GetDbContext();
            IEnumerable<SupplierDTO> supplierDTOs = await context.Suppliers.ToListAsync();

            return supplierDTOs.Select(ModelConverters.SupplierDTOToSupplier);
        }

    }
}
