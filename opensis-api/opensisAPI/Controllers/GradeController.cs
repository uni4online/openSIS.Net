using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.Grade.Interfaces;
using opensis.data.Models;
using opensis.data.ViewModels.Grades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Grade")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost("addGradeScale")]
        public ActionResult<GradeScaleAddViewModel> AddGradeScale(GradeScaleAddViewModel gradeScaleAddViewModel)
        {
            GradeScaleAddViewModel gradeScaleAdd = new GradeScaleAddViewModel();
            try
            {
                gradeScaleAdd = _gradeService.AddGradeScale(gradeScaleAddViewModel);
            }
            catch (Exception es)
            {
                gradeScaleAdd._failure = true;
                gradeScaleAdd._message = es.Message;
            }
            return gradeScaleAdd;
        }

        [HttpPut("updateGradeScale")]
        public ActionResult<GradeScaleAddViewModel> UpdateGradeScale(GradeScaleAddViewModel gradeScaleAddViewModel)
        {
            GradeScaleAddViewModel gradeScaleUpdate = new GradeScaleAddViewModel();
            try
            {
                gradeScaleUpdate = _gradeService.UpdateGradeScale(gradeScaleAddViewModel);
            }
            catch (Exception es)
            {
                gradeScaleUpdate._failure = true;
                gradeScaleUpdate._message = es.Message;
            }
            return gradeScaleUpdate;
        }

        [HttpPost("deleteGradeScale")]
        public ActionResult<GradeScaleAddViewModel> DeleteGradeScale(GradeScaleAddViewModel gradeScaleAddViewModel)
        {
            GradeScaleAddViewModel gradeScaleDelete = new GradeScaleAddViewModel();
            try
            {
                gradeScaleDelete = _gradeService.DeleteGradeScale(gradeScaleAddViewModel);
            }
            catch (Exception es)
            {
                gradeScaleDelete._failure = true;
                gradeScaleDelete._message = es.Message;
            }
            return gradeScaleDelete;
        }

        [HttpPost("addGrade")]
        public ActionResult<GradeAddViewModel> AddGrade(GradeAddViewModel gradeAddViewModel)
        {
            GradeAddViewModel gradeAdd = new GradeAddViewModel();
            try
            {
                gradeAdd = _gradeService.AddGrade(gradeAddViewModel);
            }
            catch (Exception es)
            {
                gradeAdd._failure = true;
                gradeAdd._message = es.Message;
            }
            return gradeAdd;
        }

        [HttpPut("updateGrade")]
        public ActionResult<GradeAddViewModel> UpdateGrade(GradeAddViewModel gradeAddViewModel)
        {
            GradeAddViewModel gradeUpdate = new GradeAddViewModel();
            try
            {
                gradeUpdate = _gradeService.UpdateGrade(gradeAddViewModel);
            }
            catch (Exception es)
            {
                gradeUpdate._failure = true;
                gradeUpdate._message = es.Message;
            }
            return gradeUpdate;
        }

        [HttpPost("deleteGrade")]
        public ActionResult<GradeAddViewModel> DeleteGrade(GradeAddViewModel gradeAddViewModel)
        {
            GradeAddViewModel gradeDelete = new GradeAddViewModel();
            try
            {
                gradeDelete = _gradeService.DeleteGrade(gradeAddViewModel);
            }
            catch (Exception es)
            {
                gradeDelete._failure = true;
                gradeDelete._message = es.Message;
            }
            return gradeDelete;
        }

        [HttpPost("getAllGradeScaleList")]
        public ActionResult<GradeScaleListViewModel> GetAllGradeScaleList(GradeScaleListViewModel gradeScaleListViewModel)
        {
            GradeScaleListViewModel gradeScaleList = new GradeScaleListViewModel();
            try
            {
                gradeScaleList = _gradeService.GetAllGradeScaleList(gradeScaleListViewModel);
            }
            catch (Exception es)
            {
                gradeScaleList._message = es.Message;
                gradeScaleList._failure = true;
            }
            return gradeScaleList;
        }

        [HttpPut("updateGradeSortOrder")]
        public ActionResult<GradeSortOrderModel> UpdateGradeSortOrder(GradeSortOrderModel gradeSortOrderModel)
        {
            GradeSortOrderModel gradeSortOrderUpdate = new GradeSortOrderModel();
            try
            {
                gradeSortOrderUpdate = _gradeService.UpdateGradeSortOrder(gradeSortOrderModel);
            }
            catch (Exception es)
            {
                gradeSortOrderUpdate._failure = true;
                gradeSortOrderUpdate._message = es.Message;
            }
            return gradeSortOrderUpdate;
        }

        [HttpPost("addEffortGradeLibraryCategory")]
        public ActionResult<EffortGradeLibraryCategoryAddViewModel> AddEffortGradeLibraryCategory(EffortGradeLibraryCategoryAddViewModel effortGradeLibraryCategoryAddViewModel)
        {
            EffortGradeLibraryCategoryAddViewModel EffortGradeLibraryCategoryAdd = new EffortGradeLibraryCategoryAddViewModel();
            try
            {
                EffortGradeLibraryCategoryAdd = _gradeService.AddEffortGradeLibraryCategory(effortGradeLibraryCategoryAddViewModel);
            }
            catch (Exception es)
            {
                EffortGradeLibraryCategoryAdd._failure = true;
                EffortGradeLibraryCategoryAdd._message = es.Message;
            }
            return EffortGradeLibraryCategoryAdd;
        }

        [HttpPut("updateEffortGradeLibraryCategory")]
        public ActionResult<EffortGradeLibraryCategoryAddViewModel> UpdateEffortGradeLibraryCategory(EffortGradeLibraryCategoryAddViewModel effortGradeLibraryCategoryAddViewModel)
        {
            EffortGradeLibraryCategoryAddViewModel EffortGradeLibraryCategoryUpdate = new EffortGradeLibraryCategoryAddViewModel();
            try
            {
                EffortGradeLibraryCategoryUpdate = _gradeService.UpdateEffortGradeLibraryCategory(effortGradeLibraryCategoryAddViewModel);
            }
            catch (Exception es)
            {
                EffortGradeLibraryCategoryUpdate._failure = true;
                EffortGradeLibraryCategoryUpdate._message = es.Message;
            }
            return EffortGradeLibraryCategoryUpdate;
        }

        [HttpPost("deleteEffortGradeLibraryCategory")]
        public ActionResult<EffortGradeLibraryCategoryAddViewModel> DeleteEffortGradeLibraryCategory(EffortGradeLibraryCategoryAddViewModel effortGradeLibraryCategoryAddViewModel)
        {
            EffortGradeLibraryCategoryAddViewModel effortGradeLibraryCategoryDelete = new EffortGradeLibraryCategoryAddViewModel();
            try
            {
                effortGradeLibraryCategoryDelete = _gradeService.DeleteEffortGradeLibraryCategory(effortGradeLibraryCategoryAddViewModel);
            }
            catch (Exception es)
            {
                effortGradeLibraryCategoryDelete._failure = true;
                effortGradeLibraryCategoryDelete._message = es.Message;
            }
            return effortGradeLibraryCategoryDelete;
        }

        [HttpPost("addEffortGradeLibraryCategoryItem")]
        public ActionResult<EffortGradeLibraryCategoryItemAddViewModel> AddEffortGradeLibraryCategoryItem(EffortGradeLibraryCategoryItemAddViewModel effortGradeLibraryCategoryItemAddViewModel)
        {
            EffortGradeLibraryCategoryItemAddViewModel EffortGradeLibraryCategoryItemAdd = new EffortGradeLibraryCategoryItemAddViewModel();
            try
            {
                EffortGradeLibraryCategoryItemAdd = _gradeService.AddEffortGradeLibraryCategoryItem(effortGradeLibraryCategoryItemAddViewModel);
            }
            catch (Exception es)
            {
                EffortGradeLibraryCategoryItemAdd._failure = true;
                EffortGradeLibraryCategoryItemAdd._message = es.Message;
            }
            return EffortGradeLibraryCategoryItemAdd;
        }

        [HttpPut("updateEffortGradeLibraryCategoryItem")]
        public ActionResult<EffortGradeLibraryCategoryItemAddViewModel> UpdateEffortGradeLibraryCategoryItem(EffortGradeLibraryCategoryItemAddViewModel effortGradeLibraryCategoryItemAddViewModel)
        {
            EffortGradeLibraryCategoryItemAddViewModel EffortGradeLibraryCategoryItemUpdate = new EffortGradeLibraryCategoryItemAddViewModel();
            try
            {
                EffortGradeLibraryCategoryItemUpdate = _gradeService.UpdateEffortGradeLibraryCategoryItem(effortGradeLibraryCategoryItemAddViewModel);
            }
            catch (Exception es)
            {
                EffortGradeLibraryCategoryItemUpdate._failure = true;
                EffortGradeLibraryCategoryItemUpdate._message = es.Message;
            }
            return EffortGradeLibraryCategoryItemUpdate;
        }

        [HttpPost("deleteEffortGradeLibraryCategoryItem")]
        public ActionResult<EffortGradeLibraryCategoryItemAddViewModel> DeleteEffortGradeLibraryCategoryItem(EffortGradeLibraryCategoryItemAddViewModel effortGradeLibraryCategoryItemAddViewModel)
        {
            EffortGradeLibraryCategoryItemAddViewModel effortGradeLibraryCategoryItemDelete = new EffortGradeLibraryCategoryItemAddViewModel();
            try
            {
                effortGradeLibraryCategoryItemDelete = _gradeService.DeleteEffortGradeLibraryCategoryItem(effortGradeLibraryCategoryItemAddViewModel);
            }
            catch (Exception es)
            {
                effortGradeLibraryCategoryItemDelete._failure = true;
                effortGradeLibraryCategoryItemDelete._message = es.Message;
            }
            return effortGradeLibraryCategoryItemDelete;
        }

        [HttpPost("getAllEffortGradeLlibraryCategoryList")]
        public ActionResult<EffortGradeLlibraryCategoryListViewModel> GetAllEffortGradeLlibraryCategoryList(EffortGradeLlibraryCategoryListViewModel effortGradeLlibraryCategoryListViewModel)
        {
            EffortGradeLlibraryCategoryListViewModel effortGradeLlibraryCategoryList = new EffortGradeLlibraryCategoryListViewModel();
            try
            {
                effortGradeLlibraryCategoryList = _gradeService.GetAllEffortGradeLlibraryCategoryList(effortGradeLlibraryCategoryListViewModel);
            }
            catch (Exception es)
            {
                effortGradeLlibraryCategoryList._message = es.Message;
                effortGradeLlibraryCategoryList._failure = true;
            }
            return effortGradeLlibraryCategoryList;
        }

        [HttpPut("updateEffortGradeLlibraryCategorySortOrder")]
        public ActionResult<EffortgradeLibraryCategorySortOrderModel> UpdateEffortGradeLlibraryCategorySortOrder(EffortgradeLibraryCategorySortOrderModel effortgradeLibraryCategorySortOrderModel)
        {
            EffortgradeLibraryCategorySortOrderModel effortgradeLibraryCategorySortOrderUpdate = new EffortgradeLibraryCategorySortOrderModel();
            try
            {
                effortgradeLibraryCategorySortOrderUpdate = _gradeService.UpdateEffortGradeLlibraryCategorySortOrder(effortgradeLibraryCategorySortOrderModel);
            }
            catch (Exception es)
            {
                effortgradeLibraryCategorySortOrderUpdate._failure = true;
                effortgradeLibraryCategorySortOrderUpdate._message = es.Message;
            }
            return effortgradeLibraryCategorySortOrderUpdate;
        }

        [HttpPost("addEffortGradeScale")]
        public ActionResult<EffortGradeScaleAddViewModel> AddEffortGradeScale(EffortGradeScaleAddViewModel effortGradeScaleAddViewModel)
        {
            EffortGradeScaleAddViewModel effortGradeScaleAdd = new EffortGradeScaleAddViewModel();
            try
            {
                effortGradeScaleAdd = _gradeService.AddEffortGradeScale(effortGradeScaleAddViewModel);
            }
            catch (Exception es)
            {
                effortGradeScaleAdd._failure = false;
                effortGradeScaleAdd._message = es.Message;
            }
            return effortGradeScaleAdd;
        }

        [HttpPut("updateEffortGradeScale")]
        public ActionResult<EffortGradeScaleAddViewModel> UpdateEffortGradeScale(EffortGradeScaleAddViewModel effortGradeScaleAddViewModel)
        {
            EffortGradeScaleAddViewModel effortGradeScaleUpdate = new EffortGradeScaleAddViewModel();
            try
            {
                effortGradeScaleUpdate = _gradeService.UpdateEffortGradeScale(effortGradeScaleAddViewModel);
            }
            catch (Exception es)
            {
                effortGradeScaleUpdate._failure = false;
                effortGradeScaleUpdate._message = es.Message;
            }
            return effortGradeScaleUpdate;
        }

        [HttpPost("deleteEffortGradeScale")]
        public ActionResult<EffortGradeScaleAddViewModel> DeleteEffortGradeScale(EffortGradeScaleAddViewModel effortGradeScaleAddViewModel)
        {
            EffortGradeScaleAddViewModel effortGradeScaleDelete = new EffortGradeScaleAddViewModel();
            try
            {
                effortGradeScaleDelete = _gradeService.DeleteEffortGradeScale(effortGradeScaleAddViewModel);
            }
            catch (Exception es)
            {
                effortGradeScaleDelete._failure = false;
                effortGradeScaleDelete._message = es.Message;
            }
            return effortGradeScaleDelete;
        }

        [HttpPost("getAllEffortGradeScaleList")]

        public ActionResult<EffortGradeScaleListModel> GetAllEffortGradeScale(PageResult pageResult)
        {
            EffortGradeScaleListModel effortGradeScaleList = new EffortGradeScaleListModel();
            try
            {
                effortGradeScaleList = _gradeService.GetAllEffortGradeScale(pageResult);
            }
            catch (Exception es)
            {
                effortGradeScaleList._message = es.Message;
                effortGradeScaleList._failure = true;
            }
            return effortGradeScaleList;
        }

        [HttpPut("updateEffortGradeScaleSortOrder")]
        public ActionResult<EffortGradeScaleSortOrderViewModel> UpdateEffortGradeScaleSortOrder(EffortGradeScaleSortOrderViewModel effortGradeScaleSortOrderViewModel)
        {
            EffortGradeScaleSortOrderViewModel effortGradeScaleSortOrder = new EffortGradeScaleSortOrderViewModel();
            try
            {
                effortGradeScaleSortOrder = _gradeService.UpdateEffortGradeScaleSortOrder(effortGradeScaleSortOrderViewModel);
            }
            catch (Exception es)
            {
                effortGradeScaleSortOrder._failure = true;
                effortGradeScaleSortOrder._message = es.Message;
            }
            return effortGradeScaleSortOrder;
        }
    }
}
