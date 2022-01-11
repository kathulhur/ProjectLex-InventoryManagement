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
    public class AttributeCollection : IDataCollection<Models.Attribute>, ILoadable<Models.Attribute>
    {
        private readonly IProvider<Models.Attribute> _provider;

        private List<Models.Attribute> _dataList;

        public IEnumerable<Models.Attribute> DataList => _dataList;


        public AttributeCollection(IProvider<Models.Attribute> controller)
        {
            _provider = controller;
            _dataList = new List<Models.Attribute>();
        }

        public event Action<Models.Attribute> AttributeCreated;
        public event Action<Models.Attribute> AttributeRemoved;
        public event Action<Models.Attribute> AttributeModified;

        public async Task Load()
        {
            try
            {
                IEnumerable<Models.Attribute> data = await _provider.GetAll();
                _dataList.Clear();
                _dataList.AddRange(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("AttributeCollection.Load");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
            }
        }

        public async Task<IEnumerable<Models.Attribute>> GetAll()
        {
            return await _provider.GetAll();
        }

        public async Task Create(Models.Attribute newAttribute)
        {
            await _provider.Create(newAttribute);
            _dataList.Add(newAttribute);
            OnAttributeCreated(newAttribute);
        }

        public async Task Remove(Models.Attribute attribute)
        {
            await _provider.Remove(attribute);
            Models.Attribute removedAttribute = _dataList.Where(b => b.AttributeID == attribute.AttributeID).First();
            _dataList.Remove(removedAttribute);
            OnAttributeRemoved(removedAttribute);
        }

        public async Task Modify(Models.Attribute modifiedAttribute)
        {
            await _provider.Modify(modifiedAttribute);
            int index = _dataList.FindIndex(b => b.AttributeID == modifiedAttribute.AttributeID);
            _dataList[index] = modifiedAttribute;
            OnAttributeModified(modifiedAttribute);
    }


        private void OnAttributeCreated(Models.Attribute attribute)
        {
            AttributeCreated?.Invoke(attribute);
        }

        private void OnAttributeRemoved(Models.Attribute attribute)
        {
            AttributeRemoved?.Invoke(attribute);
        }

        private void OnAttributeModified(Models.Attribute attribute)
        {
            AttributeModified?.Invoke(attribute);
        }

}
}
