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
    public class UserCollection : IDataCollection<User>, ILoadable<User>
    {
        private readonly IController<User> _controller;

        private List<User> _dataList;

        private Lazy<Task> _initializeLazy;

        public IEnumerable<User> DataList => _dataList;


        public UserCollection(IController<User> controller)
        {
            _controller = controller;
            _dataList = new List<User>();
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public event Action<User> UserCreated;
        public event Action<User> UserRemoved;
        public event Action<User> UserModified;
        private async Task Initialize()
        {
            IEnumerable<User> data = await _controller.Provider.GetAll();
            _dataList.Clear();
            _dataList.AddRange(data);
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _controller.Provider.GetAll();
        }

        public async Task Create(User newUser)
        {
            await _controller.Creator.Create(newUser);
            _dataList.Add(newUser);
            OnUserCreated(newUser);
        }

        public async Task Remove(User user)
        {
            await _controller.Remover.Remove(user);
            User removedUser = _dataList.Where(r => r.UserID == user.UserID).First();
            _dataList.Remove(removedUser);
            OnUserRemoved(removedUser);
        }

        public async Task Modify(User modifiedUser)
        {
            await _controller.Modifier.Modify(modifiedUser);
            int index = _dataList.FindIndex(b => b.UserID == modifiedUser.UserID);
            _dataList[index] = modifiedUser;
            OnUserModified(modifiedUser);
    }


        private void OnUserCreated(User user)
        {
            UserCreated?.Invoke(user);
        }

        private void OnUserRemoved(User user)
        {
            UserRemoved?.Invoke(user);
        }

        private void OnUserModified(User user)
        {
            UserModified?.Invoke(user);
        }

}
}
