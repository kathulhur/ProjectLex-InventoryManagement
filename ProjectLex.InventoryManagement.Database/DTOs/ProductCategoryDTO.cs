using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class ProductCategoryDTO
    {
        [Key]
        public Guid ProductID { get; set; }
        [Key]
        public Guid StoreID { get; set; }
        [Key]
        public Guid CategoryID { get; set; }
        public ProductDTO Product { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
