using opensis.core.helper;
using opensis.core.Student.Interfaces;
using opensis.data.Interface;
using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Student.Services
{
    public class StudentService: IStudentService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public IStudentRepository studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        //Required for Unit Testing
        public StudentService() { }

        /// <summary>
        /// Student Add
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel SaveStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentAddViewModel = new StudentAddViewModel();
            if (TokenManager.CheckToken(student._tenantName, student._token))
            {
                studentAddViewModel = this.studentRepository.AddStudent(student);
            }
            else
            {
                studentAddViewModel._failure = true;
                studentAddViewModel._message = TOKENINVALID;
            }
            return studentAddViewModel;
        }

        /// <summary>
        /// Student Update
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel UpdateStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentUpdate = new StudentAddViewModel();
            if (TokenManager.CheckToken(student._tenantName, student._token))
            {

                studentUpdate = this.studentRepository.UpdateStudent(student);
            }
            else
            {
                studentUpdate._failure = true;
                studentUpdate._message = TOKENINVALID;                
            }
            return studentUpdate;
        }
        /// <summary>
        /// Student View By Id
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel ViewStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentView = new StudentAddViewModel();
            if (TokenManager.CheckToken(student._tenantName, student._token))
            {

                studentView = this.studentRepository.ViewStudent(student);
            }
            else
            {
                studentView._failure = true;
                studentView._message = TOKENINVALID;     
            }
            return studentView;
        }

        public StudentAddViewModel DeleteStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentDelete = new StudentAddViewModel();
            try
            {
                if (TokenManager.CheckToken(student._tenantName, student._token))
                {
                    studentDelete = this.studentRepository.DeleteStudent(student);
                }
                else
                {
                    studentDelete._failure = true;
                    studentDelete._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                studentDelete._failure = true;
                studentDelete._message = es.Message;
            }

            return studentDelete;
        }


    }
}
