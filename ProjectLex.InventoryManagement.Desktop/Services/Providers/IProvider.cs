using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    public interface IProvider<TModel>
    {
        public Task<IEnumerable<TModel>> GetAll();

        public Task Remove(TModel model);

        public Task Modify(TModel model);

        public Task Create(TModel model);

    }
}
