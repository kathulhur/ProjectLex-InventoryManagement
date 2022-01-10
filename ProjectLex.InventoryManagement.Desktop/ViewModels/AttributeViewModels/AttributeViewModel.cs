using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class AttributeViewModel : ViewModelBase
    {
        private readonly Models.Attribute _attribute;
        public string AttributeID => _attribute.AttributeID;
        public string AttributeName => _attribute.AttributeName;

        public AttributeViewModel(Models.Attribute attribute)
        {
            _attribute = attribute;
        }
    }
}
