using Microsoft.EntityFrameworkCore;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.School;
using System;
using System.Linq;

namespace opensis.data.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public SchoolRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

       

        private static string ToFullAddress(string Address1, string Address2,string City,string State,string Country,string Zip)
        {
            string address = "";
            if (!string.IsNullOrWhiteSpace(Address1))
            {


              return  address == null
                    ? null
                    : $"{Address1?.Trim()}{(!string.IsNullOrWhiteSpace(Address2) ? $", {Address2?.Trim()}" : string.Empty)}, {City?.Trim()}, {State?.Trim()} {Zip?.Trim()}";
            }
            return address;
        }
        /// <summary>
        /// Get SchoolsList with pagination
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public SchoolListModel GetAllSchoolList(PageResult pageResult)
        {
            SchoolListModel schoolListModel = new SchoolListModel();
            try
            {
                //string sortField = "SchoolName"; string sortOrder = "desc";

                IQueryable<SchoolMaster> transactionIQ = this.context?.SchoolMaster
                    .Include(d => d.SchoolDetail)
                    .Where(x => x.TenantId == pageResult.TenantId);
                if (pageResult.SoertingModel != null)
                {
                    switch (pageResult.SoertingModel.SortColumn)
                    {
                        case "NameOfPrincipal":

                            if (pageResult.SoertingModel.SortDirection.ToLower() == "asc")
                            {

                                transactionIQ = transactionIQ.OrderBy(a => a.SchoolDetail.FirstOrDefault().NameOfPrincipal);
                            }
                            else
                            {
                                transactionIQ = transactionIQ.OrderByDescending(a => a.SchoolDetail.FirstOrDefault().NameOfPrincipal);
                            }
                            break;

                        default:
                            transactionIQ = Utility.Sort(transactionIQ, pageResult.SoertingModel.SortColumn, pageResult.SoertingModel.SortDirection.ToLower());
                            break;
                    }

                }

                int totalCount = transactionIQ.Count();
                transactionIQ = transactionIQ.Skip((pageResult.PageNumber - 1) * pageResult.PageSize).Take(pageResult.PageSize);
                var schoollist = transactionIQ.AsNoTracking().Select(s => new GetSchoolForView
                {
                    School_Id = s.SchoolId,
                    School_Name = s.SchoolName,
                    Tenant_Id = s.TenantId,
                    Phone = s.SchoolDetail.FirstOrDefault().Telephone == null ? string.Empty : s.SchoolDetail.FirstOrDefault().Telephone.Trim(),
                    Principle = s.SchoolDetail.FirstOrDefault().NameOfPrincipal == null ? string.Empty : s.SchoolDetail.FirstOrDefault().NameOfPrincipal.Trim(),
                    School_Address = ToFullAddress(s.StreetAddress1, s.StreetAddress2,
                    //(!string.IsNullOrEmpty(s.City)?this.context.TableCity.Where(x => x.Id == int.Parse(s.City)).FirstOrDefault().Name: string.Empty),
                    this.context.City.Where(x => x.Id == Convert.ToInt32(s.City)).FirstOrDefault().Name,
                    this.context.State.Where(x => x.Id == Convert.ToInt32(s.State)).FirstOrDefault().Name,
                    this.context.Country.Where(x => x.Id == Convert.ToInt32(s.Country)).FirstOrDefault().Name, s.Zip),
                    Status = s.SchoolDetail.FirstOrDefault().Status == null ? false : s.SchoolDetail.FirstOrDefault().Status
                }).ToList();

                schoolListModel.GetSchoolForView = schoollist;
                schoolListModel.PageNumber = pageResult.PageNumber;
                schoolListModel._pageSize = pageResult.PageSize;
                schoolListModel._tenantName = pageResult._tenantName;
                schoolListModel._token = pageResult._token;
                schoolListModel._failure = false;
            }
            catch (Exception es)
            {
                schoolListModel._message = es.Message;
                schoolListModel._failure = true;
                schoolListModel._tenantName = pageResult._tenantName;
                schoolListModel._token = pageResult._token;
            }
            return schoolListModel;

        }
        /// <summary>
        /// Get All school for dropdown
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public SchoolListModel GetAllSchools(SchoolListModel school)
        {
            SchoolListModel schoolListModel = new SchoolListModel();
            try
            {

                var schoolList = this.context?.SchoolMaster.Where(x => x.TenantId == school.TenantId).OrderBy(x => x.SchoolName).Select(x=> new GetSchoolForView() { School_Id=x.SchoolId,Tenant_Id=x.TenantId,School_Name=x.SchoolName.Trim(),Phone=null,Principle=null,School_Address=null,Status=null}).ToList();
                schoolListModel.GetSchoolForView = schoolList;
                schoolListModel.PageNumber = null;
                schoolListModel._pageSize = null;
                schoolListModel._tenantName = school._tenantName;
                schoolListModel._token = school._token;
                schoolListModel._failure = false;
            }
            catch (Exception es)
            {
                schoolListModel._message = es.Message;
                schoolListModel._failure = true;
                schoolListModel._tenantName = school._tenantName;
                schoolListModel._token = school._token;
            }
            return schoolListModel;

        }
        /// <summary>
        /// Get School by id
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public SchoolAddViewModel ViewSchool(SchoolAddViewModel school)
        {
            try
            {
                SchoolAddViewModel SchoolAddViewModel = new SchoolAddViewModel();
                var schoolMaster =  this.context?.SchoolMaster.Include(x=>x.SchoolDetail).FirstOrDefault(x => x.TenantId == school.schoolMaster.TenantId && x.SchoolId == school.schoolMaster.SchoolId);
                if (schoolMaster != null)
                {
                    school.schoolMaster = schoolMaster;
                    if (school.schoolMaster.SchoolDetail.ToList().Count > 0)
                    {
                        school.schoolMaster.SchoolDetail.FirstOrDefault().SchoolMaster = null;
                    } 
                    school._tenantName = school._tenantName;
                    return school;
                }
                else
                {
                    SchoolAddViewModel._failure = true;
                    SchoolAddViewModel._message = NORECORDFOUND;
                    return SchoolAddViewModel;
                }
            }
            catch (Exception es)
            {

                throw;
            }

        }

        public SchoolAddViewModel UpdateSchool(SchoolAddViewModel school)
        {
            try
            {
                var schoolMaster = this.context?.SchoolMaster.Include(x => x.SchoolDetail).FirstOrDefault(x => x.TenantId == school.schoolMaster.TenantId && x.SchoolId == school.schoolMaster.SchoolId);

                schoolMaster.SchoolAltId = school.schoolMaster.SchoolAltId;
                schoolMaster.SchoolStateId = school.schoolMaster.SchoolStateId;
                schoolMaster.SchoolDistrictId = school.schoolMaster.SchoolDistrictId;
                schoolMaster.SchoolLevel = school.schoolMaster.SchoolLevel;
                schoolMaster.SchoolClassification = school.schoolMaster.SchoolClassification;
                schoolMaster.SchoolName = school.schoolMaster.SchoolName;
                schoolMaster.AlternateName = school.schoolMaster.AlternateName;
                schoolMaster.StreetAddress1 = school.schoolMaster.StreetAddress1;
                schoolMaster.StreetAddress2 = school.schoolMaster.StreetAddress2;
                schoolMaster.City = school.schoolMaster.City;
                schoolMaster.County = school.schoolMaster.County;
                schoolMaster.Division = school.schoolMaster.Division;
                schoolMaster.State = school.schoolMaster.State;
                schoolMaster.District = school.schoolMaster.District;
                schoolMaster.Zip = school.schoolMaster.Zip;
                schoolMaster.Country = school.schoolMaster.Country;
                schoolMaster.Features = school.schoolMaster.Features;
                schoolMaster.ModifiedBy = school.schoolMaster.ModifiedBy;
                schoolMaster.DateModifed = DateTime.UtcNow;
                schoolMaster.MaxApiChecks = school.schoolMaster.MaxApiChecks;
                schoolMaster.SchoolInternalId = school.schoolMaster.SchoolInternalId;
                schoolMaster.Latitude = school.schoolMaster.Latitude;
                schoolMaster.Longitude = school.schoolMaster.Longitude;
                if (schoolMaster.SchoolDetail.ToList().Count == 0 && school.schoolMaster.SchoolDetail.ToList().Count > 0)
                {
                    school.schoolMaster.SchoolDetail.ToList().ForEach(p => p.Id = (int)Utility.GetMaxPK(this.context, new Func<SchoolDetail, int>(x => x.Id)));
                    school.schoolMaster.SchoolDetail.ToList().ForEach(p => p.SchoolId = school.schoolMaster.SchoolId);
                    school.schoolMaster.SchoolDetail.ToList().ForEach(p => p.TenantId = school.schoolMaster.TenantId);
                    this.context?.SchoolDetail.AddRange(school.schoolMaster.SchoolDetail);
                }
                if (schoolMaster.SchoolDetail.ToList().Count > 0)
                {
                    foreach (var detailes in schoolMaster.SchoolDetail.ToList())
                    {
                        detailes.Affiliation = school.schoolMaster.SchoolDetail.FirstOrDefault().Affiliation;
                        detailes.Associations = school.schoolMaster.SchoolDetail.FirstOrDefault().Associations;
                        detailes.Locale = school.schoolMaster.SchoolDetail.FirstOrDefault().Locale;
                        detailes.LowestGradeLevel = school.schoolMaster.SchoolDetail.FirstOrDefault().LowestGradeLevel;
                        detailes.HighestGradeLevel = school.schoolMaster.SchoolDetail.FirstOrDefault().HighestGradeLevel;
                        detailes.DateSchoolOpened = school.schoolMaster.SchoolDetail.FirstOrDefault().DateSchoolOpened;
                        detailes.DateSchoolClosed = school.schoolMaster.SchoolDetail.FirstOrDefault().DateSchoolClosed;
                        detailes.Status = school.schoolMaster.SchoolDetail.FirstOrDefault().Status;
                        detailes.Gender = school.schoolMaster.SchoolDetail.FirstOrDefault().Gender;
                        detailes.Internet = school.schoolMaster.SchoolDetail.FirstOrDefault().Internet;
                        detailes.Electricity = school.schoolMaster.SchoolDetail.FirstOrDefault().Electricity;
                        detailes.Telephone = school.schoolMaster.SchoolDetail.FirstOrDefault().Telephone;
                        detailes.Fax = school.schoolMaster.SchoolDetail.FirstOrDefault().Fax;
                        detailes.Website = school.schoolMaster.SchoolDetail.FirstOrDefault().Website;
                        detailes.Email = school.schoolMaster.SchoolDetail.FirstOrDefault().Email;
                        detailes.Facebook = school.schoolMaster.SchoolDetail.FirstOrDefault().Facebook;
                        detailes.Twitter = school.schoolMaster.SchoolDetail.FirstOrDefault().Twitter;
                        detailes.Instagram = school.schoolMaster.SchoolDetail.FirstOrDefault().Instagram;
                        detailes.Youtube = school.schoolMaster.SchoolDetail.FirstOrDefault().Youtube;
                        detailes.LinkedIn = school.schoolMaster.SchoolDetail.FirstOrDefault().LinkedIn;
                        detailes.NameOfPrincipal = school.schoolMaster.SchoolDetail.FirstOrDefault().NameOfPrincipal;
                        detailes.NameOfAssistantPrincipal = school.schoolMaster.SchoolDetail.FirstOrDefault().NameOfAssistantPrincipal;
                        detailes.SchoolLogo = school.schoolMaster.SchoolDetail.FirstOrDefault().SchoolLogo;
                        detailes.RunningWater = school.schoolMaster.SchoolDetail.FirstOrDefault().RunningWater;
                        detailes.MainSourceOfDrinkingWater = school.schoolMaster.SchoolDetail.FirstOrDefault().MainSourceOfDrinkingWater;
                        detailes.CurrentlyAvailable = school.schoolMaster.SchoolDetail.FirstOrDefault().CurrentlyAvailable;
                        detailes.FemaleToiletType = school.schoolMaster.SchoolDetail.FirstOrDefault().FemaleToiletType;
                        detailes.TotalFemaleToilets = school.schoolMaster.SchoolDetail.FirstOrDefault().TotalFemaleToilets;
                        detailes.TotalFemaleToiletsUsable = school.schoolMaster.SchoolDetail.FirstOrDefault().TotalFemaleToiletsUsable;
                        detailes.FemaleToiletAccessibility = school.schoolMaster.SchoolDetail.FirstOrDefault().FemaleToiletAccessibility;
                        detailes.MaleToiletType = school.schoolMaster.SchoolDetail.FirstOrDefault().MaleToiletType;
                        detailes.TotalMaleToilets = school.schoolMaster.SchoolDetail.FirstOrDefault().TotalMaleToilets;
                        detailes.TotalMaleToiletsUsable = school.schoolMaster.SchoolDetail.FirstOrDefault().TotalMaleToiletsUsable;
                        detailes.MaleToiletAccessibility = school.schoolMaster.SchoolDetail.FirstOrDefault().MaleToiletAccessibility;
                        detailes.ComonToiletType = school.schoolMaster.SchoolDetail.FirstOrDefault().ComonToiletType;
                        detailes.TotalCommonToilets = school.schoolMaster.SchoolDetail.FirstOrDefault().TotalCommonToilets;
                        detailes.TotalCommonToiletsUsable = school.schoolMaster.SchoolDetail.FirstOrDefault().TotalCommonToiletsUsable;
                        detailes.CommonToiletAccessibility = school.schoolMaster.SchoolDetail.FirstOrDefault().CommonToiletAccessibility;
                        detailes.HandwashingAvailable = school.schoolMaster.SchoolDetail.FirstOrDefault().HandwashingAvailable;
                        detailes.SoapAndWaterAvailable = school.schoolMaster.SchoolDetail.FirstOrDefault().SoapAndWaterAvailable;
                        detailes.HygeneEducation = school.schoolMaster.SchoolDetail.FirstOrDefault().HygeneEducation;
                    }

                }
                this.context?.SaveChanges();
                if (school.schoolMaster.SchoolDetail.ToList().Count > 0)
                {
                    school.schoolMaster.SchoolDetail.FirstOrDefault().SchoolMaster = null;
                }
                school._failure = false;
                return school;
            }
            catch (Exception ex)
            {
                school.schoolMaster = null;
                school._failure = true;
                school._message = NORECORDFOUND;
                return school;
            }
            
        }
        /// <summary>
        /// School Add
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public SchoolAddViewModel AddSchool(SchoolAddViewModel school)
        {
            try
            {
                int? MasterSchoolId = Utility.GetMaxPK(this.context, new Func<SchoolMaster, int>(x => x.SchoolId));
                school.schoolMaster.SchoolId = (int)MasterSchoolId;
                if (school.schoolMaster.SchoolDetail.ToList().Count>0)
                {
                    school.schoolMaster.SchoolDetail.ToList().ForEach(p => p.Id=(int)Utility.GetMaxPK(this.context, new Func<SchoolDetail, int>(x=>x.Id)));
                }
                school.schoolMaster.DateCreated = DateTime.UtcNow;
                school.schoolMaster.TenantId = school.schoolMaster.TenantId;
                this.context?.SchoolMaster.Add(school.schoolMaster);
                this.context?.SaveChanges();
                school._failure = false;
                if (school.schoolMaster.SchoolDetail.ToList().Count>0)
                {
                    school.schoolMaster.SchoolDetail.FirstOrDefault().SchoolMaster = null;
                }
                return school;

            }
            catch (Exception es)
            {

                throw;
            }

        }
        
        
    }
}
