using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Collections
{
    public class RoleCollection : IDataCollection<Role>, ILoadable<Role>
    {
        private readonly IController<Role> _controller;

        private List<Role> _dataList;

        public IEnumerable<Role> DataList => _dataList;


        public RoleCollection(IController<Role> controller)
        {
            _controller = controller;
            _dataList = new List<Role>();
        }

        public event Action<Role> RoleCreated;
        public event Action<Role> RoleRemoved;
        public event Action<Role> RoleModified;

        public async Task Load()
        {
            try
            {
                IEnumerable<Role> data = await _controller.Provider.GetAll();
                _dataList.Clear();
                _dataList.AddRange(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("RoleCollection.Load : ");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
            }
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _controller.Provider.GetAll();
        }

        public async Task Create(Role newRole)
        {
            await _controller.Creator.Create(newRole);
            _dataList.Add(newRole);
            OnRoleCreated(newRole);
        }

        public async Task Remove(Role role)
        {
            await _controller.Remover.Remove(role);
            Role removedRole = _dataList.Where(r => r.RoleID == role.RoleID).First();
            _dataList.Remove(removedRole);
            OnRoleRemoved(removedRole);
        }

        public async Task Modify(Role modifiedRole)
        {
            await _controller.Modifier.Modify(modifiedRole);
            int index = _dataList.FindIndex(b => b.RoleID == modifiedRole.RoleID);
            _dataList[index] = modifiedRole;
            OnRoleModified(modifiedRole);
    }


        private void OnRoleCreated(Role role)
        {
            RoleCreated?.Invoke(role);
        }

        private void OnRoleRemoved(Role role)
        {
            RoleRemoved?.Invoke(role);
        }

        private void OnRoleModified(Role role)
        {
            RoleModified?.Invoke(role);
        }

}
}
