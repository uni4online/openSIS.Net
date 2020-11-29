using opensis.data.Models;
using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.ParentInfos
{
    public class ParentInfoAddViewModel : CommonFields
    {
        public ParentInfoAddViewModel()
        {
            getStudentForView = new List<GetStudentForView>();
        }
        public ParentInfo parentInfo { get; set; }
        public string PasswordHash { get; set; }
        public List<GetStudentForView> getStudentForView { get; set; }
    }
}
