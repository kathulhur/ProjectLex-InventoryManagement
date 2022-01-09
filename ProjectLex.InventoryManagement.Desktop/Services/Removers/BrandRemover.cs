using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Removers
{
    class BrandRemover : DatabaseServiceBase, IRemover<Brand>
    {
        public BrandRemover(ContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public async Task Remove(Brand brand)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            BrandDTO brandDTO = context.Brands
                .Where(b => b.BrandID == new Guid(brand.BrandID)).First();

            context.Brands.Remove(brandDTO);
            await context.SaveChangesAsync();
        }
    }
}
