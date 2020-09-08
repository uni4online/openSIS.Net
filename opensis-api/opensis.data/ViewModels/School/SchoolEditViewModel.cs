using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.School
{
    public class SchoolEditViewModel : CommonFields
    {
        public Guid Tenant_Id { get; set; }
        public int School_Id { get; set; }
    }
}
