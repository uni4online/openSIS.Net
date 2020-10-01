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


    }
}
