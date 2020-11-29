using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.CustomField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace opensis.data.Repository
{
    public class CustomFieldRepository : ICustomFieldRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public CustomFieldRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        /// <summary>
        /// Add Custom Field
        /// </summary>
        /// <param name="customFieldAddViewModel"></param>
        /// <returns></returns>
        public CustomFieldAddViewModel AddCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(customFieldAddViewModel.customFields.Type) && !string.IsNullOrWhiteSpace(customFieldAddViewModel.customFields.Module))
                {
                    int? MasterFieldId = Utility.GetMaxPK(this.context, new Func<CustomFields, int>(x => x.FieldId));
                    customFieldAddViewModel.customFields.FieldId = (int)MasterFieldId;
                    customFieldAddViewModel.customFields.LastUpdate = DateTime.UtcNow;
                    this.context?.CustomFields.Add(customFieldAddViewModel.customFields);
                    this.context?.SaveChanges();
                    customFieldAddViewModel._failure = false;
                }
                else
                {
                    customFieldAddViewModel.customFields = null;
                    customFieldAddViewModel._failure = true;
                    customFieldAddViewModel._message = "Please enter Type and Module";
                }
            }
            catch (Exception es)
            {
                customFieldAddViewModel._failure = true;
                customFieldAddViewModel._message = es.Message;
            }

            return customFieldAddViewModel;
        }

        /// <summary>
        /// View Custom Field By Id
        /// </summary>
        /// <param name="customFieldAddViewModel"></param>
        /// <returns></returns>
        //public CustomFieldAddViewModel ViewCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        //{
        //    CustomFieldAddViewModel customFieldView = new CustomFieldAddViewModel();
        //    try
        //    {
        //        var CustomField = this.context?.CustomFields.FirstOrDefault(x => x.TenantId == customFieldAddViewModel.customFields.TenantId && x.SchoolId == customFieldAddViewModel.customFields.SchoolId && x.FieldId == customFieldAddViewModel.customFields.FieldId);
        //        if (CustomField != null)
        //        {
        //            customFieldView.customFields = CustomField;
        //            customFieldView._failure = false;
        //        }
        //        else
        //        {
        //            customFieldView._failure = true;
        //            customFieldView._message = NORECORDFOUND;
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        customFieldView._failure = true;
        //        customFieldView._message = es.Message;
        //    }
        //    return customFieldView;

        //}

        /// <summary>
        /// Update Custom Field
        /// </summary>
        /// <param name="customFieldAddViewModel"></param>
        /// <returns></returns>
        public CustomFieldAddViewModel UpdateCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(customFieldAddViewModel.customFields.Type) && !string.IsNullOrWhiteSpace(customFieldAddViewModel.customFields.Module))
                {                
                    var customFieldUpdate = this.context?.CustomFields.FirstOrDefault(x => x.TenantId == customFieldAddViewModel.customFields.TenantId && x.SchoolId == customFieldAddViewModel.customFields.SchoolId && x.FieldId == customFieldAddViewModel.customFields.FieldId);

                    customFieldUpdate.TenantId = customFieldAddViewModel.customFields.TenantId;
                    customFieldUpdate.SchoolId = customFieldAddViewModel.customFields.SchoolId;
                    customFieldUpdate.FieldId = customFieldAddViewModel.customFields.FieldId;
                    customFieldUpdate.Type = customFieldAddViewModel.customFields.Type;
                    customFieldUpdate.Search = customFieldAddViewModel.customFields.Search;
                    customFieldUpdate.Title = customFieldAddViewModel.customFields.Title;
                    customFieldUpdate.SortOrder = customFieldAddViewModel.customFields.SortOrder;
                    customFieldUpdate.SelectOptions = customFieldAddViewModel.customFields.SelectOptions;
                    customFieldUpdate.CategoryId = customFieldAddViewModel.customFields.CategoryId;
                    customFieldUpdate.SystemField = customFieldAddViewModel.customFields.SystemField;
                    customFieldUpdate.Required = customFieldAddViewModel.customFields.Required;
                    customFieldUpdate.DefaultSelection = customFieldAddViewModel.customFields.DefaultSelection;
                    customFieldUpdate.Hide = customFieldAddViewModel.customFields.Hide;
                    customFieldUpdate.LastUpdate = DateTime.UtcNow;
                    customFieldUpdate.UpdatedBy = customFieldAddViewModel.customFields.UpdatedBy;

                    this.context?.SaveChanges();
                    customFieldAddViewModel._failure = false;
                    customFieldAddViewModel._message = "Entity Updated";
                }
                else
                {
                    customFieldAddViewModel.customFields = null;
                    customFieldAddViewModel._failure = true;
                    customFieldAddViewModel._message = "Please enter Type and Module";
                }
            }
            catch (Exception ex)
            {
                    customFieldAddViewModel.customFields = null;
                    customFieldAddViewModel._failure = true;
                    customFieldAddViewModel._message = ex.Message;
            }
            return customFieldAddViewModel;
        }

        /// <summary>
        /// Delete Custom Field
        /// </summary>
        /// <param name="customFieldAddViewModel"></param>
        /// <returns></returns>
        public CustomFieldAddViewModel DeleteCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            try
            {
                var customFieldDelete = this.context?.CustomFields.FirstOrDefault(x => x.TenantId == customFieldAddViewModel.customFields.TenantId && x.SchoolId == customFieldAddViewModel.customFields.SchoolId && x.FieldId == customFieldAddViewModel.customFields.FieldId);

                this.context?.CustomFields.Remove(customFieldDelete);
                this.context?.SaveChanges();
                customFieldAddViewModel._failure = false;
                customFieldAddViewModel._message = "Deleted";
            }

            catch (Exception es)
            {
                customFieldAddViewModel._failure = true;
                customFieldAddViewModel._message = es.Message;
            }
            return customFieldAddViewModel;
        }

        /// <summary>
        /// Add FieldsCategory
        /// </summary>
        /// <param name="fieldsCategoryAddViewModel"></param>
        /// <returns></returns>
        public FieldsCategoryAddViewModel AddFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        {
            int? CategoryId = Utility.GetMaxPK(this.context, new Func<FieldsCategory, int>(x => x.CategoryId));
            fieldsCategoryAddViewModel.fieldsCategory.CategoryId = (int)CategoryId;
            fieldsCategoryAddViewModel.fieldsCategory.LastUpdate = DateTime.UtcNow;
            this.context?.FieldsCategory.Add(fieldsCategoryAddViewModel.fieldsCategory);
            this.context?.SaveChanges();
            fieldsCategoryAddViewModel._failure = false;

            return fieldsCategoryAddViewModel;
        }
        /// <summary>
        /// View FieldsCategory By Id
        /// </summary>
        /// <param name="fieldsCategoryAddViewModel"></param>
        /// <returns></returns>
        //public FieldsCategoryAddViewModel ViewFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        //{
        //    FieldsCategoryAddViewModel fieldsCategoryViewModel = new FieldsCategoryAddViewModel();
        //    try
        //    {
        //        var fieldsCategoryView = this.context?.FieldsCategory.FirstOrDefault(x => x.TenantId == fieldsCategoryAddViewModel.fieldsCategory.TenantId && x.SchoolId == fieldsCategoryAddViewModel.fieldsCategory.SchoolId && x.CategoryId == fieldsCategoryAddViewModel.fieldsCategory.CategoryId);
        //        if (fieldsCategoryView != null)
        //        {
        //            fieldsCategoryViewModel.fieldsCategory = fieldsCategoryView;
        //            fieldsCategoryViewModel._failure = false;
        //        }
        //        else
        //        {
        //            fieldsCategoryViewModel._failure = true;
        //            fieldsCategoryViewModel._message = NORECORDFOUND;
        //        }
        //    }
        //    catch (Exception es)
        //    {

        //        fieldsCategoryViewModel._failure = true;
        //        fieldsCategoryViewModel._message = es.Message;
        //    }
        //    return fieldsCategoryViewModel;
        //}
        /// <summary>
        /// Update FieldsCategory 
        /// </summary>
        /// <param name="fieldsCategoryAddViewModel"></param>
        /// <returns></returns>
        public FieldsCategoryAddViewModel UpdateFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        {
            FieldsCategoryAddViewModel fieldsCategoryUpdateModel = new FieldsCategoryAddViewModel();
            try
            {
                var fieldsCategoryUpdate = this.context?.FieldsCategory.FirstOrDefault(x => x.TenantId == fieldsCategoryAddViewModel.fieldsCategory.TenantId && x.SchoolId == fieldsCategoryAddViewModel.fieldsCategory.SchoolId && x.CategoryId == fieldsCategoryAddViewModel.fieldsCategory.CategoryId);
                fieldsCategoryUpdate.IsSystemCategory = fieldsCategoryAddViewModel.fieldsCategory.IsSystemCategory;
                fieldsCategoryUpdate.Search = fieldsCategoryAddViewModel.fieldsCategory.Search;
                fieldsCategoryUpdate.Title = fieldsCategoryAddViewModel.fieldsCategory.Title;
                fieldsCategoryUpdate.Module = fieldsCategoryAddViewModel.fieldsCategory.Module;
                fieldsCategoryUpdate.SortOrder = fieldsCategoryAddViewModel.fieldsCategory.SortOrder;
                fieldsCategoryUpdate.Required = fieldsCategoryAddViewModel.fieldsCategory.Required;
                fieldsCategoryUpdate.Hide = fieldsCategoryAddViewModel.fieldsCategory.Hide;
                fieldsCategoryUpdate.LastUpdate = DateTime.UtcNow;
                fieldsCategoryUpdate.UpdatedBy = fieldsCategoryAddViewModel.fieldsCategory.UpdatedBy;
                this.context?.SaveChanges();
                fieldsCategoryAddViewModel._failure = false;
                fieldsCategoryAddViewModel._message = "Entity Updated";
            }
            catch (Exception es)
            {
                fieldsCategoryAddViewModel._failure = true;
                fieldsCategoryAddViewModel._message = es.Message;
            }
            return fieldsCategoryAddViewModel;
        }
        /// <summary>
        /// Delete FieldsCategory
        /// </summary>
        /// <param name="fieldsCategoryAddViewModel"></param>
        /// <returns></returns>
        public FieldsCategoryAddViewModel DeleteFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        {
            try
            {
                var fieldsCategoryDelete = this.context?.FieldsCategory.FirstOrDefault(x => x.TenantId == fieldsCategoryAddViewModel.fieldsCategory.TenantId && x.SchoolId == fieldsCategoryAddViewModel.fieldsCategory.SchoolId && x.CategoryId == fieldsCategoryAddViewModel.fieldsCategory.CategoryId);
                this.context?.FieldsCategory.Remove(fieldsCategoryDelete);
                this.context?.SaveChanges();
                fieldsCategoryAddViewModel._failure = false;
                fieldsCategoryAddViewModel._message = "Deleted";
            }
            catch (Exception es)
            {
                fieldsCategoryAddViewModel._failure = true;
                fieldsCategoryAddViewModel._message = es.Message;
            }
            return fieldsCategoryAddViewModel;
        }
        /// <summary>
        /// Get All FieldsCategory
        /// </summary>
        /// <param name="fieldsCategoryListViewModel"></param>
        /// <returns></returns>
        public FieldsCategoryListViewModel GetAllFieldsCategory(FieldsCategoryListViewModel fieldsCategoryListViewModel)
        {
            FieldsCategoryListViewModel fieldsCategoryListModel = new FieldsCategoryListViewModel();
            try
            {

                var fieldsCategoryList = this.context?.FieldsCategory
                    .Include(x=>x.CustomFields)
                    .ThenInclude(y=>y.CustomFieldsValue)
                    .Where(x => x.TenantId == fieldsCategoryListViewModel.TenantId && 
                                x.SchoolId == fieldsCategoryListViewModel.SchoolId && 
                                x.Module== fieldsCategoryListViewModel.Module)
                    .OrderByDescending(x => x.IsSystemCategory).ThenBy(x=>x.SortOrder).ToList();
                var customFields = fieldsCategoryList.FirstOrDefault().CustomFields.OrderByDescending(y => y.SystemField).ThenBy(y => y.SortOrder).ToList();
                fieldsCategoryListModel.fieldsCategoryList = fieldsCategoryList;
                fieldsCategoryListModel.fieldsCategoryList.FirstOrDefault().CustomFields = customFields;
                fieldsCategoryListModel._tenantName = fieldsCategoryListViewModel._tenantName;
                fieldsCategoryListModel._token = fieldsCategoryListViewModel._token;
                fieldsCategoryListModel._failure = false;
            }
            catch (Exception es)
            {
                fieldsCategoryListModel._message = es.Message;
                fieldsCategoryListModel._failure = true;
                fieldsCategoryListModel._tenantName = fieldsCategoryListViewModel._tenantName;
                fieldsCategoryListModel._token = fieldsCategoryListViewModel._token;
            }
            //fieldsCategoryListModel.fieldsCategoryList.ToList().ForEach(x => x.CustomFields.ToList().ForEach(y => y.FieldsCategory = null));
            return fieldsCategoryListModel;
        }
    }
}
