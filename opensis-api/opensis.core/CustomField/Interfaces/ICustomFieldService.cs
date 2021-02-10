using opensis.data.ViewModels.CustomField;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.CustomField.Interfaces
{
    public interface ICustomFieldService
    {
        public CustomFieldAddViewModel SaveCustomField(CustomFieldAddViewModel customFieldAddViewModel);
        //public CustomFieldAddViewModel ViewCustomField(CustomFieldAddViewModel customFieldAddViewModel);
        public CustomFieldAddViewModel UpdateCustomField(CustomFieldAddViewModel customFieldAddViewModel);
        public CustomFieldAddViewModel DeleteCustomField(CustomFieldAddViewModel customFieldAddViewModel);
        public FieldsCategoryAddViewModel SaveFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel);
        //public FieldsCategoryAddViewModel ViewFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel);
        public FieldsCategoryAddViewModel UpdateFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel);
        public FieldsCategoryAddViewModel DeleteFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel);
        public FieldsCategoryListViewModel GetAllFieldsCategory(FieldsCategoryListViewModel fieldsCategoryListViewModel);
        public CustomFieldSortOrderModel UpdateCustomFieldSortOrder(CustomFieldSortOrderModel customFieldSortOrderModel);
    }
}

