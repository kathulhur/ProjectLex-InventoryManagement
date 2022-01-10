using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Attribute
    {
        public string AttributeID { get; set; }
        public string AttributeName { get; set; }

        public Attribute(AttributeDTO attribute)
        {
            AttributeID = attribute.AttributeID.ToString();
            AttributeName = attribute.AttributeName;
        }

        public Attribute(CreateAttributeViewModel createAttributeViewModel)
        {
            AttributeID = Guid.NewGuid().ToString();
            AttributeName = createAttributeViewModel.AttributeName;
        }
        public Attribute(ModifyAttributeViewModel modifyAttributeViewModel)
        {
            AttributeID = modifyAttributeViewModel.AttributeID;
            AttributeName = modifyAttributeViewModel.AttributeName;
        }

        public Attribute(AttributeViewModel attributeViewModel)
        {
            AttributeID = attributeViewModel.AttributeID;
            AttributeName = attributeViewModel.AttributeName;
        }
    }
}
