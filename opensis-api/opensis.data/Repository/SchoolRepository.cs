using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
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

        public async Task<SchoolAddViewMopdel> ViewSchool(SchoolAddViewMopdel school)
        {
            try
            {
                SchoolAddViewMopdel schoolAddViewMopdel = new SchoolAddViewMopdel();
                var schoolMaster = await this.context?.Table_School_Master.FirstOrDefaultAsync(x => x.Tenant_Id == school.tblSchoolDetail.SchoolMaster.Tenant_Id && x.School_Id == school.tblSchoolDetail.SchoolMaster.School_Id);
                var schoolDetails = await this.context?.Table_School_Detail.FirstOrDefaultAsync(x => x.Tenant_Id == school.tblSchoolDetail.SchoolMaster.Tenant_Id && x.School_Id == school.tblSchoolDetail.SchoolMaster.School_Id);
                if (schoolMaster != null)
                {
                    if (schoolMaster.GeoPosition != null)
                    {
                        school.longitude = schoolMaster.GeoPosition.Centroid.X;
                        school.latitude = schoolMaster.GeoPosition.Centroid.Y;
                    }
                    school.tblSchoolDetail.SchoolMaster = schoolMaster;
                    school.tblSchoolDetail.SchoolMaster.GeoPosition = null;
                    school.tblSchoolDetail = schoolDetails;
                    school._tenantName = school._tenantName;
                    return school;
                }
                else
                {
                    schoolAddViewMopdel._failure = true;
                    schoolAddViewMopdel._message = NORECORDFOUND;
                    return schoolAddViewMopdel;
                }
            }
            catch (Exception es)
            {

                throw;
            }

        }

        public async Task<SchoolAddViewMopdel> UpdateSchool(SchoolAddViewMopdel school)
        {
            var schoolMaster = await this.context?.Table_School_Master.FirstOrDefaultAsync(x => x.Tenant_Id == school.tblSchoolDetail.SchoolMaster.Tenant_Id && x.School_Id == school.tblSchoolDetail.SchoolMaster.School_Id);
            var schoolDetails = await this.context?.Table_School_Detail.FirstOrDefaultAsync(x => x.Tenant_Id == school.tblSchoolDetail.SchoolMaster.Tenant_Id && x.School_Id == school.tblSchoolDetail.SchoolMaster.School_Id);
            if (schoolMaster != null && schoolDetails != null)
            {
                schoolMaster.School_Alt_Id = school.tblSchoolDetail.SchoolMaster.School_Alt_Id;
                schoolMaster.School_State_Id = school.tblSchoolDetail.SchoolMaster.School_State_Id;
                schoolMaster.School_District_Id = school.tblSchoolDetail.SchoolMaster.School_District_Id;
                schoolMaster.School_Level = school.tblSchoolDetail.SchoolMaster.School_Level;
                schoolMaster.School_Classification = school.tblSchoolDetail.SchoolMaster.School_Classification;
                schoolMaster.School_Name = school.tblSchoolDetail.SchoolMaster.School_Name;
                schoolMaster.Alternate_Name = school.tblSchoolDetail.SchoolMaster.Alternate_Name;
                schoolMaster.Street_Address_1 = school.tblSchoolDetail.SchoolMaster.Street_Address_1;
                schoolMaster.Street_Address_2 = school.tblSchoolDetail.SchoolMaster.Street_Address_2;
                schoolMaster.City = school.tblSchoolDetail.SchoolMaster.City;
                schoolMaster.County = school.tblSchoolDetail.SchoolMaster.County;
                schoolMaster.Division = school.tblSchoolDetail.SchoolMaster.Division;
                schoolMaster.State = school.tblSchoolDetail.SchoolMaster.State;
                schoolMaster.District = school.tblSchoolDetail.SchoolMaster.District;
                schoolMaster.Zip = school.tblSchoolDetail.SchoolMaster.Zip;
                schoolMaster.Country = school.tblSchoolDetail.SchoolMaster.Country;
                schoolMaster.Features = school.tblSchoolDetail.SchoolMaster.Features;
                //schoolMaster.Plan_id = school.tblSchoolMaster.Plan_id;
                //public Geometry Latitude
                //public Geometry Longitude
                schoolMaster.Created_By = school.tblSchoolDetail.SchoolMaster.Created_By;
                schoolMaster.Date_Created = school.tblSchoolDetail.SchoolMaster.Date_Created;
                schoolMaster.Modified_By = school.tblSchoolDetail.SchoolMaster.Modified_By;
                schoolMaster.Date_Modifed = school.tblSchoolDetail.SchoolMaster.Date_Modifed;
                schoolMaster.Max_api_checks = school.tblSchoolDetail.SchoolMaster.Max_api_checks;


                schoolDetails.Affiliation = school.tblSchoolDetail.Affiliation;
                schoolDetails.Associations = school.tblSchoolDetail.Associations;
                schoolDetails.Locale = school.tblSchoolDetail.Locale;
                schoolDetails.Lowest_Grade_Level = school.tblSchoolDetail.Lowest_Grade_Level;
                schoolDetails.Highest_Grade_Level = school.tblSchoolDetail.Highest_Grade_Level;
                schoolDetails.Date_School_Opened = school.tblSchoolDetail.Date_School_Opened;
                schoolDetails.Date_School_Closed = school.tblSchoolDetail.Date_School_Closed;
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
                schoolDetails.Name_of_Principal = school.tblSchoolDetail.Name_of_Principal;
                schoolDetails.Name_of_Assistant_Principal = school.tblSchoolDetail.Name_of_Assistant_Principal;
                schoolDetails.School_Logo = school.tblSchoolDetail.School_Logo;
                schoolDetails.Running_Water = school.tblSchoolDetail.Running_Water;
                schoolDetails.Main_Source_of_Drinking_Water = school.tblSchoolDetail.Main_Source_of_Drinking_Water;
                schoolDetails.Currently_Available = school.tblSchoolDetail.Currently_Available;
                schoolDetails.Female_Toilet_Type = school.tblSchoolDetail.Female_Toilet_Type;
                schoolDetails.Total_Female_Toilets = school.tblSchoolDetail.Total_Female_Toilets;
                schoolDetails.Total_Female_Toilets_Usable = school.tblSchoolDetail.Total_Female_Toilets_Usable;
                schoolDetails.Female_Toilet_Accessibility = school.tblSchoolDetail.Female_Toilet_Accessibility;
                schoolDetails.Male_Toilet_Type = school.tblSchoolDetail.Male_Toilet_Type;
                schoolDetails.Total_Male_Toilets = school.tblSchoolDetail.Total_Male_Toilets;
                schoolDetails.Total_Male_Toilets_Usable = school.tblSchoolDetail.Total_Male_Toilets_Usable;
                schoolDetails.Male_Toilet_Accessibility = school.tblSchoolDetail.Male_Toilet_Accessibility;
                schoolDetails.Comon_Toilet_Type = school.tblSchoolDetail.Comon_Toilet_Type;
                schoolDetails.Total_Common_Toilets = school.tblSchoolDetail.Total_Common_Toilets;
                schoolDetails.Total_Common_Toilets_Usable = school.tblSchoolDetail.Total_Common_Toilets_Usable;
                schoolDetails.Common_Toilet_Accessibility = school.tblSchoolDetail.Common_Toilet_Accessibility;
                schoolDetails.Handwashing_Available = school.tblSchoolDetail.Handwashing_Available;
                schoolDetails.Soap_and_Water_Available = school.tblSchoolDetail.Soap_and_Water_Available;
                schoolDetails.Hygene_Education = school.tblSchoolDetail.Hygene_Education;

                await this.context?.SaveChangesAsync();
                school._failure = false;
                return school;
            }
            else
            {
                school.tblSchoolDetail = null;
                school.tblSchoolDetail.SchoolMaster = null;
                school._failure = true;
                school._message = NORECORDFOUND;
                return school;
            }
        }
        public async Task<SchoolAddViewMopdel> AddSchool(SchoolAddViewMopdel school)
        {
            try
            {
                Point currentLocation=null;
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                if (school.longitude != null && school.latitude != null)
                {
                    currentLocation = geometryFactory.CreatePoint(new Coordinate((double)school.longitude, (double)school.latitude));
                }
                
                var schoolMaster = this.context?.Table_School_Master.ToList().OrderBy(x => x.School_Id).LastOrDefault();
                if (schoolMaster != null)
                {
                    school.tblSchoolDetail.SchoolMaster.School_Id = schoolMaster.School_Id + 1;
                    school.tblSchoolDetail.School_Id = schoolMaster.School_Id + 1;
                }
                else
                {
                    school.tblSchoolDetail.SchoolMaster.School_Id = 1;
                    school.tblSchoolDetail.School_Id = 1;
                }
                school.tblSchoolDetail.SchoolMaster.Tenant_Id = Guid.NewGuid();
                school.tblSchoolDetail.SchoolMaster.GeoPosition = currentLocation;
                school.tblSchoolDetail.Tenant_Id = school.tblSchoolDetail.SchoolMaster.Tenant_Id;

                this.context?.Table_School_Detail.Add(school.tblSchoolDetail);

                await this.context?.SaveChangesAsync();
                school._failure = false;
                school.tblSchoolDetail.SchoolMaster.GeoPosition = null;
                return school;

            }
            catch (Exception es)
            {

                throw;
            }

        }
        public async Task<SchoolAddViewMopdel> EditSchool(SchoolAddViewMopdel school)
        {
            try
            {
                SchoolAddViewMopdel schoolAddViewMopdel = new SchoolAddViewMopdel();
                var schoolMaster = await this.context?.Table_School_Master.FirstOrDefaultAsync(x => x.Tenant_Id == school.tblSchoolDetail.SchoolMaster.Tenant_Id && x.School_Id == school.tblSchoolDetail.SchoolMaster.School_Id);
                var schoolDetails = await this.context?.Table_School_Detail.FirstOrDefaultAsync(x => x.Tenant_Id == school.tblSchoolDetail.SchoolMaster.Tenant_Id && x.School_Id == school.tblSchoolDetail.SchoolMaster.School_Id);
                if (schoolMaster != null)
                {
                    if (schoolMaster.GeoPosition != null) 
                    {
                        school.longitude = schoolMaster.GeoPosition.Centroid.X;
                        school.latitude = schoolMaster.GeoPosition.Centroid.Y;
                    }
                    school.tblSchoolDetail.SchoolMaster = schoolMaster;
                    school.tblSchoolDetail.SchoolMaster.GeoPosition = null;
                    school.tblSchoolDetail = schoolDetails;
                    school._tenantName = school._tenantName;
                    return school;
                }
                else
                {
                    schoolAddViewMopdel._failure = true;
                    schoolAddViewMopdel._message = NORECORDFOUND;
                    return schoolAddViewMopdel;
                }
            }
            catch (Exception es)
            {

                throw;
            }

        }
    }
}
