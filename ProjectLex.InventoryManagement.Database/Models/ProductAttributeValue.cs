using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class ProductAttributeValue
    {
        [Key]
        public Guid ProductID { get; set; }
        [Key]
        public Guid StoreID { get; set; }
        [Key]
        public Guid AttributeValueID { get; set; }
        [Key]
        public Guid AttributeID { get; set; }
        public Product Product { get; set; }
        public AttributeValue AttributeValue { get; set; }

    }
}
