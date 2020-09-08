using Microsoft.EntityFrameworkCore;
using opensis.core.helper;
using opensis.core.School.Interfaces;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opensis.core.School.Services
{
    public class SchoolRegister : ISchoolRegisterService
    {

        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";
        public ISchoolRepository schoolRepository;
        public SchoolRegister(ISchoolRepository schoolRepository)
        {
            this.schoolRepository = schoolRepository;
        }

        //Required for Unit Testing
        public SchoolRegister() { }
        //public List<Schools> getAllSchools(opensisContext context)
        public SchoolListViewModel getAllSchools(SchoolViewModel objModel)
        {
            logger.Info("Method getAllSchools called.");
            SchoolListViewModel schoolList = new SchoolListViewModel();
            try
            {
                if (TokenManager.CheckToken(objModel._tenantName, objModel._token))
                {
                    List<Schools> schools = this.schoolRepository.GetAllSchools();
                    schoolList.schoolList = schools;
                    schoolList._message = SUCCESS;
                    schoolList._failure = false;
                    logger.Info("Method getAllSchools end with success.");
                }
                else
                {
                    schoolList._failure = true;
                    schoolList._message = TOKENINVALID;
                }


            }
            catch (Exception ex)
            {
                schoolList._message = ex.Message;
                schoolList._failure = true;
                logger.Error("Method getAllSchools end with error :" + ex.Message);
            }


            return schoolList;
        }

        //public List<Schools> SaveSchool(Schools schools, opensisContext context)
        public SchoolListViewModel SaveSchool(Schools schools)
        {


            //context.tblSchool.Add(schools);
            //context.SaveChanges();

            if (IsMandatoryFieldsArePresent(schools))
            {
                this.schoolRepository.AddSchools(schools);
                //return getAllSchools();
                return null;
            }
            else
            {
                return null;
            }

        }
        public async Task<SchoolAddViewMopdel> UpdateSchool(SchoolAddViewMopdel schools)
        {
            SchoolAddViewMopdel schoolAddViewMopdel = new SchoolAddViewMopdel();
            if (TokenManager.CheckToken(schools._tenantName, schools._token))
            {
                schoolAddViewMopdel = await this.schoolRepository.UpdateSchool(schools);
                //return getAllSchools();
                return schoolAddViewMopdel;
            }
            else
            {
                schoolAddViewMopdel._failure = true;
                schoolAddViewMopdel._message = TOKENINVALID;
                return schoolAddViewMopdel;
            }

        }

        public async Task<SchoolAddViewMopdel> SaveSchool(SchoolAddViewMopdel schools)
        {
            SchoolAddViewMopdel schoolAddViewMopdel = new SchoolAddViewMopdel();
            if (TokenManager.CheckToken(schools._tenantName, schools._token))
            {
                schoolAddViewMopdel = await this.schoolRepository.AddSchool(schools);
                //return getAllSchools();
                return schoolAddViewMopdel;
            }
            else
            {
                schoolAddViewMopdel._failure = true;
                schoolAddViewMopdel._message = TOKENINVALID;
                return schoolAddViewMopdel;
            }

        }
        public async Task<SchoolAddViewMopdel> ViewSchool(SchoolAddViewMopdel schools)
        {
            SchoolAddViewMopdel schoolAddViewMopdel = new SchoolAddViewMopdel();
            if (TokenManager.CheckToken(schools._tenantName, schools._token))
            {
                schoolAddViewMopdel = await this.schoolRepository.ViewSchool(schools);
                //return getAllSchools();
                return schoolAddViewMopdel;

            }
            else
            {
                schoolAddViewMopdel._failure = true;
                schoolAddViewMopdel._message = TOKENINVALID;
                return schoolAddViewMopdel;
            }

        }

        public async Task<SchoolAddViewMopdel> EditSchool(SchoolAddViewMopdel schools)
        {
            SchoolAddViewMopdel schoolAddViewMopdel = new SchoolAddViewMopdel();
            if (TokenManager.CheckToken(schools._tenantName, schools._token))
            {
                schoolAddViewMopdel = await this.schoolRepository.EditSchool(schools);
                //return getAllSchools();
                return schoolAddViewMopdel;

            }
            else
            {
                schoolAddViewMopdel._failure = true;
                schoolAddViewMopdel._message = TOKENINVALID;
                return schoolAddViewMopdel;
            }

        }

        public bool IsMandatoryFieldsArePresent(Schools schools)
        {
            bool isvalid = false;
            if (schools.tenant_id != "" && schools.school_name != "")
            {
                isvalid = true;
            }

            return isvalid;
        }
    }
}
