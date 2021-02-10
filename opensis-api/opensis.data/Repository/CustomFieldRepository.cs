﻿using opensis.data.Helper;
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
                    //int? MasterFieldId = Utility.GetMaxPK(this.context, new Func<CustomFields, int>(x => x.FieldId));

                    int? MasterFieldId = 1;
                    int? SortOrder = 1;

                    var CustomFieldData = this.context?.CustomFields.Where(x => x.SchoolId == customFieldAddViewModel.customFields.SchoolId && x.TenantId == customFieldAddViewModel.customFields.TenantId).OrderByDescending(x => x.FieldId).FirstOrDefault();

                    if (CustomFieldData != null)
                    {
                        MasterFieldId = CustomFieldData.FieldId + 1;
                    }

                    var SortOrderData = this.context?.CustomFields.Where(x => x.SchoolId == customFieldAddViewModel.customFields.SchoolId && x.TenantId == customFieldAddViewModel.customFields.TenantId && x.CategoryId == customFieldAddViewModel.customFields.CategoryId).OrderByDescending(x => x.SortOrder).FirstOrDefault();

                    if (SortOrderData != null)
                    {
                        SortOrder = SortOrderData.SortOrder + 1;
                    }

                    customFieldAddViewModel.customFields.FieldId = (int)MasterFieldId;
                    customFieldAddViewModel.customFields.SortOrder = (int)SortOrder;
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
                    
                    customFieldAddViewModel.customFields.LastUpdate = DateTime.Now;
                    customFieldAddViewModel.customFields.SortOrder = customFieldUpdate.SortOrder;
                    customFieldAddViewModel.customFields.Module = customFieldUpdate.Module;
                    this.context.Entry(customFieldUpdate).CurrentValues.SetValues(customFieldAddViewModel.customFields);
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
                if (customFieldDelete != null)
                {
                    var customFieldsValueExists = this.context?.CustomFieldsValue.FirstOrDefault(x => x.TenantId == customFieldDelete.TenantId && x.SchoolId == customFieldDelete.SchoolId && x.CategoryId == customFieldDelete.CategoryId && x.FieldId == customFieldDelete.FieldId);
                    if (customFieldsValueExists != null)
                    {
                        customFieldAddViewModel._failure = true;
                        customFieldAddViewModel._message = "It Can't Be Deleted Because It Has Association";
                    }
                    else
                    {
                        this.context?.CustomFields.Remove(customFieldDelete);
                        this.context?.SaveChanges();
                        customFieldAddViewModel._failure = false;
                        customFieldAddViewModel._message = "Deleted";
                    }
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
        /// Add FieldsCategory
        /// </summary>
        /// <param name="fieldsCategoryAddViewModel"></param>
        /// <returns></returns>
        public FieldsCategoryAddViewModel AddFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        {
            //int? CategoryId = Utility.GetMaxPK(this.context, new Func<FieldsCategory, int>(x => x.CategoryId));
            int? CategoryId = 1;

            var FieldCategoryData = this.context?.FieldsCategory.Where(x => x.SchoolId == fieldsCategoryAddViewModel.fieldsCategory.SchoolId && x.TenantId == fieldsCategoryAddViewModel.fieldsCategory.TenantId).OrderByDescending(x => x.CategoryId).FirstOrDefault();

            if (FieldCategoryData != null)
            {
                CategoryId = FieldCategoryData.CategoryId + 1;
            }

            fieldsCategoryAddViewModel.fieldsCategory.CategoryId = (int)CategoryId;
            fieldsCategoryAddViewModel.fieldsCategory.LastUpdate = DateTime.UtcNow;
            this.context?.FieldsCategory.Add(fieldsCategoryAddViewModel.fieldsCategory);
            this.context?.SaveChanges();
            fieldsCategoryAddViewModel._failure = false;

            return fieldsCategoryAddViewModel;
        }
        
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
                
                fieldsCategoryAddViewModel.fieldsCategory.LastUpdate = DateTime.Now;
                this.context.Entry(fieldsCategoryUpdate).CurrentValues.SetValues(fieldsCategoryAddViewModel.fieldsCategory);
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
                if(fieldsCategoryDelete!= null)
                {
                    var customFieldsExists = this.context?.CustomFields.FirstOrDefault(x => x.TenantId == fieldsCategoryDelete.TenantId && x.SchoolId == fieldsCategoryDelete.SchoolId && x.CategoryId == fieldsCategoryDelete.CategoryId);
                    if(customFieldsExists!=null)
                    {
                        fieldsCategoryAddViewModel._failure = true;
                        fieldsCategoryAddViewModel._message = "It Can't Be Deleted Because It Has Association";
                    }
                    else
                    {
                        this.context?.FieldsCategory.Remove(fieldsCategoryDelete);
                        this.context?.SaveChanges();
                        fieldsCategoryAddViewModel._failure = false;
                        fieldsCategoryAddViewModel._message = "Deleted";
                    }
                }                              
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
                    .Where(x => x.TenantId == fieldsCategoryListViewModel.TenantId && 
                                x.SchoolId == fieldsCategoryListViewModel.SchoolId && 
                                x.Module== fieldsCategoryListViewModel.Module)
                    .OrderByDescending(x => x.IsSystemCategory).ThenBy(x=>x.SortOrder).ToList();
                    
                if (fieldsCategoryList.Count > 0)
                {
                    foreach (var fieldsCategory in fieldsCategoryList)
                    {
                        fieldsCategory.CustomFields = fieldsCategory.CustomFields.OrderByDescending(y => y.SystemField).ThenBy(y => y.SortOrder).ToList();
                    }
                    
                }
                fieldsCategoryListModel.fieldsCategoryList = fieldsCategoryList;
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

        /// <summary>
        /// Update CustomField Sort Order
        /// </summary>
        /// <param name="customFieldSortOrderModel"></param>
        /// <returns></returns>

        public CustomFieldSortOrderModel UpdateCustomFieldSortOrder(CustomFieldSortOrderModel customFieldSortOrderModel)
        {            
            try
            {
                var customFieldRecords = new List<CustomFields>();

                var targetCustomField = this.context?.CustomFields.FirstOrDefault(x => x.SortOrder == customFieldSortOrderModel.PreviousSortOrder && x.SchoolId == customFieldSortOrderModel.SchoolId && x.CategoryId== customFieldSortOrderModel.CategoryId);
                targetCustomField.SortOrder = customFieldSortOrderModel.CurrentSortOrder;

                if (customFieldSortOrderModel.PreviousSortOrder > customFieldSortOrderModel.CurrentSortOrder)
                {
                    customFieldRecords = this.context?.CustomFields.Where(x => x.SortOrder >= customFieldSortOrderModel.CurrentSortOrder && x.SortOrder < customFieldSortOrderModel.PreviousSortOrder && x.SchoolId == customFieldSortOrderModel.SchoolId && x.CategoryId == customFieldSortOrderModel.CategoryId).ToList();

                    if (customFieldRecords.Count > 0)
                    {
                        customFieldRecords.ForEach(x => x.SortOrder = x.SortOrder + 1);
                    }
                }
                if (customFieldSortOrderModel.CurrentSortOrder > customFieldSortOrderModel.PreviousSortOrder)
                {
                    customFieldRecords = this.context?.CustomFields.Where(x => x.SortOrder <= customFieldSortOrderModel.CurrentSortOrder && x.SortOrder > customFieldSortOrderModel.PreviousSortOrder && x.SchoolId == customFieldSortOrderModel.SchoolId && x.CategoryId == customFieldSortOrderModel.CategoryId).ToList();
                    if (customFieldRecords.Count > 0)
                    {
                        customFieldRecords.ForEach(x => x.SortOrder = x.SortOrder - 1);
                    }
                }
                this.context?.SaveChanges();
                customFieldSortOrderModel._failure = false;
            }
            catch (Exception es)
            {
                customFieldSortOrderModel._message = es.Message;
                customFieldSortOrderModel._failure = true;                
            }
            return customFieldSortOrderModel;
        }

    }
}
