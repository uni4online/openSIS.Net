using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.School
{
    public class GetSchoolForView
    {
        public int? School_Id { get; set; }
        public Guid? Tenant_Id { get; set; }
        public string School_Name { get; set; }
        public string School_Address { get; set; }
        public string Principle { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }
    }
}
