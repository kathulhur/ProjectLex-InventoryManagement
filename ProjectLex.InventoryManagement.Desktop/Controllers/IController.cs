using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Modifiers;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using ProjectLex.InventoryManagement.Desktop.Services.Removers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Controllers
{
    public interface IController<TModel>
    {
        public IProvider<TModel> Provider { get; }
        public ICreator<TModel> Creator { get; }
        public IRemover<TModel> Remover { get; }
        public IModifier<TModel> Modifier { get; }
    }
}
