using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database
{
    class Program
    {
        public static void Main()
        {
            foreach(var od in Query())
            {
                Console.WriteLine($"ID : {od.OrderDetailId}");
                Console.WriteLine($"total : {od.Total}\n");
            }
            
        }

        public static IEnumerable<OrderDetailDTO> Query()
        {
            using InventoryManagementContext context = new InventoryManagementContext();
            IEnumerable<OrderDetailDTO> orderDetails = context.Orders
                .Select(o => o.OrderDetails)
                .First()
                .Select(od => new OrderDetailDTO
                {
                    OrderDetailId = od.OrderDetailId,
                    Total = od.Total
                });
            return orderDetails;
        }
    }
}
