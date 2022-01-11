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
    public class AttributeProvider : DatabaseServiceBase, IProvider<Models.Attribute>
    {

        public AttributeProvider(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<Models.Attribute>> GetAll()
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            IEnumerable<AttributeDTO> attributeDTOs = await context.Attributes.ToListAsync();

            return attributeDTOs.Select(a => new Models.Attribute(a));
        }

        public async Task Create(Models.Attribute attribute)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            AttributeDTO AttributeDTO = ModelConverters.AttributeToAttributeDTO(attribute);
            context.Attributes.Add(AttributeDTO);
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

        public async Task Modify(Models.Attribute attribute)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            AttributeDTO attributeDTO = context.Attributes.Where(b => b.AttributeID == new Guid(attribute.AttributeID)).First();
            UpdateAttribute(attributeDTO, attribute);
            await context.SaveChangesAsync();
        }

        private void UpdateAttribute(AttributeDTO attributeDTO, Models.Attribute attribute)
        {
            if (!attributeDTO.AttributeName.Equals(attribute.AttributeName))
            {
                attributeDTO.AttributeName = attribute.AttributeName;
            }

        }

        public async Task Remove(Models.Attribute attribute)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            AttributeDTO attributeDTO = context.Attributes
                .Where(b => b.AttributeID == new Guid(attribute.AttributeID)).First();

            context.Attributes.Remove(attributeDTO);
            await context.SaveChangesAsync();
        }

    }
}
