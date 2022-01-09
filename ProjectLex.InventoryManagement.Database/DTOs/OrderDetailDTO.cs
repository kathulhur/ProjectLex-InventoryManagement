﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class OrderDetailDTO
    {
        [Key]
        public Guid ProductID { get; set; }
        [Key]
        public Guid StoreID { get; set; }

        [Key]
        public Guid OrderID { get; set; }
        public int OrderDetailQuantity { get; set; }
        public decimal OrderDetailAmount { get; set; }
        public ProductDTO Product { get; set; }

        public OrderDTO Order { get; set; }
    }
}
