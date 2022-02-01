using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Defective
    {
        [Key]
        public Guid DefectiveID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateDeclared { get; set; }
        public Product Product { get; set; }
    }
}
