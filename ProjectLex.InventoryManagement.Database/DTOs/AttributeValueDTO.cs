using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class AttributeValueDTO
    {
        [Key]
        public Guid AttributeValueID { get; set; }
        public Guid AttributeID { get; set; }
        public string AttributeValueName { get; set; }
        public string AttributeValueStatus { get; set; }
        public AttributeDTO Attribute { get; set; }
    }
}
