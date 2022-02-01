using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Location
    {
        [Key]
        public Guid LocationID { get; set; }
        public string LocationName { get; set; }
        public ICollection<ProductLocation> ProductLocations { get; set; }
    }
}
