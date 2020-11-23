using opensis.data.Models;
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
        public LoginInfoAddModel AddStudentLoginInfo(LoginInfoAddModel login);
        public StudentListModel GetAllStudentList(PageResult pageResult);

        public SearchContactViewModel SearchContactForStudent(SearchContactViewModel searchContactViewModel);

        public StudentDocumentAddViewModel SaveStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel);
        public StudentDocumentAddViewModel UpdateStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel);
        public StudentDocumentListViewModel GetAllStudentDocumentsList(StudentDocumentListViewModel studentDocumentListViewModel);
        public StudentDocumentAddViewModel DeleteStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel);

        //public StudentAddViewModel ViewStudent(StudentAddViewModel student);
        //public StudentAddViewModel DeleteStudent(StudentAddViewModel student);
    }
}
