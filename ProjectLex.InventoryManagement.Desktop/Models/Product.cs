using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Product
    {
        public string ProductId { get; }
        public string ProductName { get; }
        public string ProductDescription { get; }
        public string ProductUnit { get; }
        public decimal ProductPrice { get; }
        public int ProductQuantity { get; }
        public int ProductStatus { get; }
        public string OtherDetails { get; }
        public int SupplierId { get; }
        public int CategoryId { get; }

        public Product(string productId, string productName, string productDescription, 
            string productUnit, decimal productPrice, int productQuantity, int productStatus, 
            string otherDetails, int supplierId, int categoryId)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductUnit = productUnit;
            ProductPrice = productPrice;
            ProductQuantity = productQuantity;
            ProductStatus = productStatus;
            OtherDetails = otherDetails;
            SupplierId = supplierId;
            CategoryId = categoryId;
        }

    }
}
