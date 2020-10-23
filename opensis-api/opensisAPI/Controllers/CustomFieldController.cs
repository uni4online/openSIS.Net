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


        [HttpPost("viewCustomField")]

        public ActionResult<CustomFieldAddViewModel> ViewCustomField(CustomFieldAddViewModel customFieldAddViewModel)
        {
            CustomFieldAddViewModel customFieldView = new CustomFieldAddViewModel();
            try
            {
                if (customFieldAddViewModel.customFields.SchoolId > 0)
                {
                    customFieldView = _customFieldService.ViewCustomField(customFieldAddViewModel);
                }
                else
                {
                    customFieldView._token = customFieldAddViewModel._token;
                    customFieldView._tenantName = customFieldAddViewModel._tenantName;
                    customFieldView._failure = true;
                    customFieldView._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                customFieldView._failure = true;
                customFieldView._message = es.Message;
            }
            return customFieldView;
        }


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



        [HttpPost("getAllCustomField")]

        public ActionResult<CustomFieldListViewModel> GetAllCustomField(CustomFieldListViewModel customFieldListViewModel)
        {
            CustomFieldListViewModel customFieldList = new CustomFieldListViewModel();
            try
            {
                if (customFieldListViewModel.SchoolId > 0)
                {
                    customFieldList = _customFieldService.GetAllCustomField(customFieldListViewModel);
                }
                else
                {
                    customFieldList._token = customFieldListViewModel._token;
                    customFieldList._tenantName = customFieldListViewModel._tenantName;
                    customFieldList._failure = true;
                    customFieldList._message = "Please enter valid scholl id";
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
