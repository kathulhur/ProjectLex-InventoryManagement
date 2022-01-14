using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class AttributeValue
    {
        [Key]
        public Guid AttributeValueID { get; set; }
        [Key]
        public Guid AttributeID { get; set; }
        public string AttributeValueName { get; set; }
        public string AttributeValueStatus { get; set; }
        public Attribute Attribute { get; set; }
        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
    }
}
