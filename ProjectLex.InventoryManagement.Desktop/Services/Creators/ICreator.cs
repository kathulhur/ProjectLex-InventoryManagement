using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Creators
{

    public interface ICreator<TModel>
    {

        public Task Create(TModel model);
    }
}
