using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class AttributeValueViewModel : ViewModelBase
    {
        public string AttributeValueID { get; }
        public string AttributeID { get; }
        public AttributeViewModel Attribute { get; }
        public string AttributeValueName { get; }
        public string AttributeValueStatus { get; }


        public AttributeValueViewModel(AttributeValue attributeValue, Models.Attribute attribute)
        {
            AttributeValueID = attributeValue.AttributeValueID;
            AttributeID = attributeValue.AttributeID;
            AttributeValueName = attributeValue.AttributeValueName;
            AttributeValueStatus = attributeValue.AttributeValueStatus;
            Attribute = new AttributeViewModel(attribute);
        }

        public AttributeValueViewModel(AttributeValue attributeValue, AttributeViewModel attribute)
        {
            AttributeValueID = attributeValue.AttributeValueID;
            AttributeID = attributeValue.AttributeID;
            AttributeValueName = attributeValue.AttributeValueName;
            AttributeValueStatus = attributeValue.AttributeValueStatus;
            Attribute = attribute;
        }

        public AttributeValueViewModel(AttributeValue attributeValue)
        {
            AttributeValueID = attributeValue.AttributeValueID;
            AttributeID = attributeValue.AttributeID;
            AttributeValueName = attributeValue.AttributeValueName;
            AttributeValueStatus = attributeValue.AttributeValueStatus;

        }
    }
}
