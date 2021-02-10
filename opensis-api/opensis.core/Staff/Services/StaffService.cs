﻿using opensis.core.helper;
using opensis.core.Staff.Interfaces;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Staff.Services
{
    public class StaffService : IStaffService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public IStaffRepository staffRepository;
        public StaffService(IStaffRepository staffRepository)
        {
            this.staffRepository = staffRepository;
        }
      
        /// <summary>
        /// Add Staff
        /// </summary>
        /// <param name="StaffAddViewModel"></param>
        /// <returns></returns>
        public StaffAddViewModel AddStaff(StaffAddViewModel staffAddViewModel)
        {
            StaffAddViewModel staffInfoAddViewModel = new StaffAddViewModel();
            try
            {
                if (TokenManager.CheckToken(staffAddViewModel._tenantName, staffAddViewModel._token))
                {

                    staffInfoAddViewModel = this.staffRepository.AddStaff(staffAddViewModel);

                }
                else
                {
                    staffInfoAddViewModel._failure = true;
                    staffInfoAddViewModel._message = TOKENINVALID;

                }
            }
            catch (Exception es)
            {

                staffInfoAddViewModel._failure = true;
                staffInfoAddViewModel._message = es.Message;
            }
            return staffInfoAddViewModel;

        }

        /// <summary>
        /// Get All Staff List
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public StaffListModel GetAllStaffList(PageResult pageResult)
        {
            logger.Info("Method getAllStaffList called.");
            StaffListModel staffList = new StaffListModel();
            try
            {
                if (TokenManager.CheckToken(pageResult._tenantName, pageResult._token))
                {
                    staffList = this.staffRepository.GetAllStaffList(pageResult);
                    if (staffList.staffMaster.Count > 0)
                    {
                        staffList._message = SUCCESS;
                        staffList._failure = false;
                    }
                    else
                    {
                        staffList._message = "NO RECORD FOUND";
                        staffList._failure = true;
                    }
                    logger.Info("Method getAllStaffList end with success.");
                }

                else
                {
                    staffList._failure = true;
                    staffList._message = TOKENINVALID;
                    return staffList;
                }
            }
            catch (Exception ex)
            {
                staffList._message = ex.Message;
                staffList._failure = true;
                logger.Error("Method getAllStaffList end with error :" + ex.Message);
            }
            return staffList;
        }

        /// <summary>
        /// View Staff By Id
        /// </summary>
        /// <param name="staffAddViewModel"></param>
        /// <returns></returns>
        public StaffAddViewModel ViewStaff(StaffAddViewModel staffAddViewModel)
        {
            StaffAddViewModel staffView = new StaffAddViewModel();
            if (TokenManager.CheckToken(staffAddViewModel._tenantName, staffAddViewModel._token))
            {
                staffView = this.staffRepository.ViewStaff(staffAddViewModel);
            }
            else
            {
                staffView._failure = true;
                staffView._message = TOKENINVALID;
            }
            return staffView;
        }

        /// <summary>
        /// Update Staff
        /// </summary>
        /// <param name="staffAddViewModel"></param>
        /// <returns></returns>
        public StaffAddViewModel UpdateStaff(StaffAddViewModel staffAddViewModel)
        {
            StaffAddViewModel staffUpdate = new StaffAddViewModel();
            if (TokenManager.CheckToken(staffAddViewModel._tenantName, staffAddViewModel._token))
            {
                staffUpdate = this.staffRepository.UpdateStaff(staffAddViewModel);
            }
            else
            {
                staffUpdate._failure = true;
                staffUpdate._message = TOKENINVALID;
            }
            return staffUpdate;
        }

        /// <summary>
        /// Check Staff InternalId
        /// </summary>
        /// <param name="checkStaffInternalIdViewModel"></param>
        /// <returns></returns>
        public CheckStaffInternalIdViewModel CheckStaffInternalId(CheckStaffInternalIdViewModel checkStaffInternalIdViewModel)
        {
            CheckStaffInternalIdViewModel checkInternalId = new CheckStaffInternalIdViewModel();
            if (TokenManager.CheckToken(checkStaffInternalIdViewModel._tenantName, checkStaffInternalIdViewModel._token))
            {
                checkInternalId = this.staffRepository.CheckStaffInternalId(checkStaffInternalIdViewModel);
            }
            else
            {
                checkInternalId._failure = true;
                checkInternalId._message = TOKENINVALID;
            }
            return checkInternalId;
        }

        /// <summary>
        /// Add StaffSchoolInfo
        /// </summary>
        /// <param name="staffSchoolInfoAddViewModel"></param>
        /// <returns></returns>
        public StaffSchoolInfoAddViewModel AddStaffSchoolInfo(StaffSchoolInfoAddViewModel staffSchoolInfoAddViewModel)
        {
            StaffSchoolInfoAddViewModel staffSchoolInfoAdd = new StaffSchoolInfoAddViewModel();
            try
            {
                if (TokenManager.CheckToken(staffSchoolInfoAddViewModel._tenantName, staffSchoolInfoAddViewModel._token))
                {
                    staffSchoolInfoAdd = this.staffRepository.AddStaffSchoolInfo(staffSchoolInfoAddViewModel);
                }
                else
                {
                    staffSchoolInfoAdd._failure = true;
                    staffSchoolInfoAdd._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                staffSchoolInfoAdd._failure = true;
                staffSchoolInfoAdd._message = es.Message;
            }
            return staffSchoolInfoAdd;
        }

        /// <summary>
        /// View StaffSchoolInfo
        /// </summary>
        /// <param name="staffSchoolInfoAddViewModel"></param>
        /// <returns></returns>
        public StaffSchoolInfoAddViewModel ViewStaffSchoolInfo(StaffSchoolInfoAddViewModel staffSchoolInfoAddViewModel)
        {
            StaffSchoolInfoAddViewModel staffSchoolInfoView = new StaffSchoolInfoAddViewModel();
            if (TokenManager.CheckToken(staffSchoolInfoAddViewModel._tenantName, staffSchoolInfoAddViewModel._token))
            {
                staffSchoolInfoView = this.staffRepository.ViewStaffSchoolInfo(staffSchoolInfoAddViewModel);
            }
            else
            {
                staffSchoolInfoView._failure = true;
                staffSchoolInfoView._message = TOKENINVALID;
            }
            return staffSchoolInfoView;
        }

        /// <summary>
        /// Update StaffSchoolInfo
        /// </summary>
        /// <param name="staffSchoolInfoAddViewModel"></param>
        /// <returns></returns>
        public StaffSchoolInfoAddViewModel UpdateStaffSchoolInfo(StaffSchoolInfoAddViewModel staffSchoolInfoAddViewModel)
        {
            StaffSchoolInfoAddViewModel staffSchoolInfoUpdate = new StaffSchoolInfoAddViewModel();
            try
            {
                if (TokenManager.CheckToken(staffSchoolInfoAddViewModel._tenantName, staffSchoolInfoAddViewModel._token))
                {
                    staffSchoolInfoUpdate = this.staffRepository.UpdateStaffSchoolInfo(staffSchoolInfoAddViewModel);
                }
                else
                {
                    staffSchoolInfoUpdate._failure = true;
                    staffSchoolInfoUpdate._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                staffSchoolInfoUpdate._failure = true;
                staffSchoolInfoUpdate._message = es.Message;
            }
            return staffSchoolInfoUpdate;
        }

        /// <summary>
        /// Add Staff Certificate Info
        /// </summary>
        /// <param name="staffCertificateInfoAddViewModel"></param>
        /// <returns></returns>
        public StaffCertificateInfoAddViewModel AddStaffCertificateInfo(StaffCertificateInfoAddViewModel staffCertificateInfoAddViewModel)
        {
            StaffCertificateInfoAddViewModel staffCertificateInfoAdd = new StaffCertificateInfoAddViewModel();
            try
            {
                if (TokenManager.CheckToken(staffCertificateInfoAddViewModel._tenantName, staffCertificateInfoAddViewModel._token))
                {

                    staffCertificateInfoAdd = this.staffRepository.AddStaffCertificateInfo(staffCertificateInfoAddViewModel);

                }
                else
                {
                    staffCertificateInfoAdd._failure = true;
                    staffCertificateInfoAdd._message = TOKENINVALID;

                }
            }
            catch (Exception es)
            {

                staffCertificateInfoAdd._failure = true;
                staffCertificateInfoAdd._message = es.Message;
            }
            return staffCertificateInfoAdd;
        }
        /// <summary>
        /// Get All Staff Certificate Info
        /// </summary>
        /// <param name="staffCertificateInfoListModel"></param>
        /// <returns></returns>
        public StaffCertificateInfoListModel GetAllStaffCertificateInfo(StaffCertificateInfoListModel staffCertificateInfoListModel)
        {
            StaffCertificateInfoListModel staffCertificateInfoListView = new StaffCertificateInfoListModel();
            try
            {
                if (TokenManager.CheckToken(staffCertificateInfoListModel._tenantName, staffCertificateInfoListModel._token))
                {
                    staffCertificateInfoListView = this.staffRepository.GetAllStaffCertificateInfo(staffCertificateInfoListModel);
                }
                else
                {
                    staffCertificateInfoListView._failure = true;
                    staffCertificateInfoListView._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                staffCertificateInfoListView._failure = true;
                staffCertificateInfoListView._message = es.Message;
            }
            return staffCertificateInfoListView;
        }
        /// <summary>
        /// Update Staff Certificate Info
        /// </summary>
        /// <param name="staffCertificateInfoAddViewModel"></param>
        /// <returns></returns>
        public StaffCertificateInfoAddViewModel UpdateStaffCertificateInfo(StaffCertificateInfoAddViewModel staffCertificateInfoAddViewModel)
        {
            StaffCertificateInfoAddViewModel staffCertificateInfoUpdate = new StaffCertificateInfoAddViewModel();
            try
            {
                if (TokenManager.CheckToken(staffCertificateInfoAddViewModel._tenantName, staffCertificateInfoAddViewModel._token))
                {
                    staffCertificateInfoUpdate = this.staffRepository.UpdateStaffCertificateInfo(staffCertificateInfoAddViewModel);
                }
                else
                {
                    staffCertificateInfoUpdate._failure = true;
                    staffCertificateInfoUpdate._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                staffCertificateInfoUpdate._failure = true;
                staffCertificateInfoUpdate._message = es.Message;
            }
            return staffCertificateInfoUpdate;
        }
        /// <summary>
        /// Delete Staff Certificate Info
        /// </summary>
        /// <param name="staffCertificateInfoAddViewModel"></param>
        /// <returns></returns>
        public StaffCertificateInfoAddViewModel DeleteStaffCertificateInfo(StaffCertificateInfoAddViewModel staffCertificateInfoAddViewModel)
        {
            StaffCertificateInfoAddViewModel staffCertificateInfoDelete = new StaffCertificateInfoAddViewModel();
            try
            {
                if (TokenManager.CheckToken(staffCertificateInfoAddViewModel._tenantName, staffCertificateInfoAddViewModel._token))
                {
                    staffCertificateInfoDelete = this.staffRepository.DeleteStaffCertificateInfo(staffCertificateInfoAddViewModel);
                }
                else
                {
                    staffCertificateInfoDelete._failure = true;
                    staffCertificateInfoDelete._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                staffCertificateInfoDelete._failure = true;
                staffCertificateInfoDelete._message = es.Message;
            }
            return staffCertificateInfoDelete;
        }

        /// <summary>
        /// Add or Update Staff Photo
        /// </summary>
        /// <param name="staffAddViewModel"></param>
        /// <returns></returns>
        public StaffAddViewModel AddUpdateStaffPhoto(StaffAddViewModel staffAddViewModel)
        {
            StaffAddViewModel staffPhotoUpdate = new StaffAddViewModel();
            if (TokenManager.CheckToken(staffAddViewModel._tenantName, staffAddViewModel._token))
            {
                staffPhotoUpdate = this.staffRepository.AddUpdateStaffPhoto(staffAddViewModel);
            }
            else
            {
                staffPhotoUpdate._failure = true;
                staffPhotoUpdate._message = TOKENINVALID;
            }
            return staffPhotoUpdate;
        }
    }
}
