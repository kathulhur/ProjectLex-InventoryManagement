using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Log
    {
        [Key]
        public Guid LogID { get; set; }
        public Guid StaffID { get; set; }
        public string LogCategory { get; set; }
        public string ActionType { get; set; }
        public string LogDetails { get; set; }
        public DateTime DateTime { get; set; }

        public Staff Staff { get; set; }
    }
}
