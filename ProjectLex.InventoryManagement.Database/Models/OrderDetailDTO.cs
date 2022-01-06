using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class OrderDetailDTO
    {
        [Key]
        public string OrderDetailId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
        public int PaymentId { get; set; }
        public PaymentDTO Payment { get; set; }
    }
}
