using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Modifiers
{
    public interface IModifier<TModel>
    {
        public Task Modify(TModel model);
    }
}
