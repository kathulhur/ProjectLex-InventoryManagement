using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class OrderDTO
    {
        [Key]
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public string CustomerName { get; set; }
        public decimal OrderTotal { get; set; }
        public UserDTO User { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
