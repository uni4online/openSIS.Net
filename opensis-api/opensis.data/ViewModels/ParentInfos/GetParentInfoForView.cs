using opensis.data.Models;
using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.ParentInfos
{
     public class GetParentInfoForView
    {
        public GetParentInfoForView()
        {
            getStudentForView = new List<GetStudentForView>();
        }

        public string Associationship { get; set; }
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string AddressLineOne { get; set; }
        public List<GetStudentForView> getStudentForView { get; set; }
        

        //public string Country { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Zip { get; set; }
    }
}
