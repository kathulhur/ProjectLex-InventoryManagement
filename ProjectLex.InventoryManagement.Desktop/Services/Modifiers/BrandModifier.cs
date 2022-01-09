using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Modifiers
{
    class BrandModifier : DatabaseServiceBase, IModifier<Brand>
    {
        public BrandModifier(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task Modify(Brand brand)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            BrandDTO brandDTO = context.Brands.Where(b => b.BrandID == new Guid(brand.BrandID)).First();
            UpdateBrand(brandDTO, brand);
            await context.SaveChangesAsync();
        }

        private void UpdateBrand(BrandDTO brandDTO, Brand brand)
        {
            if (!brandDTO.BrandName.Equals(brand.BrandName))
            {
                brandDTO.BrandName = brand.BrandName;
            }

            if (!brandDTO.BrandStatus.Equals(brand.BrandStatus))
            {
                brandDTO.BrandStatus = brand.BrandStatus;
            }

        }
    }
}
