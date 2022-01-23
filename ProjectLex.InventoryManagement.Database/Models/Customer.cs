using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; set; }
        public Guid StaffID { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public Staff Staff { get; set; }
    }
}
