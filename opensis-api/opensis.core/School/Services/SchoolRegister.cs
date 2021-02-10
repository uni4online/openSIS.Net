﻿using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Check School InternalId Exist or Not
        /// </summary>
        /// <param name="checkSchoolInternalIdViewModel"></param>
        /// <returns></returns>
        public CheckSchoolInternalIdViewModel CheckSchoolInternalId(CheckSchoolInternalIdViewModel checkSchoolInternalIdViewModel)
        {
            CheckSchoolInternalIdViewModel checkInternalId = new CheckSchoolInternalIdViewModel();
            if (TokenManager.CheckToken(checkSchoolInternalIdViewModel._tenantName, checkSchoolInternalIdViewModel._token))
            {
                checkInternalId = this.schoolRepository.CheckSchoolInternalId(checkSchoolInternalIdViewModel);
            }
            else
            {
                checkInternalId._failure = true;
                checkInternalId._message = TOKENINVALID;
            }
            return checkInternalId;
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
        /// <summary>
        /// Student Enrollment School List
        /// </summary>
        /// <param name="schoolListViewModel"></param>
        /// <returns></returns>
        public SchoolListViewModel StudentEnrollmentSchoolList(SchoolListViewModel schoolListViewModel)
        {
            logger.Info("Method studentEnrollmentSchoolList called.");
            SchoolListViewModel schoolListView = new SchoolListViewModel();
            try
            {
                if (TokenManager.CheckToken(schoolListViewModel._tenantName, schoolListViewModel._token))
                {
                    schoolListView = this.schoolRepository.StudentEnrollmentSchoolList(schoolListViewModel);
                    schoolListView._message = SUCCESS;
                    schoolListView._failure = false;
                    logger.Info("Method StudentEnrollmentSchoolList end with success.");
                }
                else
                {
                    schoolListView._failure = true;
                    schoolListView._message = TOKENINVALID;
                }
            }
            catch (Exception ex)
            {
                schoolListView._message = ex.Message;
                schoolListView._failure = true;
                logger.Error("Method StudentEnrollmentSchoolList end with error :" + ex.Message);
            }
            return schoolListView;
        }
    }
}
