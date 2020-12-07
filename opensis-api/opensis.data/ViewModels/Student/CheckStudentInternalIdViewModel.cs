using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class CheckStudentInternalIdViewModel : CommonFields
    {
        public Guid TenantId { get; set; }
        public string StudentInternalId { get; set; }
        public bool IsValidInternalId { get; set; }
    }
}
