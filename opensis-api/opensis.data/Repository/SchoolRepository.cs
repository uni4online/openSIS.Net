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
                                     School_Address = schoolMaster.StreetAddress1.Trim() + " " + schoolMaster.StreetAddress2.Trim() + " " + schoolMaster.State.Trim() + " " + schoolMaster.Country.Trim() + " " + schoolMaster.Zip.Trim(),
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

        public SchoolAddViewModel ViewSchool(SchoolAddViewModel school)
        {
            try
            {
                SchoolAddViewModel SchoolAddViewModel = new SchoolAddViewModel();
                var schoolMaster =  this.context?.TableSchoolMaster.FirstOrDefault(x => x.TenantId == school.tblSchoolDetail.TableSchoolMaster.TenantId && x.SchoolId == school.tblSchoolDetail.TableSchoolMaster.SchoolId);
                var schoolDetails = this.context?.TableSchoolDetail.FirstOrDefault(x => x.TenantId == school.tblSchoolDetail.TableSchoolMaster.TenantId && x.SchoolId == school.tblSchoolDetail.TableSchoolMaster.SchoolId);
                if (schoolMaster != null)
                {
                    if (schoolMaster.GeoPosition != null)
                    {
                        school.longitude = schoolMaster.GeoPosition.Centroid.X;
                        school.latitude = schoolMaster.GeoPosition.Centroid.Y;
                    }
                    school.tblSchoolDetail.TableSchoolMaster = schoolMaster;
                    school.tblSchoolDetail.TableSchoolMaster.GeoPosition = null;
                    school.tblSchoolDetail = schoolDetails;
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
            var schoolMaster =  this.context?.TableSchoolMaster.FirstOrDefault(x => x.TenantId == school.tblSchoolDetail.TableSchoolMaster.TenantId && x.SchoolId == school.tblSchoolDetail.TableSchoolMaster.SchoolId);
            var schoolDetails = this.context?.TableSchoolDetail.FirstOrDefault(x => x.TenantId == school.tblSchoolDetail.TableSchoolMaster.TenantId && x.SchoolId == school.tblSchoolDetail.TableSchoolMaster.SchoolId);
            if (schoolMaster != null && schoolDetails != null)
            {
                schoolMaster.SchoolAltId = school.tblSchoolDetail.TableSchoolMaster.SchoolAltId;
                schoolMaster.SchoolStateId = school.tblSchoolDetail.TableSchoolMaster.SchoolStateId;
                schoolMaster.SchoolDistrictId = school.tblSchoolDetail.TableSchoolMaster.SchoolDistrictId;
                schoolMaster.SchoolLevel = school.tblSchoolDetail.TableSchoolMaster.SchoolLevel;
                schoolMaster.SchoolClassification = school.tblSchoolDetail.TableSchoolMaster.SchoolClassification;
                schoolMaster.SchoolName = school.tblSchoolDetail.TableSchoolMaster.SchoolName;
                schoolMaster.AlternateName = school.tblSchoolDetail.TableSchoolMaster.AlternateName;
                schoolMaster.StreetAddress1 = school.tblSchoolDetail.TableSchoolMaster.StreetAddress1;
                schoolMaster.StreetAddress2 = school.tblSchoolDetail.TableSchoolMaster.StreetAddress2;
                schoolMaster.City = school.tblSchoolDetail.TableSchoolMaster.City;
                schoolMaster.County = school.tblSchoolDetail.TableSchoolMaster.County;
                schoolMaster.Division = school.tblSchoolDetail.TableSchoolMaster.Division;
                schoolMaster.State = school.tblSchoolDetail.TableSchoolMaster.State;
                schoolMaster.District = school.tblSchoolDetail.TableSchoolMaster.District;
                schoolMaster.Zip = school.tblSchoolDetail.TableSchoolMaster.Zip;
                schoolMaster.Country = school.tblSchoolDetail.TableSchoolMaster.Country;
                schoolMaster.Features = school.tblSchoolDetail.TableSchoolMaster.Features;
                //schoolMaster.Plan_id = school.tblSchoolMaster.Plan_id;
                //public Geometry Latitude
                //public Geometry Longitude
                schoolMaster.CreatedBy = school.tblSchoolDetail.TableSchoolMaster.CreatedBy;
                schoolMaster.DateCreated = school.tblSchoolDetail.TableSchoolMaster.DateCreated;
                schoolMaster.ModifiedBy = school.tblSchoolDetail.TableSchoolMaster.ModifiedBy;
                schoolMaster.DateModifed = school.tblSchoolDetail.TableSchoolMaster.DateModifed;
                schoolMaster.MaxApiChecks = school.tblSchoolDetail.TableSchoolMaster.MaxApiChecks;


                schoolDetails.Affiliation = school.tblSchoolDetail.Affiliation;
                schoolDetails.Associations = school.tblSchoolDetail.Associations;
                schoolDetails.Locale = school.tblSchoolDetail.Locale;
                schoolDetails.LowestGradeLevel = school.tblSchoolDetail.LowestGradeLevel;
                schoolDetails.HighestGradeLevel = school.tblSchoolDetail.HighestGradeLevel;
                schoolDetails.DateSchoolOpened = school.tblSchoolDetail.DateSchoolOpened;
                schoolDetails.DateSchoolClosed = school.tblSchoolDetail.DateSchoolClosed;
                schoolDetails.Status = school.tblSchoolDetail.Status;
                schoolDetails.Gender = school.tblSchoolDetail.Gender;
                schoolDetails.Internet = school.tblSchoolDetail.Internet;
                schoolDetails.Electricity = school.tblSchoolDetail.Electricity;
                schoolDetails.Telephone = school.tblSchoolDetail.Telephone;
                schoolDetails.Fax = school.tblSchoolDetail.Fax;
                schoolDetails.Website = school.tblSchoolDetail.Website;
                schoolDetails.Email = school.tblSchoolDetail.Email;
                schoolDetails.Facebook = school.tblSchoolDetail.Facebook;
                schoolDetails.Twitter = school.tblSchoolDetail.Twitter;
                schoolDetails.Instagram = school.tblSchoolDetail.Instagram;
                schoolDetails.Youtube = school.tblSchoolDetail.Youtube;
                schoolDetails.LinkedIn = school.tblSchoolDetail.LinkedIn;
                schoolDetails.NameOfPrincipal = school.tblSchoolDetail.NameOfPrincipal;
                schoolDetails.NameOfAssistantPrincipal = school.tblSchoolDetail.NameOfAssistantPrincipal;
                schoolDetails.SchoolLogo = school.tblSchoolDetail.SchoolLogo;
                schoolDetails.RunningWater = school.tblSchoolDetail.RunningWater;
                schoolDetails.MainSourceOfDrinkingWater = school.tblSchoolDetail.MainSourceOfDrinkingWater;
                schoolDetails.CurrentlyAvailable = school.tblSchoolDetail.CurrentlyAvailable;
                schoolDetails.FemaleToiletType = school.tblSchoolDetail.FemaleToiletType;
                schoolDetails.TotalFemaleToilets = school.tblSchoolDetail.TotalFemaleToilets;
                schoolDetails.TotalFemaleToiletsUsable = school.tblSchoolDetail.TotalFemaleToiletsUsable;
                schoolDetails.FemaleToiletAccessibility = school.tblSchoolDetail.FemaleToiletAccessibility;
                schoolDetails.MaleToiletType = school.tblSchoolDetail.MaleToiletType;
                schoolDetails.TotalMaleToilets = school.tblSchoolDetail.TotalMaleToilets;
                schoolDetails.TotalMaleToiletsUsable = school.tblSchoolDetail.TotalMaleToiletsUsable;
                schoolDetails.MaleToiletAccessibility = school.tblSchoolDetail.MaleToiletAccessibility;
                schoolDetails.ComonToiletType = school.tblSchoolDetail.ComonToiletType;
                schoolDetails.TotalCommonToilets = school.tblSchoolDetail.TotalCommonToilets;
                schoolDetails.TotalCommonToiletsUsable = school.tblSchoolDetail.TotalCommonToiletsUsable;
                schoolDetails.CommonToiletAccessibility = school.tblSchoolDetail.CommonToiletAccessibility;
                schoolDetails.HandwashingAvailable = school.tblSchoolDetail.HandwashingAvailable;
                schoolDetails.SoapAndWaterAvailable = school.tblSchoolDetail.SoapAndWaterAvailable;
                schoolDetails.HygeneEducation = school.tblSchoolDetail.HygeneEducation;

                this.context?.SaveChanges();
                school._failure = false;
                return school;
            }
            else
            {
                school.tblSchoolDetail = null;
                school.tblSchoolDetail.TableSchoolMaster = null;
                school._failure = true;
                school._message = NORECORDFOUND;
                return school;
            }
        }
        public SchoolAddViewModel AddSchool(SchoolAddViewModel school)
        {
            try
            {
                //int? MasterSchoolId = Utility<TableSchoolMaster>.GetMaxPK(this.context, new Func<TableSchoolMaster, int>(x => x.SchoolId));
               // int? Detail_Id = (int)Utility<TableSchoolDetail>.GetMaxPK(this.context, new Func<TableSchoolDetail, int>(x => x.Id));

                int? MasterSchoolId = Utility.GetMaxPK(this.context, new Func<TableSchoolMaster, int>(x => x.SchoolId));
                int? Detail_Id = (int)Utility.GetMaxPK(this.context, new Func<TableSchoolDetail, int>(x => x.Id));

                Point currentLocation = null;
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                if (school.longitude != null && school.latitude != null)
                {
                    currentLocation = geometryFactory.CreatePoint(new Coordinate((double)school.longitude, (double)school.latitude));
                }

                var schoolMaster = this.context?.TableSchoolMaster.ToList().OrderBy(x => x.SchoolId).LastOrDefault();
                var schoolDetail = this.context?.TableSchoolDetail.ToList().OrderBy(x => x.SchoolId).LastOrDefault();
                school.tblSchoolDetail.TableSchoolMaster.SchoolId = (int)MasterSchoolId;
                school.tblSchoolDetail.SchoolId = MasterSchoolId;
                school.tblSchoolDetail.Id = (int)Detail_Id;
                school.tblSchoolDetail.TableSchoolMaster.DateCreated = DateTime.UtcNow;
                school.tblSchoolDetail.TableSchoolMaster.TenantId = school.tblSchoolDetail.TableSchoolMaster.TenantId;
                school.tblSchoolDetail.TableSchoolMaster.GeoPosition = currentLocation;
                school.tblSchoolDetail.TenantId = school.tblSchoolDetail.TableSchoolMaster.TenantId;

                this.context?.TableSchoolDetail.Add(school.tblSchoolDetail);

                this.context?.SaveChanges();
                school._failure = false;
                school.tblSchoolDetail.TableSchoolMaster.GeoPosition = null;
                return school;

            }
            catch (Exception es)
            {

                throw;
            }

        }
        
        
    }
}
