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
    class AttributeRemover : DatabaseServiceBase, IRemover<Models.Attribute>
    {
        public AttributeRemover(ContextFactory contextFactory)
            : base(contextFactory)
        {
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
