using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Removers
{
    public interface IRemover<TModel>
    {
        public Task Remove(TModel model);
    }
}
