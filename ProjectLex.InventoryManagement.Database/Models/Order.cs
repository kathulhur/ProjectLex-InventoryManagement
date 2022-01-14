using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public string CustomerName { get; set; }
        public decimal OrderTotal { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
