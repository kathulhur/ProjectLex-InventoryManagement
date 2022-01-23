using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Supplier
    {
        [Key]
        public Guid SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierStatus { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
