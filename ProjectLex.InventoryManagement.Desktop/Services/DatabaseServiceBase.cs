using ProjectLex.InventoryManagement.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services
{

    public abstract class DatabaseServiceBase
    {
        private readonly ContextFactory _dbContextFactory;
        protected ContextFactory ContextFactory => _dbContextFactory;

        public DatabaseServiceBase(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

    }
}
