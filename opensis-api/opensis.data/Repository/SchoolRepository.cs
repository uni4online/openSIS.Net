using Microsoft.EntityFrameworkCore;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.School;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Get Schools for pagination
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public SchoolListModel GetAllSchools(PageResult pageResult)
        {
            SchoolListModel schoolListModel = new SchoolListModel();
            try
            {

                var courseInfo = from schoolMaster in this.context?.SchoolMaster.ToList()
                                 join schoolDetails in this.context?.SchoolDetail.ToList()
                                 on schoolMaster.SchoolId equals schoolDetails.SchoolId into schoolInfo
                                 from schools in schoolInfo.DefaultIfEmpty()
                                 select new GetSchoolForView()
                                 {
                                     School_Name = schoolMaster.SchoolName.Trim(),
                                     School_Id = schoolMaster.SchoolId,
                                     Tenant_Id = schoolMaster.TenantId,
                                     Phone = schools == null ? string.Empty : schools.Telephone.Trim(),
                                     Principle = schools == null ? string.Empty : schools.NameOfPrincipal.Trim(),
                                     ////School_Address = schoolMaster.StreetAddress1 == null ? string.Empty : schoolMaster.StreetAddress1.Trim() + " " + schoolMaster.StreetAddress2.Trim() + " " + schoolMaster.State.Trim() + " " + schoolMaster.Country.Trim() + " " + schoolMaster.Zip.Trim(),
                                     School_Address = ToFullAddress(schoolMaster.StreetAddress1, schoolMaster.StreetAddress2, schoolMaster.City, schoolMaster.State, schoolMaster.Country, schoolMaster.Zip),
                                     Status = schools?.Status
                                 };
                schoolListModel.TotalCount = courseInfo.Count();

                //courseInfo=courseInfo.Where(s => s.School_Name.Contains(pageResult.FilterText) || s.Principle.Contains(pageResult.FilterText) || s.School_Address.Contains(pageResult.FilterText) || s.Phone.Contains(pageResult.FilterText)).Where(x=>x.School_Name.Equals(pageResult.SchoolNameFilter));
                courseInfo = courseInfo.OrderBy(x=>x.School_Name).Skip((pageResult.PageNumber - 1) * pageResult.PageSize).Take(pageResult.PageSize).ToList();
                schoolListModel.GetSchoolForView = courseInfo.ToList();
                schoolListModel.PageNumber = pageResult.PageNumber;
                schoolListModel._pageSize = pageResult.PageSize;
                schoolListModel._tenantName = pageResult._tenantName;
                schoolListModel._token = pageResult._token;
                schoolListModel._failure = false;
            }
            catch (Exception es)
            {
                schoolListModel._message = es.Message;
                schoolListModel._failure=true;
                schoolListModel._tenantName = pageResult._tenantName;
                schoolListModel._token = pageResult._token;
            }
            return schoolListModel;

        }
        /// <summary>
        /// Get All school list for dropdown
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public SchoolListModel GetAllSchoolList(SchoolListModel school)
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
                var schoolMaster =  this.context?.SchoolMaster.Include(x=>x.SchoolDetail).FirstOrDefault(x => x.TenantId == school.tblSchoolMaster.TenantId && x.SchoolId == school.tblSchoolMaster.SchoolId);
                if (schoolMaster != null)
                {
                    school.tblSchoolMaster = schoolMaster;
                    if (school.tblSchoolMaster.SchoolDetail.ToList().Count > 0)
                    {
                        school.tblSchoolMaster.SchoolDetail.FirstOrDefault().SchoolMaster = null;
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
                var schoolMaster = this.context?.SchoolMaster.Include(x => x.SchoolDetail).FirstOrDefault(x => x.TenantId == school.tblSchoolMaster.TenantId && x.SchoolId == school.tblSchoolMaster.SchoolId);

                schoolMaster.SchoolAltId = school.tblSchoolMaster.SchoolAltId;
                schoolMaster.SchoolStateId = school.tblSchoolMaster.SchoolStateId;
                schoolMaster.SchoolDistrictId = school.tblSchoolMaster.SchoolDistrictId;
                schoolMaster.SchoolLevel = school.tblSchoolMaster.SchoolLevel;
                schoolMaster.SchoolClassification = school.tblSchoolMaster.SchoolClassification;
                schoolMaster.SchoolName = school.tblSchoolMaster.SchoolName;
                schoolMaster.AlternateName = school.tblSchoolMaster.AlternateName;
                schoolMaster.StreetAddress1 = school.tblSchoolMaster.StreetAddress1;
                schoolMaster.StreetAddress2 = school.tblSchoolMaster.StreetAddress2;
                schoolMaster.City = school.tblSchoolMaster.City;
                schoolMaster.County = school.tblSchoolMaster.County;
                schoolMaster.Division = school.tblSchoolMaster.Division;
                schoolMaster.State = school.tblSchoolMaster.State;
                schoolMaster.District = school.tblSchoolMaster.District;
                schoolMaster.Zip = school.tblSchoolMaster.Zip;
                schoolMaster.Country = school.tblSchoolMaster.Country;
                schoolMaster.Features = school.tblSchoolMaster.Features;
                schoolMaster.ModifiedBy = school.tblSchoolMaster.ModifiedBy;
                schoolMaster.DateModifed = DateTime.UtcNow;
                schoolMaster.MaxApiChecks = school.tblSchoolMaster.MaxApiChecks;
                schoolMaster.SchoolInternalId = school.tblSchoolMaster.SchoolInternalId;
                schoolMaster.Latitude = school.tblSchoolMaster.Latitude;
                schoolMaster.Longitude = school.tblSchoolMaster.Longitude;
                if (schoolMaster.SchoolDetail.ToList().Count == 0 && school.tblSchoolMaster.SchoolDetail.ToList().Count > 0)
                {
                    school.tblSchoolMaster.SchoolDetail.ToList().ForEach(p => p.Id = (int)Utility.GetMaxPK(this.context, new Func<SchoolDetail, int>(x => x.Id)));
                    school.tblSchoolMaster.SchoolDetail.ToList().ForEach(p => p.SchoolId = school.tblSchoolMaster.SchoolId);
                    school.tblSchoolMaster.SchoolDetail.ToList().ForEach(p => p.TenantId = school.tblSchoolMaster.TenantId);
                    this.context?.SchoolDetail.AddRange(school.tblSchoolMaster.SchoolDetail);
                }
                if (schoolMaster.SchoolDetail.ToList().Count > 0)
                {
                    foreach (var detailes in schoolMaster.SchoolDetail.ToList())
                    {
                        detailes.Affiliation = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Affiliation;
                        detailes.Associations = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Associations;
                        detailes.Locale = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Locale;
                        detailes.LowestGradeLevel = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().LowestGradeLevel;
                        detailes.HighestGradeLevel = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().HighestGradeLevel;
                        detailes.DateSchoolOpened = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().DateSchoolOpened;
                        detailes.DateSchoolClosed = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().DateSchoolClosed;
                        detailes.Status = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Status;
                        detailes.Gender = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Gender;
                        detailes.Internet = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Internet;
                        detailes.Electricity = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Electricity;
                        detailes.Telephone = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Telephone;
                        detailes.Fax = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Fax;
                        detailes.Website = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Website;
                        detailes.Email = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Email;
                        detailes.Facebook = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Facebook;
                        detailes.Twitter = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Twitter;
                        detailes.Instagram = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Instagram;
                        detailes.Youtube = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().Youtube;
                        detailes.LinkedIn = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().LinkedIn;
                        detailes.NameOfPrincipal = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().NameOfPrincipal;
                        detailes.NameOfAssistantPrincipal = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().NameOfAssistantPrincipal;
                        detailes.SchoolLogo = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().SchoolLogo;
                        detailes.RunningWater = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().RunningWater;
                        detailes.MainSourceOfDrinkingWater = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().MainSourceOfDrinkingWater;
                        detailes.CurrentlyAvailable = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().CurrentlyAvailable;
                        detailes.FemaleToiletType = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().FemaleToiletType;
                        detailes.TotalFemaleToilets = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().TotalFemaleToilets;
                        detailes.TotalFemaleToiletsUsable = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().TotalFemaleToiletsUsable;
                        detailes.FemaleToiletAccessibility = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().FemaleToiletAccessibility;
                        detailes.MaleToiletType = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().MaleToiletType;
                        detailes.TotalMaleToilets = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().TotalMaleToilets;
                        detailes.TotalMaleToiletsUsable = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().TotalMaleToiletsUsable;
                        detailes.MaleToiletAccessibility = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().MaleToiletAccessibility;
                        detailes.ComonToiletType = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().ComonToiletType;
                        detailes.TotalCommonToilets = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().TotalCommonToilets;
                        detailes.TotalCommonToiletsUsable = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().TotalCommonToiletsUsable;
                        detailes.CommonToiletAccessibility = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().CommonToiletAccessibility;
                        detailes.HandwashingAvailable = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().HandwashingAvailable;
                        detailes.SoapAndWaterAvailable = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().SoapAndWaterAvailable;
                        detailes.HygeneEducation = school.tblSchoolMaster.SchoolDetail.FirstOrDefault().HygeneEducation;
                    }

                }
                this.context?.SaveChanges();
                if (school.tblSchoolMaster.SchoolDetail.ToList().Count > 0)
                {
                    school.tblSchoolMaster.SchoolDetail.FirstOrDefault().SchoolMaster = null;
                }
                school._failure = false;
                return school;
            }
            catch (Exception ex)
            {
                school.tblSchoolMaster = null;
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
                school.tblSchoolMaster.SchoolId = (int)MasterSchoolId;
                if (school.tblSchoolMaster.SchoolDetail.ToList().Count>0)
                {
                    school.tblSchoolMaster.SchoolDetail.ToList().ForEach(p => p.Id=(int)Utility.GetMaxPK(this.context, new Func<SchoolDetail, int>(x=>x.Id)));
                }
                school.tblSchoolMaster.DateCreated = DateTime.UtcNow;
                school.tblSchoolMaster.TenantId = school.tblSchoolMaster.TenantId;
                this.context?.SchoolMaster.Add(school.tblSchoolMaster);
                this.context?.SaveChanges();
                school._failure = false;
                if (school.tblSchoolMaster.SchoolDetail.ToList().Count>0)
                {
                    school.tblSchoolMaster.SchoolDetail.FirstOrDefault().SchoolMaster = null;
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
