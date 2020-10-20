using opensis.data.ViewModels.StudentEnrollmentCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface IStudentEnrollmentCodeRepository
    {
        public StudentEnrollmentCodeAddViewModel AddStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel);
        public StudentEnrollmentCodeAddViewModel ViewStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel);
        public StudentEnrollmentCodeAddViewModel DeleteStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel);
        public StudentEnrollmentCodeAddViewModel UpdateStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel);
        public StudentEnrollmentCodeListViewModel GetAllStudentEnrollmentCode(StudentEnrollmentCodeListViewModel studentEnrollmentCodeListView);
    }

}
