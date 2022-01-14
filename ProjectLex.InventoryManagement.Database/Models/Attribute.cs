using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Attribute
    {
        [Key]
        public Guid AttributeID { get; set; }
        public string AttributeName { get; set; }
        public ICollection<AttributeValue> AttributeValues { get; set; }
        
    }
}
