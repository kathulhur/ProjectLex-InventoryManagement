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
        public string LocationZone { get; set; }
        public string LocationAisle { get; set; }
        public string LocationBay { get; set; }
        public string LocationRow { get; set; }
        public string SubLocation { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
