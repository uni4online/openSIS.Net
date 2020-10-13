using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.MarkingPeriods.Interfaces;
using opensis.data.ViewModels.MarkingPeriods;
using opensis.data.ViewModels.Quarter;
using opensis.data.ViewModels.SchoolYear;
using opensis.data.ViewModels.ProgressPeriod;
using opensis.data.ViewModels.Semester;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/MarkingPeriod")]
    [ApiController]
    public class MarkingPeriodController : ControllerBase
    {
        private IMarkingPeriodService _markingPeriodService;
        public MarkingPeriodController(IMarkingPeriodService markingPeriodService)
        {
            _markingPeriodService = markingPeriodService;
        }
        [HttpPost("getMarkingPeriod")]

        public ActionResult<MarkingPeriod> GetMarkingPeriod(MarkingPeriod markingPeriod)
        {
            MarkingPeriod markingPeriodModel = new MarkingPeriod();
            try
            {
                markingPeriodModel = _markingPeriodService.GetMarkingPeriod(markingPeriod);
            }
            catch (Exception es)
            {
                markingPeriodModel._failure = true;
                markingPeriodModel._message = es.Message;
            }
            return markingPeriodModel;
        }
        [HttpPost("addSchoolYear")]
        public ActionResult<SchoolYearsAddViewModel> AddSchoolYear(SchoolYearsAddViewModel schoolYear)
        {
            SchoolYearsAddViewModel schoolYearAdd = new SchoolYearsAddViewModel();
            try
            {
                schoolYearAdd = _markingPeriodService.SaveSchoolYear(schoolYear);
            }
            catch (Exception es)
            {
                schoolYearAdd._failure = true;
                schoolYearAdd._message = es.Message;
            }
            return schoolYearAdd;
        }
        [HttpPost("viewSchoolYear")]

        public ActionResult<SchoolYearsAddViewModel> ViewSchoolYear(SchoolYearsAddViewModel schoolYear)
        {
            SchoolYearsAddViewModel SchoolYearsView = new SchoolYearsAddViewModel();
            try
            {
                SchoolYearsView = _markingPeriodService.ViewSchoolYear(schoolYear);
            }
            catch (Exception es)
            {
                SchoolYearsView._failure = true;
                SchoolYearsView._message = es.Message;
            }
            return SchoolYearsView;
        }
        [HttpPut("updateSchoolYear")]

        public ActionResult<SchoolYearsAddViewModel> UpdateSchoolYear(SchoolYearsAddViewModel schoolYear)
        {
            SchoolYearsAddViewModel SchoolYearsUpdate = new SchoolYearsAddViewModel();
            try
            {
                SchoolYearsUpdate = _markingPeriodService.UpdateSchoolYear(schoolYear);
            }
            catch (Exception es)
            {
                SchoolYearsUpdate._failure = true;
                SchoolYearsUpdate._message = es.Message;
            }
            return SchoolYearsUpdate;
        }
        [HttpPost("deleteSchoolYear")]

        public ActionResult<SchoolYearsAddViewModel> DeleteSchoolYear(SchoolYearsAddViewModel schoolYear)
        {
            SchoolYearsAddViewModel schoolYearlDelete = new SchoolYearsAddViewModel();
            try
            {
                schoolYearlDelete = _markingPeriodService.DeleteSchoolYear(schoolYear);
            }
            catch (Exception es)
            {
                schoolYearlDelete._failure = true;
                schoolYearlDelete._message = es.Message;
            }
            return schoolYearlDelete;
        }
        [HttpPost("addQuarter")]
        public ActionResult<QuarterAddViewModel> AddQuarter(QuarterAddViewModel quarter)
        {
            QuarterAddViewModel quarterAdd = new QuarterAddViewModel();
            try
            {
                quarterAdd = _markingPeriodService.SaveQuarter(quarter);
            }
            catch (Exception es)
            {
                quarterAdd._failure = true;
                quarterAdd._message = es.Message;
            }
            return quarterAdd;
        }

        [HttpPost("viewQuarter")]

        public ActionResult<QuarterAddViewModel> ViewQuarter(QuarterAddViewModel quarter)
        {
            QuarterAddViewModel quarterAdd = new QuarterAddViewModel();
            try
            {
                quarterAdd = _markingPeriodService.ViewQuarter(quarter);
            }
            catch (Exception es)
            {
                quarterAdd._failure = true;
                quarterAdd._message = es.Message;
            }
            return quarterAdd;
        }

        [HttpPut("updateQuarter")]

        public ActionResult<QuarterAddViewModel> UpdateQuarter(QuarterAddViewModel quarter)
        {
            QuarterAddViewModel quarterAdd = new QuarterAddViewModel();
            try
            {
                quarterAdd = _markingPeriodService.UpdateQuarter(quarter);
            }
            catch (Exception es)
            {
                quarterAdd._failure = true;
                quarterAdd._message = es.Message;
            }
            return quarterAdd;
        }

        [HttpPost("deleteQuarter")]

        public ActionResult<QuarterAddViewModel> DeleteQuarter(QuarterAddViewModel quarter)
        {
            QuarterAddViewModel quarterlDelete = new QuarterAddViewModel();
            try
            {
                quarterlDelete = _markingPeriodService.DeleteQuarter(quarter);
            }
            catch (Exception es)
            {
                quarterlDelete._failure = true;
                quarterlDelete._message = es.Message;
            }
            return quarterlDelete;
        }
        [HttpPost("addSemester")]
        public ActionResult<SemesterAddViewModel> AddSemester(SemesterAddViewModel semester)
        {
            SemesterAddViewModel semesterAdd = new SemesterAddViewModel();
            try
            {
                semesterAdd = _markingPeriodService.SaveSemester(semester);
            }
            catch (Exception es)
            {
                semesterAdd._failure = true;
                semesterAdd._message = es.Message;
            }
            return semesterAdd;
        }

        [HttpPut("updateSemester")]

        public ActionResult<SemesterAddViewModel> UpdateSemester(SemesterAddViewModel semester)
        {
            SemesterAddViewModel semesterUpdate = new SemesterAddViewModel();
            try
            {
                semesterUpdate = _markingPeriodService.UpdateSemester(semester);
            }
            catch (Exception es)
            {
                semesterUpdate._failure = true;
                semesterUpdate._message = es.Message;
            }
            return semesterUpdate;
        }


        [HttpPost("viewSemester")]

        public ActionResult<SemesterAddViewModel> ViewSemester(SemesterAddViewModel semester)
        {
            SemesterAddViewModel semesterView = new SemesterAddViewModel();
            try
            {
                semesterView = _markingPeriodService.ViewSemester(semester);
            }
            catch (Exception es)
            {
                semesterView._failure = true;
                semesterView._message = es.Message;
            }
            return semesterView;
        }

        [HttpPost("deleteSemester")]

        public ActionResult<SemesterAddViewModel> DeleteSemester(SemesterAddViewModel semester)
        {
            SemesterAddViewModel semesterDelete = new SemesterAddViewModel();
            try
            {
                semesterDelete = _markingPeriodService.DeleteSemester(semester);
            }
            catch (Exception es)
            {
                semesterDelete._failure = true;
                semesterDelete._message = es.Message;
            }
            return semesterDelete;
        }

        [HttpPost("addProgressPeriod")]
        public ActionResult<ProgressPeriodAddViewModel> AddProgressPeriod(ProgressPeriodAddViewModel progressPeriod)
        {
            ProgressPeriodAddViewModel progressPeriodAdd = new ProgressPeriodAddViewModel();
            try
            {
                progressPeriodAdd = _markingPeriodService.SaveProgressPeriod(progressPeriod);
            }
            catch (Exception es)
            {
                progressPeriodAdd._failure = true;
                progressPeriodAdd._message = es.Message;
            }
            return progressPeriodAdd;
        }

        [HttpPut("updateProgressPeriod")]

        public ActionResult<ProgressPeriodAddViewModel> UpdateProgressPeriod(ProgressPeriodAddViewModel progressPeriod)
        {
            ProgressPeriodAddViewModel progressUpdate = new ProgressPeriodAddViewModel();
            try
            {
                progressUpdate = _markingPeriodService.UpdateProgressPeriod(progressPeriod);
            }
            catch (Exception es)
            {
                progressUpdate._failure = true;
                progressUpdate._message = es.Message;
            }
            return progressUpdate;
        }

        [HttpPost("viewProgressPeriod")]

        public ActionResult<ProgressPeriodAddViewModel> ViewProgressPeriod(ProgressPeriodAddViewModel progressPeriod)
        {
            ProgressPeriodAddViewModel progressPeriodView = new ProgressPeriodAddViewModel();
            try
            {
                progressPeriodView = _markingPeriodService.ViewProgressPeriod(progressPeriod);
            }
            catch (Exception es)
            {
                progressPeriodView._failure = true;
                progressPeriodView._message = es.Message;
            }
            return progressPeriodView;
        }

        [HttpPost("deleteProgressPeriod")]

        public ActionResult<ProgressPeriodAddViewModel> DeleteProgressPeriod(ProgressPeriodAddViewModel progressPeriod)
        {
            ProgressPeriodAddViewModel progressPeriodDelete = new ProgressPeriodAddViewModel();
            try
            {
                progressPeriodDelete = _markingPeriodService.DeleteProgressPeriod(progressPeriod);
            }
            catch (Exception es)
            {
                progressPeriodDelete._failure = true;
                progressPeriodDelete._message = es.Message;
            }
            return progressPeriodDelete;
        }

    }
}
