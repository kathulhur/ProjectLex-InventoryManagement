using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Collections
{
    public interface IDataCollection <TModel>
    {
        public Task<IEnumerable<TModel>> GetAll();
        public Task Create(object viewModel);
        public Task Remove(object viewModel);
        public Task Modify(object viewModel);
    }
}
