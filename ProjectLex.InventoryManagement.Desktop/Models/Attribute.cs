using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    class Attribute
    {
        public string AttributeID { get; set; }
        public string AttributeName { get; set; }
        public ICollection<AttributeValue> AttributeValues { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }

        public Attribute(Attribute attribute)
        {
            AttributeID = attribute.AttributeID.ToString();
            AttributeName = attribute.AttributeName;
            AttributeValues = attribute.AttributeValues;
            ProductAttributes = attribute.ProductAttributes;
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
            AttributeStatus = modifyAttributeViewModel.AttributeStatus;
        }

        public Attribute(AttributeViewModel attributeViewModel)
        {
            AttributeID = attributeViewModel.AttributeID;
            AttributeName = attributeViewModel.AttributeName;
            AttributeStatus = attributeViewModel.AttributeStatus;
        }
    }
}
