using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    class AttributeValue
    {
        public string AttributeValueID { get; set; }
        public string AttributeID { get; set; }
        public string AttributeValueName { get; set; }
        public string AttributeValueStatus { get; set; }
        public Attribute Attribute { get; set; }
    }
}
