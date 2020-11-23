using opensis.data.Models;
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
        public StudentListModel GetAllStudentList(PageResult pageResult);
        public SearchContactViewModel SearchContactForStudent(SearchContactViewModel searchContactViewModel);
        public LoginInfoAddModel AddStudentLoginInfo(LoginInfoAddModel login);

        public StudentDocumentAddViewModel AddStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel);
        public StudentDocumentAddViewModel UpdateStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel);
        public StudentDocumentListViewModel GetAllStudentDocumentsList(StudentDocumentListViewModel studentDocumentListViewModel);
        public StudentDocumentAddViewModel DeleteStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel);

        //public StudentAddViewModel ViewStudent(StudentAddViewModel student);
        //public StudentAddViewModel DeleteStudent(StudentAddViewModel student);
    }
}
