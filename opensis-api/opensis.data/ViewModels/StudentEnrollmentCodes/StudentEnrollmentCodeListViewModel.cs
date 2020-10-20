using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.StudentEnrollmentCodes
{
   public class StudentEnrollmentCodeListViewModel:CommonFields
    {
        public List<StudentEnrollmentCode> studentEnrollmentCodeList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
    }
}
