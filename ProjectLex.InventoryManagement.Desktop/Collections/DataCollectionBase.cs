using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Collections
{
    public class DataCollectionBase<TModel> : ILoadable<TModel>
    {
        private readonly IController<TModel> _controller;

        private List<TModel> _dataList;

        private Lazy<Task> _initializeLazy;
        public IEnumerable<TModel> DataList => _dataList;

        public event Action<TModel> DataCreated;

        public DataCollectionBase(IController<TModel> controller)
        {
            _controller = controller;
            _dataList = new List<TModel>();
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        private async Task Initialize()
        {
            IEnumerable<TModel> data = await _controller.GetAll();
             
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

        public async Task<IEnumerable<TModel>> GetAll()
        {
            return await _controller.GetAll();
        }

        public async Task Create(TModel obj)
        {
            await _controller.Create(obj);

            _dataList.Add(obj);

            OnDataCreated(obj);
        }

        public void OnDataCreated(TModel obj)
        {
            DataCreated?.Invoke(obj);
        }
    }
}
