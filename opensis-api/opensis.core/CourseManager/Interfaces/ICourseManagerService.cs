using opensis.data.ViewModels.CourseManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.CourseManager.Interfaces
{
    public interface ICourseManagerService
    {
        public ProgramAddViewModel AddProgram(ProgramAddViewModel programAddViewModel);
        public ProgramListViewModel GetAllProgram(ProgramListViewModel programListViewModel);
        public ProgramAddViewModel UpdateProgram(ProgramAddViewModel programAddViewModel);
        public ProgramAddViewModel DeleteProgram(ProgramAddViewModel programAddViewModel);
        public SubjectAddViewModel AddSubject(SubjectAddViewModel subjectAddViewModel);
        public SubjectAddViewModel UpdateSubject(SubjectAddViewModel subjectAddViewModel);
        public SubjectListViewModel GetAllSubjectList(SubjectListViewModel subjectListViewModel);
        public SubjectAddViewModel DeleteSubject(SubjectAddViewModel subjectAddViewModel);
    }
}

