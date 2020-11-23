using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class LoginInfoAddModel : CommonFields
    {
        public UserMaster userMaster { get; set; }
        public int StudentId { get; set; }
    }
}
