using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Category
    {

        [Key]
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryStatus { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
