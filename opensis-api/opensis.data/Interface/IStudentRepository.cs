using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface IStudentRepository
    {
        public StudentAddViewModel AddStudent(StudentAddViewModel student);
        public StudentAddViewModel UpdateStudent(StudentAddViewModel student);
        public StudentAddViewModel ViewStudent(StudentAddViewModel student);
        public StudentAddViewModel DeleteStudent(StudentAddViewModel student);
    }
}
