using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class AttributeDTO
    {
        [Key]
        public Guid AttributeID { get; set; }
        public string AttributeName { get; set; }
        public ICollection<AttributeValueDTO> AttributeValues { get; set; }
        public ICollection<ProductAttributeDTO> ProductAttributes { get; set; }
        
    }
}
