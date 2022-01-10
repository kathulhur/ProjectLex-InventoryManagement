using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    public class OrderProvider : IProvider<Order>
    {
        private readonly ContextFactory _dbContextFactory;

        public OrderProvider(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            using InventoryManagementContext context = _dbContextFactory.GetDbContext();
            IEnumerable<OrderDTO> orderDTOs = await context.Orders.ToListAsync();

            return orderDTOs.Select(u => new Order(u));
        }

    }
}
