using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.CustomField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                int? MasterFieldId = Utility.GetMaxPK(this.context, new Func<CustomFields, int>(x => x.FieldId));
                customFieldAddViewModel.customFields.FieldId = (int)MasterFieldId;
                customFieldAddViewModel.customFields.LastUpdate = DateTime.UtcNow;
                this.context?.CustomFields.Add(customFieldAddViewModel.customFields);
                this.context?.SaveChanges();
                customFieldAddViewModel._failure = false;
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
        public CustomFieldAddViewModel ViewCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            CustomFieldAddViewModel customFieldView = new CustomFieldAddViewModel();
            try
            {
                var CustomField = this.context?.CustomFields.FirstOrDefault(x => x.TenantId == customFieldAddViewModel.customFields.TenantId && x.SchoolId == customFieldAddViewModel.customFields.SchoolId && x.FieldId == customFieldAddViewModel.customFields.FieldId);
                if (CustomField != null)
                {
                    customFieldView.customFields = CustomField;
                    customFieldView._failure = false;
                }
                else
                {
                    customFieldView._failure = true;
                    customFieldView._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                customFieldView._failure = true;
                customFieldView._message = es.Message;
            }
            return customFieldView;

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
        ///Get All Custom Field
        /// </summary>
        /// <param name="customField"></param>
        /// <returns></returns>
        public CustomFieldListViewModel GetAllCustomField(CustomFieldListViewModel customFieldListViewModel)
        {
            CustomFieldListViewModel customFieldList = new CustomFieldListViewModel();
            try
            {

                var CustomFieldAll = this.context?.CustomFields.Where(x => x.TenantId == customFieldListViewModel.TenantId && x.SchoolId == customFieldListViewModel.SchoolId).OrderBy(x => x.SortOrder).ToList();
                customFieldList.customFieldsList = CustomFieldAll;
                customFieldList._tenantName = customFieldListViewModel._tenantName;
                customFieldList._token = customFieldListViewModel._token;
                customFieldList._failure = false;
            }
            catch (Exception es)
            {
                customFieldList._message = es.Message;
                customFieldList._failure = true;
                customFieldList._tenantName = customFieldListViewModel._tenantName;
                customFieldList._token = customFieldListViewModel._token;
            }
            return customFieldList;

        }
    }
}
