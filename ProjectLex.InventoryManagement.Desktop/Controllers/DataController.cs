using ProjectLex.InventoryManagement.Desktop.Models;
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
    public class DataController<TModel> : IController<TModel>
    {
        private readonly IProvider<TModel> _provider;
        private readonly ICreator<TModel> _creator;
        private readonly IRemover<TModel> _remover;
        private readonly IModifier<TModel> _modifier;
        public DataController
            (
                IProvider<TModel> provider, ICreator<TModel> creator, 
                IRemover<TModel> remover, IModifier<TModel> modifier)
        {
            _provider = provider;
            _creator = creator;
            _remover = remover;
            _modifier = modifier;
        }

        public IProvider<TModel> Provider => _provider;

        public ICreator<TModel> Creator => _creator;

        public IRemover<TModel> Remover => _remover;
        public IModifier<TModel> Modifier => _modifier;
    }
}
