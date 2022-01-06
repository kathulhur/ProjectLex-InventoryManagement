using ProjectLex.InventoryManagement.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Services
{
    public class ContextFactory
    {
        public InventoryContext GetDbContext()
        {
            return new InventoryContext();
        }
    }
}
