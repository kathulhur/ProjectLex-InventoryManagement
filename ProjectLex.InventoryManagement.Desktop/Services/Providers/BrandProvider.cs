using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    public class BrandProvider : DatabaseServiceBase, IProvider<Brand>
    {

        public BrandProvider(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            IEnumerable<BrandDTO> brandDTOs = await context.Brands.ToListAsync();

            return brandDTOs.Select(b => new Brand(b));
        }

        public async Task Create(Brand brand)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            BrandDTO BrandDTO = ModelConverters.BrandToBrandDTO(brand);
            context.Brands.Add(BrandDTO);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException dbE)
            {
                Debug.WriteLine(dbE.InnerException);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
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
