using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    public interface ILoadable<TModel>
    {
        public IEnumerable<TModel> DataList { get; }
        public Task Load();

    }
}
