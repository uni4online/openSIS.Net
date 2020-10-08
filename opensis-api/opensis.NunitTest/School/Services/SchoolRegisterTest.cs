using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using opensis.core.School.Interfaces;
using opensis.core.School.Services;
using opensis.data.Models;

namespace opensis.NunitTest.School.Services
{
    [TestFixture]
    public class Test_SchoolRegister
    {
       [Test]
        public void Test_IsMandatoryFieldsArePresent_Valid()
        {
            SchoolRegister srg = new SchoolRegister();
            //Schools schools = new Schools();
            //schools.tenant_id = "TenantA";
            //schools.school_name = "Test School";
            //Assert.AreEqual(true, srg.IsMandatoryFieldsArePresent(schools));
        }

        [TestCase("TenantA","TestSchool", true)]
        [TestCase("", "TestSchool", false)]
        [TestCase("TenantA", "", false)]

        public void Test_IsMandatoryFieldsArePresent_Multiple(string tenant, string schoolname, bool expectedresult)
        {
            SchoolRegister srg = new SchoolRegister();
            //Schools schools = new Schools();
            //schools.tenant_id =tenant;
            //schools.school_name = schoolname;
            //Assert.AreEqual(expectedresult, srg.IsMandatoryFieldsArePresent(schools));
        }
        [Test]
        public void Test_GetAllSchoolList()
        {
            SchoolRegister srg = new SchoolRegister();
            PageResult pr = new PageResult();
            pr.PageSize = 10;
            pr.PageNumber = 0;
            pr.TenantId =Guid.Parse("1e93c7bf-0fae-42bb-9e09-a1cedc8c0355");
            pr._tenantName = "opensisv2";
            pr._token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im9wZW5zaXN2MiIsIm5iZiI6MTYwMTgxNDIwOCwiZXhwIjoxNjAxODE2MDA4LCJpYXQiOjE2MDE4MTQyMDh9.ZHsm4jMC6S2yQvC9JejYhJCOOCQvwkxfed-mQMH9GAI";
            var data = srg.GetAllSchoolList(pr);
            Assert.AreEqual(true, data._failure);
           // Assert.AreEqual(true, srg.GetAllSchoolList(pr));
        }

    }
}
