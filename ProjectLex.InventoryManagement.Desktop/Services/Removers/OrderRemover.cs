using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Removers
{
    class OrderRemover : DatabaseServiceBase, IRemover<Order>
    {
        public OrderRemover(ContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public async Task Remove(Order order)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            OrderDTO orderDTO = context.Orders
                .Where(r => r.OrderID == new Guid(order.OrderID)).First();

            context.Orders.Remove(orderDTO);
            await context.SaveChangesAsync();
        }
    }
}
