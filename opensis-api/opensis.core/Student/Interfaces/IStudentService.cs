using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Student.Interfaces
{
    public interface IStudentService
    {
        public StudentAddViewModel SaveStudent(StudentAddViewModel student);
        public StudentAddViewModel UpdateStudent(StudentAddViewModel student);
        public StudentAddViewModel ViewStudent(StudentAddViewModel student);
        public StudentAddViewModel DeleteStudent(StudentAddViewModel student);
    }
}
