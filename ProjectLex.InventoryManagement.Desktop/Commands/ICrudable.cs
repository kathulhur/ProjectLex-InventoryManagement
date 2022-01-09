using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Commands
{
    public interface ICrudable <TModel>
    {
        public TModel RemoveData();
        public void Update(IEnumerable<TModel> products);
    }
}
