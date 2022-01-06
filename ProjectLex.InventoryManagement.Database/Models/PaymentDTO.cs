using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class PaymentDTO
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public string OtherDetails { get; set; }
    }
}
