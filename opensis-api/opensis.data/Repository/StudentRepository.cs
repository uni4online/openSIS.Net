using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public StudentRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        /// <summary>
        /// Add Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel AddStudent(StudentAddViewModel student)
        {
            try
            {
                int? MasterStudentId = Utility.GetMaxPK(this.context, new Func<StudentEnrollment, int>(x => x.StudentId));
                student.studentEnrollment.StudentId = (int)MasterStudentId;
                student.studentEnrollment.LastUpdated = DateTime.UtcNow;
                this.context?.StudentEnrollment.Add(student.studentEnrollment);
                this.context?.SaveChanges();
                student._failure = false;
            }
            catch (Exception es)
            {
                student._failure = true;
                student._message = es.Message;
            }

            return student;
        }

        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel UpdateStudent(StudentAddViewModel student)
        {
            try
            {
                var studentUpdate = this.context?.StudentEnrollment.FirstOrDefault(x => x.TenantId == student.studentEnrollment.TenantId && x.SchoolId == student.studentEnrollment.SchoolId && x.StudentId == student.studentEnrollment.StudentId);

                studentUpdate.TenantId = student.studentEnrollment.TenantId;
                studentUpdate.SchoolId = student.studentEnrollment.SchoolId;
                studentUpdate.StudentId = student.studentEnrollment.StudentId;
                studentUpdate.EnrollmentId = student.studentEnrollment.EnrollmentId;
                studentUpdate.GradeId = student.studentEnrollment.GradeId;
                studentUpdate.SectionId = student.studentEnrollment.SectionId;
                studentUpdate.StartDate = student.studentEnrollment.StartDate;
                studentUpdate.EndDate = student.studentEnrollment.EndDate;
                studentUpdate.EnrollmentCode = student.studentEnrollment.EnrollmentCode;
                studentUpdate.DropCode = student.studentEnrollment.DropCode;
                studentUpdate.NextSchool = student.studentEnrollment.NextSchool;
                studentUpdate.CalendarId = student.studentEnrollment.CalendarId;
                studentUpdate.LastSchool = student.studentEnrollment.LastSchool;
                studentUpdate.LastUpdated = DateTime.UtcNow;
                studentUpdate.UpdatedBy = student.studentEnrollment.UpdatedBy;

                this.context?.SaveChanges();
                student._failure = false;
               
            }
            catch (Exception ex)
            {
                student.studentEnrollment = null;
                student._failure = true;
                student._message = ex.Message;
                
            }
            return student;

        }
        /// <summary>
        /// View Student By Id
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>

        public StudentAddViewModel ViewStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentView = new StudentAddViewModel();
            try
            {
               
                var studentById = this.context?.StudentEnrollment.FirstOrDefault(x => x.TenantId == student.studentEnrollment.TenantId && x.SchoolId == student.studentEnrollment.SchoolId && x.StudentId == student.studentEnrollment.StudentId);
                if (studentById != null)
                {
                    studentView.studentEnrollment = studentById;                    
                }
                else
                {
                    studentView._failure = true;
                    studentView._message = NORECORDFOUND;           
                }
            }
            catch (Exception es)
            {
                studentView._failure = true;
                studentView._message=es.Message;
            }
            return studentView;
        }

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>

        public StudentAddViewModel DeleteStudent(StudentAddViewModel student)
        {
            try
            {
                var studentDel = this.context?.StudentEnrollment.FirstOrDefault(x => x.TenantId == student.studentEnrollment.TenantId && x.SchoolId == student.studentEnrollment.SchoolId && x.StudentId == student.studentEnrollment.StudentId);
                this.context?.StudentEnrollment.Remove(studentDel);
                this.context?.SaveChanges();
                student._failure = false;
                student._message = "Deleted";
            }
            catch (Exception es)
            {
                student._failure = true;
                student._message = es.Message;
            }
            return student;
        }
    }
        
}
