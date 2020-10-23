using opensis.core.CustomField.Interfaces;
using opensis.core.helper;
using opensis.data.Interface;
using opensis.data.ViewModels.CustomField;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.CustomField.Services
{
    public class CustomFieldService : ICustomFieldService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public ICustomFieldRepository customFieldRepository;
        public CustomFieldService(ICustomFieldRepository customFieldRepository)
        {
            this.customFieldRepository = customFieldRepository;
        }

        //Required for Unit Testing
        public CustomFieldService() { }

        /// <summary>
        /// Add Custom Field
        /// </summary>
        /// <param name="customFieldAddViewModel"></param>
        /// <returns></returns>
        public CustomFieldAddViewModel SaveCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            CustomFieldAddViewModel customFieldAdd = new CustomFieldAddViewModel();
            if (TokenManager.CheckToken(customFieldAddViewModel._tenantName, customFieldAddViewModel._token))
            {
                customFieldAdd = this.customFieldRepository.AddCustomField(customFieldAddViewModel);
            }
            else
            {
                customFieldAdd._failure = true;
                customFieldAdd._message = TOKENINVALID;
            }
            return customFieldAdd;
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
                if (TokenManager.CheckToken(customFieldAddViewModel._tenantName, customFieldAddViewModel._token))
                {
                    customFieldView = this.customFieldRepository.ViewCustomField(customFieldAddViewModel);
                }
                else
                {
                    customFieldView._failure = true;
                    customFieldView._message = TOKENINVALID;
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
            CustomFieldAddViewModel customFieldUpdate = new CustomFieldAddViewModel();
            try
            {
                if (TokenManager.CheckToken(customFieldAddViewModel._tenantName, customFieldAddViewModel._token))
                {
                    customFieldUpdate = this.customFieldRepository.UpdateCustomField(customFieldAddViewModel);
                }
                else
                {
                    customFieldUpdate._failure = true;
                    customFieldUpdate._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                customFieldUpdate._failure = true;
                customFieldUpdate._message = es.Message;
            }
            return customFieldUpdate;
        }

        /// <summary>
        /// Delete Custom Field
        /// </summary>
        /// <param name="customFieldAddViewModel"></param>
        /// <returns></returns>
        public CustomFieldAddViewModel DeleteCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            CustomFieldAddViewModel customFieldDelete = new CustomFieldAddViewModel();
            try
            {
                if (TokenManager.CheckToken(customFieldAddViewModel._tenantName, customFieldAddViewModel._token))
                {
                    customFieldDelete = this.customFieldRepository.DeleteCustomField(customFieldAddViewModel);
                }
                else
                {
                    customFieldDelete._failure = true;
                    customFieldDelete._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                customFieldDelete._failure = true;
                customFieldDelete._message = es.Message;
            }
            return customFieldDelete;
        }


        /// <summary>
        /// Get All Custom Field
        /// </summary>
        /// <param name="customField"></param>
        /// <returns></returns>
        public CustomFieldListViewModel GetAllCustomField(CustomFieldListViewModel customFieldListViewModel)
        {
            CustomFieldListViewModel customFieldList = new CustomFieldListViewModel();
            try
            {
                if (TokenManager.CheckToken(customFieldListViewModel._tenantName, customFieldListViewModel._token))
                {
                    customFieldList = this.customFieldRepository.GetAllCustomField(customFieldListViewModel);
                }
                else
                {
                    customFieldList._failure = true;
                    customFieldList._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                customFieldList._failure = true;
                customFieldList._message = es.Message;
            }

            return customFieldList;
        }


    }
}
