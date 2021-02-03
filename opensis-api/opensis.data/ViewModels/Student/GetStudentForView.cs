using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class GetStudentForView:CommonFields
    {
        //public List<StudentMaster> studentMaster { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? StudentId { get; set; }
        public string StudentInternalId { get; set; }
        public string AdmissionNumber { get; set; }
        public string RollNumber { get; set; }
        public string FirstGivenName { get; set; }
        public string MiddleName { get; set; }
        public string LastFamilyName { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string PreferredName { get; set; }
        public string PreviousName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string SchoolName { get; set; }
        public string GradeLevelTitle { get; set; }
        public bool? IsCustodian { get; set; }
        public string Relationship { get; set; }
        public byte[] StudentPhoto { get; set; }

    }
}
