using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Warehouse
    {

        [Key]
        public Guid WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }
        public string WarehousePhone { get; set; }
        public string WarehouseEmail { get; set; }
        public decimal WarehouseVat { get; set; }

    }
}
