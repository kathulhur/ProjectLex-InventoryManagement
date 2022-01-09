using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class CategoryDTO
    {

        [Key]
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryStatus { get; set; }
        public ICollection<ProductCategoryDTO> ProductCategories { get; set; }
    }
}
