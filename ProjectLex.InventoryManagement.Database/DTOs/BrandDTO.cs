using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class BrandDTO
    {
        [Key]
        public Guid BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandStatus { get; set; }
        public ICollection<ProductBrandDTO> ProductBrands { get; set; }
    }
}
