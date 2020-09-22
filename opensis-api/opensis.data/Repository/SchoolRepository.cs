using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
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

        public List<Schools> GetAllSchools()
        {
            return this.context?.tblSchool.Where(s=>s.isactive==true).ToList().AsEnumerable().ToList<Schools>();
        }

        public List<Schools> AddSchools(Schools school)
        {
            this.context?.tblSchool.Add(school);
            this.context?.SaveChanges();

            return GetAllSchools();
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

                var courseInfo = from schoolMaster in this.context?.TableSchoolMaster.ToList()
                                 join schoolDetails in this.context?.TableSchoolDetail.ToList()
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

                var schoolList = this.context?.TableSchoolMaster.Where(x => x.TenantId == school.TenantId).OrderBy(x => x.SchoolName).Select(x=> new GetSchoolForView() { School_Id=x.SchoolId,Tenant_Id=x.TenantId,School_Name=x.SchoolName.Trim(),Phone=null,Principle=null,School_Address=null,Status=null}).ToList();
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
                var schoolMaster =  this.context?.TableSchoolMaster.Include(x=>x.TableSchoolDetail).FirstOrDefault(x => x.TenantId == school.tblSchoolMaster.TenantId && x.SchoolId == school.tblSchoolMaster.SchoolId);
                if (schoolMaster != null)
                {
                    if (schoolMaster.GeoPosition != null)
                    {
                        school.longitude = schoolMaster.GeoPosition.Centroid.X;
                        school.latitude = schoolMaster.GeoPosition.Centroid.Y;
                    }
                    school.tblSchoolMaster = schoolMaster;
                    school.tblSchoolMaster.GeoPosition = null;
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
                var schoolMaster = this.context?.TableSchoolMaster.Include(x => x.TableSchoolDetail).FirstOrDefault(x => x.TenantId == school.tblSchoolMaster.TenantId && x.SchoolId == school.tblSchoolMaster.SchoolId);

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
                if (schoolMaster.TableSchoolDetail.ToList().Count == 0 && school.tblSchoolMaster.TableSchoolDetail.ToList().Count > 0)
                {
                    school.tblSchoolMaster.TableSchoolDetail.ToList().ForEach(p => p.Id = (int)Utility.GetMaxPK(this.context, new Func<TableSchoolDetail, int>(x => x.Id)));
                    school.tblSchoolMaster.TableSchoolDetail.ToList().ForEach(p => p.SchoolId = school.tblSchoolMaster.SchoolId);
                    school.tblSchoolMaster.TableSchoolDetail.ToList().ForEach(p => p.TenantId = school.tblSchoolMaster.TenantId);
                    this.context?.TableSchoolDetail.AddRange(school.tblSchoolMaster.TableSchoolDetail);
                }
                if (schoolMaster.TableSchoolDetail.ToList().Count > 0)
                {
                    foreach (var detailes in schoolMaster.TableSchoolDetail.ToList())
                    {
                        detailes.Affiliation = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Affiliation;
                        detailes.Associations = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Associations;
                        detailes.Locale = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Locale;
                        detailes.LowestGradeLevel = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().LowestGradeLevel;
                        detailes.HighestGradeLevel = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().HighestGradeLevel;
                        detailes.DateSchoolOpened = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().DateSchoolOpened;
                        detailes.DateSchoolClosed = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().DateSchoolClosed;
                        detailes.Status = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Status;
                        detailes.Gender = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Gender;
                        detailes.Internet = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Internet;
                        detailes.Electricity = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Electricity;
                        detailes.Telephone = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Telephone;
                        detailes.Fax = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Fax;
                        detailes.Website = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Website;
                        detailes.Email = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Email;
                        detailes.Facebook = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Facebook;
                        detailes.Twitter = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Twitter;
                        detailes.Instagram = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Instagram;
                        detailes.Youtube = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().Youtube;
                        detailes.LinkedIn = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().LinkedIn;
                        detailes.NameOfPrincipal = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().NameOfPrincipal;
                        detailes.NameOfAssistantPrincipal = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().NameOfAssistantPrincipal;
                        detailes.SchoolLogo = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().SchoolLogo;
                        detailes.RunningWater = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().RunningWater;
                        detailes.MainSourceOfDrinkingWater = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().MainSourceOfDrinkingWater;
                        detailes.CurrentlyAvailable = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().CurrentlyAvailable;
                        detailes.FemaleToiletType = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().FemaleToiletType;
                        detailes.TotalFemaleToilets = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().TotalFemaleToilets;
                        detailes.TotalFemaleToiletsUsable = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().TotalFemaleToiletsUsable;
                        detailes.FemaleToiletAccessibility = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().FemaleToiletAccessibility;
                        detailes.MaleToiletType = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().MaleToiletType;
                        detailes.TotalMaleToilets = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().TotalMaleToilets;
                        detailes.TotalMaleToiletsUsable = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().TotalMaleToiletsUsable;
                        detailes.MaleToiletAccessibility = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().MaleToiletAccessibility;
                        detailes.ComonToiletType = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().ComonToiletType;
                        detailes.TotalCommonToilets = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().TotalCommonToilets;
                        detailes.TotalCommonToiletsUsable = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().TotalCommonToiletsUsable;
                        detailes.CommonToiletAccessibility = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().CommonToiletAccessibility;
                        detailes.HandwashingAvailable = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().HandwashingAvailable;
                        detailes.SoapAndWaterAvailable = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().SoapAndWaterAvailable;
                        detailes.HygeneEducation = school.tblSchoolMaster.TableSchoolDetail.FirstOrDefault().HygeneEducation;
                    }

                }
                this.context?.SaveChanges();
                school.tblSchoolMaster.GeoPosition = null;
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
                int? MasterSchoolId = Utility.GetMaxPK(this.context, new Func<TableSchoolMaster, int>(x => x.SchoolId));
                school.tblSchoolMaster.GeoPosition = null;
                Point currentLocation = null;
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                if (school.longitude != null && school.latitude != null)
                {
                    currentLocation = geometryFactory.CreatePoint(new Coordinate((double)school.longitude, (double)school.latitude));
                }
                school.tblSchoolMaster.SchoolId = (int)MasterSchoolId;
                if (school.tblSchoolMaster.TableSchoolDetail.ToList().Count>0)
                {
                    school.tblSchoolMaster.TableSchoolDetail.ToList().ForEach(p => p.Id=(int)Utility.GetMaxPK(this.context, new Func<TableSchoolDetail, int>(x=>x.Id)));
                }
                school.tblSchoolMaster.DateCreated = DateTime.UtcNow;
                school.tblSchoolMaster.TenantId = school.tblSchoolMaster.TenantId;
                school.tblSchoolMaster.GeoPosition = currentLocation;
                this.context?.TableSchoolMaster.Add(school.tblSchoolMaster);
                this.context?.SaveChanges();
                school._failure = false;
                school.tblSchoolMaster.GeoPosition = null;
                return school;

            }
            catch (Exception es)
            {

                throw;
            }

        }
        
        
    }
}
