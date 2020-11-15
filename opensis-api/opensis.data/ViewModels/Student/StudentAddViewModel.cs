using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class StudentAddViewModel : CommonFields
    {
        public StudentMaster studentMaster { get; set; }
        public string PasswordHash {get; set;}
    }
}
