using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
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
        private readonly IProvider<Brand> _provider;

        private List<Brand> _dataList;

        public IEnumerable<Brand> DataList => _dataList;


        public BrandCollection(IProvider<Brand> provider)
        {
            _provider = provider;
            _dataList = new List<Brand>();
        }

        public event Action<Brand> BrandCreated;
        public event Action<Brand> BrandRemoved;
        public event Action<Brand> BrandModified;

        public async Task Load()
        {
            try
            {
                IEnumerable<Brand> data = await _provider.GetAll();
                _dataList.Clear();
                _dataList.AddRange(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("BrandCollection.Load");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
            }
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _provider.GetAll();
        }

        public async Task Create(Brand newBrand)
        {
            await _provider.Create(newBrand);
            _dataList.Add(newBrand);
            OnBrandCreated(newBrand);
        }

        public async Task Remove(Brand brand)
        {
            await _provider.Remove(brand);
            Brand removedBrand = _dataList.Where(b => b.BrandID == brand.BrandID).First();
            _dataList.Remove(removedBrand);
            OnBrandRemoved(removedBrand);
        }

        public async Task Modify(Brand modifiedBrand)
        {
            await _provider.Modify(modifiedBrand);
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
