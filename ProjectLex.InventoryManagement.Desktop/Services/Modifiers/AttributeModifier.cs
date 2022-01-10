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
    class AttributeModifier : DatabaseServiceBase, IModifier<Models.Attribute>
    {
        public AttributeModifier(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
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
    }
}
