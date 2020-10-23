using opensis.data.ViewModels.CustomField;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.CustomField.Interfaces
{
    public interface ICustomFieldService
    {
        public CustomFieldAddViewModel SaveCustomField(CustomFieldAddViewModel customFieldAddViewModel);
        public CustomFieldAddViewModel ViewCustomField(CustomFieldAddViewModel customFieldAddViewModel);
        public CustomFieldAddViewModel UpdateCustomField(CustomFieldAddViewModel customFieldAddViewModel);
        public CustomFieldAddViewModel DeleteCustomField(CustomFieldAddViewModel customFieldAddViewModel);
        public CustomFieldListViewModel GetAllCustomField(CustomFieldListViewModel customFieldListViewModel);

    }
}

