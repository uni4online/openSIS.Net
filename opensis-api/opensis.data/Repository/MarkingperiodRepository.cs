using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.MarkingPeriods;
using opensis.data.ViewModels.Quarter;
using opensis.data.ViewModels.SchoolYear;
using opensis.data.ViewModels.ProgressPeriod;
using opensis.data.ViewModels.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace opensis.data.Repository
{
    public class MarkingPeriodRepository : IMarkingperiodRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public MarkingPeriodRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        public MarkingPeriod GetMarkingPeriod(MarkingPeriod markingPeriod)
        {
            MarkingPeriod markingPeriodModel = new MarkingPeriod();
            try
            {
                var MarkingperiodViews = this.context?.SchoolYears.Where(x=>x.SchoolId==markingPeriod.SchoolId && x.TenantId==markingPeriod.TenantId && x.AcademicYear==markingPeriod.AcademicYear).Select(x => new SchoolYearView()
                {
                    SchoolId = x.SchoolId,
                    MarkingPeriodId = x.MarkingPeriodId,
                    ShortName = x.ShortName,
                    Title = x.Title,
                    TenantId = x.TenantId,
                    IsParent = true,
                    DoesComments=x.DoesComments,
                    DoesExam=x.DoesExam,
                    DoesGrades=x.DoesGrades,
                    EndDate=x.EndDate,
                    PostStartDate=x.PostStartDate,
                    PostEndDate=x.PostEndDate,
                    StartDate=x.StartDate,
                    Children = this.context.Semesters.Where(y => y.YearId == x.MarkingPeriodId && y.AcademicYear == markingPeriod.AcademicYear).Select(y => new SchoolSemesterView()
                    { TenantId = y.TenantId,DoesComments=y.DoesComments,DoesExam=y.DoesExam,DoesGrades=y.DoesGrades,StartDate=y.StartDate,EndDate=y.EndDate,PostStartDate=y.PostStartDate,PostEndDate=y.PostEndDate, Title = y.Title, SchoolId = y.SchoolId, YearId = (int)y.YearId, MarkingPeriodId = y.MarkingPeriodId, IsParent = false, ShortName = y.ShortName,
                    Children=this.context.Quarters.Where(z=>z.SemesterId==y.MarkingPeriodId && z.AcademicYear == markingPeriod.AcademicYear).Select(z=> new SchoolQuarterView() {MarkingPeriodId=z.MarkingPeriodId,SemesterId=(int)z.SemesterId,IsParent=false,SchoolId=z.SchoolId,
                    Title=z.Title,ShortName=z.ShortName,TenantId=z.TenantId,DoesComments=z.DoesComments,DoesExam=z.DoesExam,DoesGrades=z.DoesGrades,StartDate=z.StartDate,EndDate=z.EndDate,PostStartDate=z.PostStartDate,PostEndDate=z.PostEndDate,
                    Children=this.context.ProgressPeriods.Where(a=>a.QuarterId==z.MarkingPeriodId && a.AcademicYear == markingPeriod.AcademicYear).Select(a=> new SchoolProgressPeriodView() {IsParent=false,MarkingPeriodId=a.MarkingPeriodId,SchoolId=a.SchoolId,
                    QuarterId=a.QuarterId,ShortName=a.ShortName,TenantId=a.TenantId,Title=a.Title,DoesComments=a.DoesComments,DoesExam=a.DoesExam,DoesGrades=a.DoesGrades,StartDate=a.StartDate,EndDate=a.EndDate,PostStartDate=a.PostStartDate,PostEndDate=a.PostEndDate}).ToList() }).ToList() }).ToList() }).ToList();
                markingPeriodModel.schoolYearsView = MarkingperiodViews;
                markingPeriodModel._tenantName = markingPeriod._tenantName;
                markingPeriodModel._token = markingPeriod._token;
                markingPeriodModel._failure = false;
                markingPeriodModel.TenantId = markingPeriod.TenantId;
                markingPeriodModel.SchoolId = markingPeriod.SchoolId;
            }
            catch (Exception es)
            {
                markingPeriodModel._failure = true;
                markingPeriodModel._message = es.Message;
            }
            return markingPeriodModel;
        }

        public SemesterAddViewModel AddSemester(SemesterAddViewModel semester)
        {
            try
            {
                var SchoolYear = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == semester.tableSemesters.TenantId && x.SchoolId == semester.tableSemesters.SchoolId && x.MarkingPeriodId == semester.tableSemesters.YearId);
                if (SchoolYear.StartDate <= semester.tableSemesters.StartDate && SchoolYear.EndDate >= semester.tableSemesters.EndDate)
                {

                    int? MasterMarkingPeriodId = Utility.GetMaxPK(this.context, new Func<Semesters, int>(x => x.MarkingPeriodId));
                    semester.tableSemesters.MarkingPeriodId = (int)MasterMarkingPeriodId;
                    semester.tableSemesters.AcademicYear = SchoolYear.AcademicYear;
                    semester.tableSemesters.LastUpdated = DateTime.UtcNow;
                    this.context?.Semesters.Add(semester.tableSemesters);
                    this.context?.SaveChanges();
                    semester._failure = false;
                    semester.tableSemesters.SchoolYears = null;
                }
                else
                {
                    semester._failure = true;
                    semester._message = "Start Date And End Date of Semester Should Be Between Start Date And End Date of SchoolYear";
                }                
            }
            catch (Exception ex)
            {
                semester.tableSemesters = null;
                semester._failure = true;
                semester._message = NORECORDFOUND;                
            }
            return semester;
            
        }

        public SemesterAddViewModel UpdateSemester(SemesterAddViewModel semester)
        {
            try
            {
                var semesterUpdate = this.context?.Semesters.FirstOrDefault(x => x.TenantId == semester.tableSemesters.TenantId && x.SchoolId == semester.tableSemesters.SchoolId && x.MarkingPeriodId == semester.tableSemesters.MarkingPeriodId);
                var SchoolYear = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == semesterUpdate.TenantId && x.SchoolId == semesterUpdate.SchoolId && x.MarkingPeriodId == semesterUpdate.YearId);

                if(SchoolYear.StartDate <= semesterUpdate.StartDate && SchoolYear.EndDate >= semesterUpdate.EndDate)
                { 
                    semesterUpdate.TenantId = semester.tableSemesters.TenantId;
                    semesterUpdate.SchoolId = semester.tableSemesters.SchoolId;
                    semesterUpdate.MarkingPeriodId = semester.tableSemesters.MarkingPeriodId;
                    semesterUpdate.AcademicYear = SchoolYear.AcademicYear;
                    semesterUpdate.YearId = semester.tableSemesters.YearId;
                    semesterUpdate.Title = semester.tableSemesters.Title;
                    semesterUpdate.ShortName = semester.tableSemesters.ShortName;
                    semesterUpdate.SortOrder = semester.tableSemesters.SortOrder;
                    semesterUpdate.StartDate = semester.tableSemesters.StartDate;
                    semesterUpdate.EndDate = semester.tableSemesters.EndDate;
                    semesterUpdate.PostStartDate = semester.tableSemesters.PostStartDate;
                    semesterUpdate.PostEndDate = semester.tableSemesters.PostEndDate;
                    semesterUpdate.DoesGrades = semester.tableSemesters.DoesGrades;
                    semesterUpdate.DoesExam = semester.tableSemesters.DoesExam;
                    semesterUpdate.DoesComments = semester.tableSemesters.DoesComments;
                    semesterUpdate.RolloverId = semester.tableSemesters.RolloverId;
                    semesterUpdate.LastUpdated = semester.tableSemesters.LastUpdated;
                    semesterUpdate.UpdatedBy = semester.tableSemesters.UpdatedBy;

                this.context?.SaveChanges();

                semester._failure = false;
                }
                else
                {
                    semester._failure = true;
                    semester._message = "Start Date And End Date of Semester Should Be Between Start Date And End Date of SchoolYear";
                }
                
            }
            catch (Exception ex)
            {
                semester.tableSemesters = null;
                semester._failure = true;
                semester._message = ex.Message;                
            }
            return semester;

        }
        public SemesterAddViewModel ViewSemester(SemesterAddViewModel semester)
        {
            SemesterAddViewModel semesterView = new SemesterAddViewModel();
            try
            {                
                var semesterById = this.context?.Semesters.FirstOrDefault(x => x.TenantId == semester.tableSemesters.TenantId && x.SchoolId == semester.tableSemesters.SchoolId && x.MarkingPeriodId == semester.tableSemesters.MarkingPeriodId);
                if (semesterById != null)
                {
                    semesterView.tableSemesters = semesterById;                    
                }
                else
                {
                    semesterView._failure = true;
                    semesterView._message = NORECORDFOUND;
                    
                }
            }
            catch (Exception es)
            {

                semester._failure = true;
                semester._message = es.Message;
            }
            return semesterView;
        }
        public SemesterAddViewModel DeleteSemester(SemesterAddViewModel semester)
        {
            try
            {
                var semesterDelete = this.context?.Semesters.FirstOrDefault(x => x.TenantId == semester.tableSemesters.TenantId && x.SchoolId == semester.tableSemesters.SchoolId && x.MarkingPeriodId == semester.tableSemesters.MarkingPeriodId);
               
                var QuatersExist = this.context?.Quarters.FirstOrDefault(x => x.TenantId == semesterDelete.TenantId && x.SchoolId == semesterDelete.SchoolId && x.SemesterId == semesterDelete.MarkingPeriodId);
                if (QuatersExist != null)
                {
                    semester.tableSemesters = null;
                    semester._message = "Semester cannot be deleted because it has its association";
                    semester._failure = true;
                }
                else
                {
                    this.context?.Semesters.Remove(semesterDelete);
                    this.context?.SaveChanges();
                    semester._failure = false;
                    semester._message = "Deleted";
                }


            }
            catch (Exception es)
            {
                semester._failure = true;
                semester._message = es.Message;
            }
            return semester;
        }

        public ProgressPeriodAddViewModel AddProgressPeriod(ProgressPeriodAddViewModel progressPeriod)
        {
            var Quarter = this.context?.Quarters.FirstOrDefault(x => x.TenantId == progressPeriod.tableProgressPeriods.TenantId && x.SchoolId == progressPeriod.tableProgressPeriods.SchoolId && x.MarkingPeriodId == progressPeriod.tableProgressPeriods.QuarterId);
            if (Quarter.StartDate <= progressPeriod.tableProgressPeriods.StartDate && Quarter.EndDate >= progressPeriod.tableProgressPeriods.EndDate)
            { 
                int? MasterMarkingPeriodId = Utility.GetMaxPK(this.context, new Func<ProgressPeriods, int>(x => x.MarkingPeriodId));
                progressPeriod.tableProgressPeriods.MarkingPeriodId = (int)MasterMarkingPeriodId;
                progressPeriod.tableProgressPeriods.AcademicYear = (decimal)Quarter.AcademicYear;
                progressPeriod.tableProgressPeriods.LastUpdated = DateTime.UtcNow;
                this.context?.ProgressPeriods.Add(progressPeriod.tableProgressPeriods);
                this.context?.SaveChanges();
                progressPeriod._failure = false;
                progressPeriod.tableProgressPeriods.Quarters = null;
            }
            else
            {
                progressPeriod._failure = true;
                progressPeriod._message = "Start Date And End Date of ProgressPeriod Should Be Between Start Date And End Date of Quarter";
            }
            return progressPeriod;
        }

        public ProgressPeriodAddViewModel UpdateProgressPeriod(ProgressPeriodAddViewModel progressPeriod)
        {
            try
            {
                var progressUpdate = this.context?.ProgressPeriods.FirstOrDefault(x => x.TenantId == progressPeriod.tableProgressPeriods.TenantId && x.SchoolId == progressPeriod.tableProgressPeriods.SchoolId && x.MarkingPeriodId == progressPeriod.tableProgressPeriods.MarkingPeriodId);
                var Quarter = this.context?.Quarters.FirstOrDefault(x => x.TenantId == progressPeriod.tableProgressPeriods.TenantId && x.SchoolId == progressPeriod.tableProgressPeriods.SchoolId && x.MarkingPeriodId == progressPeriod.tableProgressPeriods.QuarterId);
                if (Quarter.StartDate <= progressPeriod.tableProgressPeriods.StartDate && Quarter.EndDate >= progressPeriod.tableProgressPeriods.EndDate)
                {
                    progressUpdate.TenantId = progressPeriod.tableProgressPeriods.TenantId;
                    progressUpdate.SchoolId = progressPeriod.tableProgressPeriods.SchoolId;
                    progressUpdate.MarkingPeriodId = progressPeriod.tableProgressPeriods.MarkingPeriodId;
                    progressUpdate.AcademicYear = (decimal)Quarter.AcademicYear;
                    progressUpdate.QuarterId = progressPeriod.tableProgressPeriods.QuarterId;
                    progressUpdate.Title = progressPeriod.tableProgressPeriods.Title;
                    progressUpdate.ShortName = progressPeriod.tableProgressPeriods.ShortName;
                    progressUpdate.SortOrder = progressPeriod.tableProgressPeriods.SortOrder;
                    progressUpdate.StartDate = progressPeriod.tableProgressPeriods.StartDate;
                    progressUpdate.EndDate = progressPeriod.tableProgressPeriods.EndDate;
                    progressUpdate.PostStartDate = progressPeriod.tableProgressPeriods.PostStartDate;
                    progressUpdate.PostEndDate = progressPeriod.tableProgressPeriods.PostEndDate;
                    progressUpdate.DoesGrades = progressPeriod.tableProgressPeriods.DoesGrades;
                    progressUpdate.DoesExam = progressPeriod.tableProgressPeriods.DoesExam;
                    progressUpdate.DoesComments = progressPeriod.tableProgressPeriods.DoesComments;
                    progressUpdate.RolloverId = progressPeriod.tableProgressPeriods.RolloverId;
                    progressUpdate.LastUpdated = progressPeriod.tableProgressPeriods.LastUpdated;
                    progressUpdate.UpdatedBy = progressPeriod.tableProgressPeriods.UpdatedBy;
                    this.context?.SaveChanges();

                    progressPeriod._failure = false;
                }
                else
                {
                    progressPeriod._failure = false;
                    progressPeriod._message = "Start Date And End Date of ProgressPeriod Should Be Between Start Date And End Date of Quarter";
                }
                
            }
            catch (Exception ex)
            {
                progressPeriod.tableProgressPeriods = null;
                progressPeriod._failure = true;
                progressPeriod._message = ex.Message;                
            }
            return progressPeriod;
        }

        public ProgressPeriodAddViewModel ViewProgressPeriod(ProgressPeriodAddViewModel progressPeriod)
        {
            ProgressPeriodAddViewModel ProgressPeriodView = new ProgressPeriodAddViewModel();
            try
            {                
                var ProgressPeriodById = this.context?.ProgressPeriods.FirstOrDefault(x => x.TenantId == progressPeriod.tableProgressPeriods.TenantId && x.SchoolId == progressPeriod.tableProgressPeriods.SchoolId && x.MarkingPeriodId == progressPeriod.tableProgressPeriods.MarkingPeriodId);
                if (ProgressPeriodById != null)
                {
                    ProgressPeriodView.tableProgressPeriods = ProgressPeriodById;                    
                }
                else
                {
                    ProgressPeriodView._failure = true;
                    ProgressPeriodView._message = NORECORDFOUND;
                    
                }
            }
            catch (Exception es)
            {

                progressPeriod._failure = true;
                progressPeriod._message = es.Message;
            }
            return ProgressPeriodView;
        }

        public ProgressPeriodAddViewModel DeleteProgressPeriod(ProgressPeriodAddViewModel progressPeriod)
        {
            try
            {
                var progressPeriodDelete = this.context?.ProgressPeriods.FirstOrDefault(x => x.TenantId == progressPeriod.tableProgressPeriods.TenantId && x.SchoolId == progressPeriod.tableProgressPeriods.SchoolId && x.MarkingPeriodId == progressPeriod.tableProgressPeriods.MarkingPeriodId);

                this.context?.ProgressPeriods.Remove(progressPeriodDelete);
                this.context?.SaveChanges();
                progressPeriod._failure = false;
                progressPeriod._message = "Deleted";
            }

            catch (Exception es)
            {
                progressPeriod._failure = true;
                progressPeriod._message = es.Message;
            }
            return progressPeriod;
        }

        public SchoolYearsAddViewModel AddSchoolYear(SchoolYearsAddViewModel schoolYears)
        {
            try
            {
                int? MarkingPeriodId = Utility.GetMaxPK(this.context, new Func<SchoolYears, int>(x => x.MarkingPeriodId));
                schoolYears.tableSchoolYears.MarkingPeriodId = (int)MarkingPeriodId;
                schoolYears.tableSchoolYears.AcademicYear = schoolYears.tableSchoolYears.StartDate.HasValue == true ? Convert.ToDecimal(schoolYears.tableSchoolYears.StartDate.Value.Year) : (decimal?)null;
                schoolYears.tableSchoolYears.LastUpdated = DateTime.UtcNow;
                schoolYears.tableSchoolYears.TenantId = schoolYears.tableSchoolYears.TenantId;
                this.context?.SchoolYears.Add(schoolYears.tableSchoolYears);
                this.context?.SaveChanges();
                schoolYears._failure = false;
            }
            catch (Exception es)
            {
                schoolYears._failure = true;
                schoolYears._message = es.Message;
            }
            return schoolYears;

        }
        /// <summary>
        /// Get School Year By Id
        /// </summary>
        /// <param name="schoolYears"></param>
        /// <returns></returns>
        public SchoolYearsAddViewModel ViewSchoolYear(SchoolYearsAddViewModel schoolYears)
        {
            SchoolYearsAddViewModel schoolYearsAddViewModel = new SchoolYearsAddViewModel();
            try
            {
                var schoolYearsMaster = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == schoolYears.tableSchoolYears.TenantId && x.SchoolId == schoolYears.tableSchoolYears.SchoolId && x.MarkingPeriodId == schoolYears.tableSchoolYears.MarkingPeriodId);
                if (schoolYearsMaster != null)
                {
                    schoolYearsAddViewModel.tableSchoolYears = schoolYearsMaster;
                    schoolYearsAddViewModel._tenantName = schoolYears._tenantName;
                    schoolYearsAddViewModel._failure = false;                    
                }
                else
                {
                    schoolYearsAddViewModel._failure = true;
                    schoolYearsAddViewModel._message = NORECORDFOUND;                    
                }
            }
            catch (Exception es)
            {

                schoolYearsAddViewModel._failure = true;
                schoolYearsAddViewModel._message = es.Message;
            }
            return schoolYearsAddViewModel;
        }
        /// <summary>
        /// Update School Year
        /// </summary>
        /// <param name="schoolYears"></param>
        /// <returns></returns>
        public SchoolYearsAddViewModel UpdateSchoolYear(SchoolYearsAddViewModel schoolYears)
        {
            try
            {
                var schoolYearsMaster = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == schoolYears.tableSchoolYears.TenantId && x.SchoolId == schoolYears.tableSchoolYears.SchoolId && x.MarkingPeriodId == schoolYears.tableSchoolYears.MarkingPeriodId);
                schoolYearsMaster.SchoolId = schoolYears.tableSchoolYears.SchoolId;
                schoolYearsMaster.TenantId = schoolYears.tableSchoolYears.TenantId;
                schoolYearsMaster.AcademicYear = schoolYears.tableSchoolYears.StartDate.HasValue == true ? Convert.ToDecimal(schoolYears.tableSchoolYears.StartDate.Value.Year) : (decimal?)null;
                schoolYearsMaster.Title = schoolYears.tableSchoolYears.Title;
                schoolYearsMaster.ShortName = schoolYears.tableSchoolYears.ShortName;
                schoolYearsMaster.SortOrder = schoolYears.tableSchoolYears.SortOrder;
                schoolYearsMaster.StartDate = schoolYears.tableSchoolYears.StartDate;
                schoolYearsMaster.EndDate = schoolYears.tableSchoolYears.EndDate;
                schoolYearsMaster.PostStartDate = schoolYears.tableSchoolYears.PostStartDate;
                schoolYearsMaster.PostEndDate = schoolYears.tableSchoolYears.PostEndDate;
                schoolYearsMaster.DoesGrades = schoolYears.tableSchoolYears.DoesGrades;
                schoolYearsMaster.DoesExam = schoolYears.tableSchoolYears.DoesExam;
                schoolYearsMaster.DoesComments = schoolYears.tableSchoolYears.DoesComments;
                schoolYearsMaster.RolloverId = schoolYears.tableSchoolYears.RolloverId;
                schoolYears.tableSchoolYears.LastUpdated = DateTime.UtcNow;
                schoolYearsMaster.UpdatedBy = schoolYears.tableSchoolYears.UpdatedBy;
                this.context?.SaveChanges();
                schoolYears._failure = false;
                
            }
            catch (Exception ex)
            {
                schoolYears.tableSchoolYears = null;
                schoolYears._failure = true;
                schoolYears._message = ex.Message;                
            }
            return schoolYears;
        }
        /// <summary>
        /// Delete School Year
        /// </summary>
        /// <param name="schoolYears"></param>
        /// <returns></returns>
        public SchoolYearsAddViewModel DeleteSchoolYear(SchoolYearsAddViewModel schoolYears)
        {
            try
            {
                var deleteSchoolYear = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == schoolYears.tableSchoolYears.TenantId && x.SchoolId == schoolYears.tableSchoolYears.SchoolId && x.MarkingPeriodId == schoolYears.tableSchoolYears.MarkingPeriodId);
                var semester = this.context?.Semesters.FirstOrDefault(z => z.TenantId == deleteSchoolYear.TenantId && z.SchoolId == deleteSchoolYear.SchoolId && z.YearId == deleteSchoolYear.MarkingPeriodId);
                if(semester!=null)
                {
                    schoolYears.tableSchoolYears = null;
                    schoolYears._message = "School Year cannot be deleted because it has its association";
                    schoolYears._failure = true;
                }
                else
                {
                    this.context?.SchoolYears.Remove(deleteSchoolYear);
                    this.context?.SaveChanges();
                    schoolYears._failure = false;
                    schoolYears._message = "Deleted";
                }                
            }
            catch (Exception es)
            {
                schoolYears._failure = true;
                schoolYears._message = es.Message;
            }
            return schoolYears;
        }
        public QuarterAddViewModel AddQuarter(QuarterAddViewModel quarters)
        {
            try
            {
                var semester = this.context?.Semesters.FirstOrDefault(x => x.TenantId == quarters.tableQuarter.TenantId && x.SchoolId == quarters.tableQuarter.SchoolId && x.MarkingPeriodId == quarters.tableQuarter.SemesterId);
                if (semester.StartDate <= quarters.tableQuarter.StartDate && semester.EndDate >= quarters.tableQuarter.EndDate)
                {
                    int? MarkingPeriodId = Utility.GetMaxPK(this.context, new Func<Quarters, int>(x => x.MarkingPeriodId));
                    quarters.tableQuarter.MarkingPeriodId = (int)MarkingPeriodId;
                    quarters.tableQuarter.AcademicYear = semester.AcademicYear;
                    quarters.tableQuarter.LastUpdated = DateTime.UtcNow;
                    quarters.tableQuarter.TenantId = quarters.tableQuarter.TenantId;
                    this.context?.Quarters.Add(quarters.tableQuarter);
                    this.context?.SaveChanges();
                    quarters._failure = false;
                    quarters.tableQuarter.Semesters = null;
                }
                else
                {
                    quarters._failure = true;
                    quarters._message = "Start Date And End Date of Quarter Should Be Between Start Date And End Date of Semester";
                }
            }
            catch (Exception es)
            {

                quarters._failure = true;
                quarters._message = es.Message;
            }

            return quarters;
        }
        /// <summary>
        /// View Quarter By Id
        /// </summary>
        /// <param name="quarter"></param>
        /// <returns></returns>
        public QuarterAddViewModel ViewQuarter(QuarterAddViewModel quarter)
        {
            QuarterAddViewModel quarterAddViewModel = new QuarterAddViewModel();
            try
            {
                var quarteMaster = this.context?.Quarters.FirstOrDefault(x => x.TenantId == quarter.tableQuarter.TenantId && x.SchoolId == quarter.tableQuarter.SchoolId && x.MarkingPeriodId == quarter.tableQuarter.MarkingPeriodId);
                if (quarteMaster != null)
                {
                    quarterAddViewModel.tableQuarter = quarteMaster;
                    quarterAddViewModel._tenantName = quarter._tenantName;
                    quarterAddViewModel._failure = false;                    
                }
                else
                {
                    quarterAddViewModel._failure = true;
                    quarterAddViewModel._message = NORECORDFOUND;                   
                }
            }
            catch (Exception es)
            {

                quarterAddViewModel._failure = true;
                quarterAddViewModel._message = es.Message;
            }
            return quarterAddViewModel;
        }
        /// <summary>
        /// Update Quarter
        /// </summary>
        /// <param name="quarters"></param>
        /// <returns></returns>
        public QuarterAddViewModel UpdateQuarter(QuarterAddViewModel quarters)
        {
            try
            {
                var quarteMaster = this.context?.Quarters.FirstOrDefault(x => x.TenantId == quarters.tableQuarter.TenantId && x.SchoolId == quarters.tableQuarter.SchoolId && x.MarkingPeriodId == quarters.tableQuarter.MarkingPeriodId);
                var semester = this.context?.Semesters.FirstOrDefault(x => x.TenantId == quarters.tableQuarter.TenantId && x.SchoolId == quarters.tableQuarter.SchoolId && x.MarkingPeriodId == quarters.tableQuarter.SemesterId);
                if(semester.StartDate <= quarters.tableQuarter.StartDate && semester.EndDate >= quarters.tableQuarter.EndDate)
                {
                    quarteMaster.SchoolId = quarters.tableQuarter.SchoolId;
                    quarteMaster.TenantId = quarters.tableQuarter.TenantId;
                    quarteMaster.AcademicYear = semester.AcademicYear;
                    quarteMaster.SemesterId = quarters.tableQuarter.SemesterId;
                    quarteMaster.Title = quarters.tableQuarter.Title;
                    quarteMaster.ShortName = quarters.tableQuarter.ShortName;
                    quarteMaster.SortOrder = quarters.tableQuarter.SortOrder;
                    quarteMaster.SortOrder = quarters.tableQuarter.SortOrder;
                    quarteMaster.StartDate = quarters.tableQuarter.StartDate;
                    quarteMaster.EndDate = quarters.tableQuarter.EndDate;
                    quarteMaster.PostStartDate = quarters.tableQuarter.PostStartDate;
                    quarteMaster.PostEndDate = quarters.tableQuarter.PostEndDate;
                    quarteMaster.DoesGrades = quarters.tableQuarter.DoesGrades;
                    quarteMaster.DoesExam = quarters.tableQuarter.DoesExam;
                    quarteMaster.DoesComments = quarters.tableQuarter.DoesComments;
                    quarteMaster.RolloverId = quarters.tableQuarter.RolloverId;
                    quarters.tableQuarter.LastUpdated = DateTime.UtcNow;
                    quarteMaster.UpdatedBy = quarters.tableQuarter.UpdatedBy;
                    this.context?.SaveChanges();
                    quarters._failure = false;
                }
                else
                {
                    quarters._failure = true;
                    quarters._message = "Start Date And End Date of Quarter Should Be Between Start Date And End Date of Semester";
                }                
            }
            catch (Exception ex)
            {
                quarters.tableQuarter = null;
                quarters._failure = true;
                quarters._message = ex.Message;                
            }
            return quarters;
        }
        /// <summary>
        /// Delete Quarter
        /// </summary>
        /// <param name="quarter"></param>
        /// <returns></returns>
        public QuarterAddViewModel DeleteQuarter(QuarterAddViewModel quarter)
        {
            try
            {
                var quarterDelete = this.context?.Quarters.FirstOrDefault(x => x.TenantId == quarter.tableQuarter.TenantId && x.SchoolId == quarter.tableQuarter.SchoolId && x.MarkingPeriodId == quarter.tableQuarter.MarkingPeriodId);
                var progressPeriod = this.context?.ProgressPeriods.FirstOrDefault(z => z.TenantId == quarterDelete.TenantId && z.SchoolId == quarterDelete.SchoolId && z.QuarterId == quarterDelete.MarkingPeriodId);
                if(progressPeriod != null)
                {
                    quarter.tableQuarter = null;
                    quarter._message = "Quarter cannot be deleted because it has its association";
                    quarter._failure = true;
                }
                else
                {
                    this.context?.Quarters.Remove(quarterDelete);
                    this.context?.SaveChanges();
                    quarter._failure = false;
                    quarter._message = "Deleted";
                }
            }
            catch (Exception es)
            {
                quarter._failure = true;
                quarter._message = es.Message;
            }
            return quarter;
        }
        public DropDownViewModel GetAcademicYearList(DropDownViewModel downViewModel)
        {
            DropDownViewModel dropDownViewModel = new DropDownViewModel();
            var data = this.context?.SchoolYears.Where(x => x.TenantId == downViewModel.TenantId && x.SchoolId == downViewModel.SchoolId).Select(m => new AcademicYear()
            {
                Year = m.StartDate.HasValue == true && m.StartDate.Value.Year != m.EndDate.Value.Year && m.EndDate.HasValue == true ? m.StartDate.Value.Year.ToString() + "-" + m.EndDate.Value.Year.ToString() :
                      m.StartDate.HasValue == true && m.StartDate.Value.Year == m.EndDate.Value.Year && m.EndDate.HasValue == true ? m.EndDate.Value.Year.ToString() : m.StartDate.HasValue == true && m.EndDate.HasValue == false ? m.StartDate.Value.Year.ToString()
                      : m.StartDate.HasValue == false && m.EndDate.HasValue == true ? m.EndDate.Value.Year.ToString() : null,AcademyYear=(int)m.AcademicYear
            }).ToList();
            dropDownViewModel.AcademicYears = data;
            dropDownViewModel.SchoolId = downViewModel.SchoolId;
            dropDownViewModel.TenantId = downViewModel.TenantId;
            dropDownViewModel._tenantName = downViewModel._tenantName;
            dropDownViewModel._failure = false;
            return dropDownViewModel;
        }
        public PeriodViewModel GetMarkingPeriodTitleList(PeriodViewModel periodViewModel)
        {
            if (periodViewModel.AcademicYear > 0)
            {
                periodViewModel.period = new List<PeriodView>();
                var SemesterList = this.context?.Semesters.Include(x=>x.Quarters).ThenInclude(x=>x.ProgressPeriods).Where(x => x.AcademicYear == periodViewModel.AcademicYear && x.SchoolId==periodViewModel.SchoolId && x.TenantId==periodViewModel.TenantId).ToList();
                if (SemesterList.Count > 0)
                {
                    foreach (var Semester in SemesterList)
                    {
                        var QuarterList = Semester.Quarters.Where(x => x.SemesterId == Semester.MarkingPeriodId && x.AcademicYear== periodViewModel.AcademicYear).ToList();
                        if (QuarterList.Count > 0)
                        {
                            foreach (var Quarter in QuarterList)
                            {
                                var ProgressPeriodList = Quarter.ProgressPeriods.Where(x => x.QuarterId == Quarter.MarkingPeriodId && x.AcademicYear == periodViewModel.AcademicYear).ToList();
                                if (ProgressPeriodList.Count > 0)
                                {
                                    foreach (var ProgressPeriod in ProgressPeriodList)
                                    {
                                        var ProgressList = new PeriodView() { PeriodTitle = ProgressPeriod.Title, EndDate = ProgressPeriod.EndDate, StartDate = ProgressPeriod.StartDate };
                                        periodViewModel.period.Add(ProgressList);
                                    }

                                }
                                else
                                {
                                    if (periodViewModel.period.Count == 0)
                                    {
                                        var QuaterTitleList = SemesterList.SelectMany(x => x.Quarters).ToList().Select(x => new PeriodView() { PeriodTitle = x.Title, EndDate = x.EndDate, StartDate = x.StartDate }).ToList();
                                        periodViewModel.period.AddRange(QuaterTitleList);
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (periodViewModel.period.Count == 0)
                            {
                                var SemesterTitleList = SemesterList.Select(x => new PeriodView() { PeriodTitle = x.Title, EndDate = x.EndDate, StartDate = x.StartDate }).ToList();
                                periodViewModel.period.AddRange(SemesterTitleList);
                            }

                        }
                    }
                }
                else
                {
                    var YearTitle = this.context?.SchoolYears.Where(x => x.TenantId == periodViewModel.TenantId && x.SchoolId == periodViewModel.SchoolId && x.AcademicYear == periodViewModel.AcademicYear).Select(x => new PeriodView() { PeriodTitle = x.Title, EndDate = x.EndDate, StartDate = x.StartDate }).FirstOrDefault();
                    periodViewModel.period.Add(YearTitle);
                }
                periodViewModel._failure = false;
            }
            else
            {
                periodViewModel._message = "Provide valid year id";
                periodViewModel._failure = true;
            }
            
            return periodViewModel;
        }
    }
}
