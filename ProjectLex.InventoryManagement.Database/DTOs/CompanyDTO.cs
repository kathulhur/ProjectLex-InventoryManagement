using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class CompanyDTO
    {

        [Key]
        public Guid CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyVAT { get; set; }

    }
}
