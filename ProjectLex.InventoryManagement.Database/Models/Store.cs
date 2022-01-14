using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Store
    {
        [Key]
        public Guid StoreID { get; set; }
        public string StoreName { get; set; }
        public string StoreStatus { get; set; }
    }
}
