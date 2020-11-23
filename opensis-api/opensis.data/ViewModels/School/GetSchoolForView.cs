using System;
using System.Collections.Generic;
using System.Text;


namespace opensis.data.ViewModels.School
{
    public class GetSchoolForView
    {
        public int? SchoolId { get; set; }
        public Guid? TenantId { get; set; }
        public string SchoolName { get; set; }
        public DateTime? DateSchoolOpened { get; set; }
        public DateTime? DateSchoolClosed { get; set; }
        public string StreetAddress1 { get; set; }
        public string NameOfPrincipal { get; set; }
        public string Telephone { get; set; }
        public bool? Status { get; set; }        
    }
}

