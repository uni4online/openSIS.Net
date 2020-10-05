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
        
        public SchoolListModel GetAllSchools(SchoolListModel school)
        {
            logger.Info("Method getAllSchools called.");
            SchoolListModel schoolList = new SchoolListModel();
            try
            {
                if (TokenManager.CheckToken(school._tenantName, school._token))
                {
                    schoolList = this.schoolRepository.GetAllSchools(school);
                    schoolList._message = SUCCESS;
                    schoolList._failure = false;
                    logger.Info("Method getAllSchools end with success.");
                }

                else
                {
                    schoolList._failure = true;
                    schoolList._message = TOKENINVALID;
                    return schoolList;
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


        public SchoolListModel GetAllSchoolList(PageResult pageResult)
        {
            logger.Info("Method getAllSchoolList called.");
            SchoolListModel schoolList = new SchoolListModel();
            try
            {
                if (TokenManager.CheckToken(pageResult._tenantName, pageResult._token))
                {
                    schoolList = this.schoolRepository.GetAllSchoolList(pageResult);
                    schoolList._message = SUCCESS;
                    schoolList._failure = false;
                    logger.Info("Method getAllSchoolList end with success.");
                }

                else
                {
                    schoolList._failure = true;
                    schoolList._message = TOKENINVALID;
                    return schoolList;
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

        
        public SchoolAddViewModel UpdateSchool(SchoolAddViewModel schools)
        {
            SchoolAddViewModel SchoolAddViewModel = new SchoolAddViewModel();
            if (TokenManager.CheckToken(schools._tenantName, schools._token))
            {
                SchoolAddViewModel =  this.schoolRepository.UpdateSchool(schools);
                //return getAllSchools();
                return SchoolAddViewModel;
            }
            else
            {
                SchoolAddViewModel._failure = true;
                SchoolAddViewModel._message = TOKENINVALID;
                return SchoolAddViewModel;
            }

        }

        public SchoolAddViewModel SaveSchool(SchoolAddViewModel schools)
        {
            SchoolAddViewModel SchoolAddViewModel = new SchoolAddViewModel();
            if (TokenManager.CheckToken(schools._tenantName, schools._token))
            {
                
                    SchoolAddViewModel = this.schoolRepository.AddSchool(schools);
                    //return getAllSchools();
                    return SchoolAddViewModel;
               
            }
            else
            {
                SchoolAddViewModel._failure = true;
                SchoolAddViewModel._message = TOKENINVALID;
                return SchoolAddViewModel;
            }

        }
        public SchoolAddViewModel ViewSchool(SchoolAddViewModel schools)
        {
            SchoolAddViewModel SchoolAddViewModel = new SchoolAddViewModel();
            if (TokenManager.CheckToken(schools._tenantName, schools._token))
            {
                SchoolAddViewModel =  this.schoolRepository.ViewSchool(schools);
                //return getAllSchools();
                return SchoolAddViewModel;

            }
            else
            {
                SchoolAddViewModel._failure = true;
                SchoolAddViewModel._message = TOKENINVALID;
                return SchoolAddViewModel;
            }

        }

        //public bool IsMandatoryFieldsArePresent(Schools schools)
        //{
        //    bool isvalid = false;
        //    if (schools.tenant_id != "" && schools.school_name != "")
        //    {
        //        isvalid = true;
        //    }

        //    return isvalid;
        //}
        
    }
}
