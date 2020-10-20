using opensis.core.AttendanceCode.Interfaces;
using opensis.core.helper;
using opensis.data.Interface;
using opensis.data.ViewModels.AttendanceCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.AttendanceCode.Services
{
    public class AttendanceCodeRegister : IAttendanceCodeRegisterService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public IAttendanceCodeRepository attendanceCodeRepository;
        public AttendanceCodeRegister(IAttendanceCodeRepository attendanceCodeRepository)
        {
            this.attendanceCodeRepository = attendanceCodeRepository;
        }
        public AttendanceCodeRegister() { }
        /// <summary>
        /// Save AttendanceCode
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel SaveAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel AttendanceCodeAddViewModel = new AttendanceCodeAddViewModel();
            try
            {
                if (TokenManager.CheckToken(attendanceCodeAddViewModel._tenantName, attendanceCodeAddViewModel._token))
                {

                    AttendanceCodeAddViewModel = this.attendanceCodeRepository.AddAttendanceCode(attendanceCodeAddViewModel);
                    return AttendanceCodeAddViewModel;

                }
                else
                {
                    AttendanceCodeAddViewModel._failure = true;
                    AttendanceCodeAddViewModel._message = TOKENINVALID;
                    return AttendanceCodeAddViewModel;
                }
            }
            catch (Exception es)
            {

                AttendanceCodeAddViewModel._failure = true;
                AttendanceCodeAddViewModel._message = es.Message;
            }
            return AttendanceCodeAddViewModel;

        }
        /// <summary>
        /// Get AttendanceCode By Id
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel ViewAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodeViewModel = new AttendanceCodeAddViewModel();
            try
            {
                if (TokenManager.CheckToken(attendanceCodeAddViewModel._tenantName, attendanceCodeAddViewModel._token))
                {
                    attendanceCodeViewModel = this.attendanceCodeRepository.ViewAttendanceCode(attendanceCodeAddViewModel);
                }
                else
                {
                    attendanceCodeViewModel._failure = true;
                    attendanceCodeViewModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                attendanceCodeViewModel._failure = true;
                attendanceCodeViewModel._message = es.Message;
            }
            return attendanceCodeViewModel;
        }
        /// <summary>
        /// Update AttendanceCode
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel UpdateAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodeUpdate = new AttendanceCodeAddViewModel();
            try
            {
                if (TokenManager.CheckToken(attendanceCodeAddViewModel._tenantName, attendanceCodeAddViewModel._token))
                {
                    attendanceCodeUpdate = this.attendanceCodeRepository.UpdateAttendanceCode(attendanceCodeAddViewModel);
                }
                else
                {
                    attendanceCodeUpdate._failure = true;
                    attendanceCodeUpdate._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                attendanceCodeUpdate._failure = true;
                attendanceCodeUpdate._message = es.Message;
            }

            return attendanceCodeUpdate;
        }
        /// <summary>
        /// Delete AttendanceCode
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel DeleteAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodedelete = new AttendanceCodeAddViewModel();
            try
            {
                if (TokenManager.CheckToken(attendanceCodeAddViewModel._tenantName, attendanceCodeAddViewModel._token))
                {
                    attendanceCodedelete = this.attendanceCodeRepository.DeleteAttendanceCode(attendanceCodeAddViewModel);
                }
                else
                {
                    attendanceCodedelete._failure = true;
                    attendanceCodedelete._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                attendanceCodedelete._failure = true;
                attendanceCodedelete._message = es.Message;
            }

            return attendanceCodedelete;
        }
    }
}
