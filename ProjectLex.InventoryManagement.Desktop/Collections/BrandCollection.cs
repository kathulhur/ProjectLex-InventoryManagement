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
    public class BrandCollection : IDataCollection<Brand>, ILoadable<Brand>
    {
        private readonly IController<Brand> _controller;

        private List<Brand> _dataList;

        private Lazy<Task> _initializeLazy;

        public IEnumerable<Brand> DataList => _dataList;


        public BrandCollection(IController<Brand> controller)
        {
            _controller = controller;
            _dataList = new List<Brand>();
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public event Action<Brand> BrandCreated;
        public event Action<Brand> BrandRemoved;
        public event Action<Brand> BrandModified;
        private async Task Initialize()
        {
            IEnumerable<Brand> data = await _controller.Provider.GetAll();
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

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _controller.Provider.GetAll();
        }

        public async Task Create(Brand newBrand)
        {
            await _controller.Creator.Create(newBrand);
            _dataList.Add(newBrand);
            OnBrandCreated(newBrand);
        }

        public async Task Remove(Brand brand)
        {
            await _controller.Remover.Remove(brand);
            Brand removedBrand = _dataList.Where(b => b.BrandID == brand.BrandID).First();
            _dataList.Remove(removedBrand);
            OnBrandRemoved(removedBrand);
        }

        public async Task Modify(Brand modifiedBrand)
        {
            await _controller.Modifier.Modify(modifiedBrand);
            int index = _dataList.FindIndex(b => b.BrandID == modifiedBrand.BrandID);
            _dataList[index] = modifiedBrand;
            OnBrandModified(modifiedBrand);
    }


        private void OnBrandCreated(Brand brand)
        {
            BrandCreated?.Invoke(brand);
        }

        private void OnBrandRemoved(Brand brand)
        {
            BrandRemoved?.Invoke(brand);
        }

        private void OnBrandModified(Brand brand)
        {
            BrandModified?.Invoke(brand);
        }

}
}
