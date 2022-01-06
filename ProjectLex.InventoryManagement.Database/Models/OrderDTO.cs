using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class OrderDTO
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
        public int CustomerId { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
