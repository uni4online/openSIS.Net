using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.CustomField.Interfaces;
using opensis.data.ViewModels.CustomField;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/CustomField")]
    [ApiController]
    public class CustomFieldController : ControllerBase
    {
        private ICustomFieldService _customFieldService;
        public CustomFieldController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }

        [HttpPost("addCustomField")]
        public ActionResult<CustomFieldAddViewModel> AddCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            CustomFieldAddViewModel customFieldAdd = new CustomFieldAddViewModel();
            try
            {
                if (customFieldAddViewModel.customFields.SchoolId > 0)
                {
                    customFieldAdd = _customFieldService.SaveCustomField(customFieldAddViewModel);
                }
                else
                {
                    customFieldAdd._token = customFieldAddViewModel._token;
                    customFieldAdd._tenantName = customFieldAddViewModel._tenantName;
                    customFieldAdd._failure = true;
                    customFieldAdd._message = "Please enter valid school id";
                }
            }
            catch (Exception es)
            {
                customFieldAdd._failure = true;
                customFieldAdd._message = es.Message;
            }
            return customFieldAdd;
        }


        //[HttpPost("viewCustomField")]

        //public ActionResult<CustomFieldAddViewModel> ViewCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        //{
        //    CustomFieldAddViewModel customFieldView = new CustomFieldAddViewModel();
        //    try
        //    {
        //        if (customFieldAddViewModel.customFields.SchoolId > 0)
        //        {
        //            customFieldView = _customFieldService.ViewCustomField(customFieldAddViewModel);
        //        }
        //        else
        //        {
        //            customFieldView._token = customFieldAddViewModel._token;
        //            customFieldView._tenantName = customFieldAddViewModel._tenantName;
        //            customFieldView._failure = true;
        //            customFieldView._message = "Please enter valid scholl id";
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        customFieldView._failure = true;
        //        customFieldView._message = es.Message;
        //    }
        //    return customFieldView;
        //}


        [HttpPut("updateCustomField")]

        public ActionResult<CustomFieldAddViewModel> UpdateCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            CustomFieldAddViewModel customFieldUpdate = new CustomFieldAddViewModel();
            try
            {
                customFieldUpdate = _customFieldService.UpdateCustomField(customFieldAddViewModel);
            }
            catch (Exception es)
            {
                customFieldUpdate._failure = true;
                customFieldUpdate._message = es.Message;
            }
            return customFieldUpdate;
        }

        [HttpPost("deleteCustomField")]

        public ActionResult<CustomFieldAddViewModel> DeleteCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            CustomFieldAddViewModel customFieldDelete = new CustomFieldAddViewModel();
            try
            {
                customFieldDelete = _customFieldService.DeleteCustomField(customFieldAddViewModel);
            }
            catch (Exception es)
            {
                customFieldDelete._failure = true;
                customFieldDelete._message = es.Message;
            }
            return customFieldDelete;
        }

        [HttpPost("addFieldsCategory")]
        public ActionResult<FieldsCategoryAddViewModel> AddFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        {
            FieldsCategoryAddViewModel fieldsCategoryAdd = new FieldsCategoryAddViewModel();
            try
            {
                fieldsCategoryAdd = _customFieldService.SaveFieldsCategory(fieldsCategoryAddViewModel);
            }
            catch (Exception es)
            {
                fieldsCategoryAdd._failure = true;
                fieldsCategoryAdd._message = es.Message;
            }
            return fieldsCategoryAdd;
        }
        //[HttpPost("viewFieldsCategory")]

        //public ActionResult<FieldsCategoryAddViewModel> ViewFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        //{
        //    FieldsCategoryAddViewModel fieldsCategoryView = new FieldsCategoryAddViewModel();
        //    try
        //    {
        //        fieldsCategoryView = _customFieldService.ViewFieldsCategory(fieldsCategoryAddViewModel);
        //    }
        //    catch (Exception es)
        //    {
        //        fieldsCategoryView._failure = true;
        //        fieldsCategoryView._message = es.Message;
        //    }
        //    return fieldsCategoryView;
        //}
        [HttpPut("updateFieldsCategory")]

        public ActionResult<FieldsCategoryAddViewModel> UpdateFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        {
            FieldsCategoryAddViewModel fieldsCategoryUpdate = new FieldsCategoryAddViewModel();
            try
            {
                fieldsCategoryUpdate = _customFieldService.UpdateFieldsCategory(fieldsCategoryAddViewModel);
            }
            catch (Exception es)
            {
                fieldsCategoryUpdate._failure = true;
                fieldsCategoryUpdate._message = es.Message;
            }
            return fieldsCategoryUpdate;
        }
        [HttpPost("deleteFieldsCategory")]

        public ActionResult<FieldsCategoryAddViewModel> DeleteFieldsCategory(FieldsCategoryAddViewModel fieldsCategoryAddViewModel)
        {
            FieldsCategoryAddViewModel fieldsCategorylDelete = new FieldsCategoryAddViewModel();
            try
            {
                fieldsCategorylDelete = _customFieldService.DeleteFieldsCategory(fieldsCategoryAddViewModel);
            }
            catch (Exception es)
            {
                fieldsCategorylDelete._failure = true;
                fieldsCategorylDelete._message = es.Message;
            }
            return fieldsCategorylDelete;
        }
        [HttpPost("getAllFieldsCategory")]

        public ActionResult<FieldsCategoryListViewModel> GetAllFieldsCategory(FieldsCategoryListViewModel fieldsCategoryListViewModel)
        {
            FieldsCategoryListViewModel fieldsCategoryList = new FieldsCategoryListViewModel();
            try
            {
                if (fieldsCategoryListViewModel.SchoolId > 0)
                {
                    fieldsCategoryList = _customFieldService.GetAllFieldsCategory(fieldsCategoryListViewModel);
                }
                else
                {
                    fieldsCategoryList._token = fieldsCategoryListViewModel._token;
                    fieldsCategoryList._tenantName = fieldsCategoryListViewModel._tenantName;
                    fieldsCategoryList._failure = true;
                    fieldsCategoryList._message = "Please enter valid school id";
                }
            }
            catch (Exception es)
            {
                fieldsCategoryList._message = es.Message;
                fieldsCategoryList._failure = true;
            }
            return fieldsCategoryList;
        }

    }
}
