﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;

namespace opensisAPI.Controllers
{
    [ApiController]
    
    [Route("{tenant}/ WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private CRMContext context;
        public WeatherForecastController(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public IActionResult Get()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            
            
            //var schoolList = this.context?.SchoolMaster.ToList();
            //foreach (var school in schoolList)
            //{                
            //    var Dp = this.context?.DpdownValuelist.OrderBy(x => x.Id).LastOrDefault()?.Id;
            //    if (Dp == null)
            //    {
            //        Dp = 0;
            //    }
            //    var DpdownValuelist = new List<DpdownValuelist>()
            //    {
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "PK", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+1},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "K", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+2},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "1", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+3},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "2", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+4},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "3", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+5},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "4", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+6},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "5", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+7},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "6", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+8},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "7", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+9},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "8", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+10},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "9", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+11},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "10", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+12},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "11", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+13},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "12", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+14},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "13", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+15},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "14", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+16},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "15", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+17},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "16", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+18},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "17", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+19},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "18", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+20},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "19", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+21},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Grade Level", LovColumnValue = "20", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+22},                    


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "School Gender", LovColumnValue = "Boys", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+23},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "School Gender", LovColumnValue = "Girls", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+24},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "School Gender", LovColumnValue = "Mixed", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+25},


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Mr.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+26},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Miss.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+27},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Mrs.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+28},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Ms.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+29},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Dr.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+30},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Rev.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+31},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Prof.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+32},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Sir.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+33},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Salutation", LovColumnValue = "Lord ", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+34},


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Suffix", LovColumnValue = "Jr.", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+35},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Suffix", LovColumnValue = "Sr", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+36},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Suffix", LovColumnValue = "Sr", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+37},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Suffix", LovColumnValue = "II", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+38},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Suffix", LovColumnValue = "III", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+39},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Suffix", LovColumnValue = "IV", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+40},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Suffix", LovColumnValue = "V", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+41},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Suffix", LovColumnValue = "PhD", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+42},


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Gender", LovColumnValue = "Male", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+43},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Gender", LovColumnValue = "Female", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+44},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Gender", LovColumnValue = "Other", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+45},


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Marital Status", LovColumnValue = "Single", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+46},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Marital Status", LovColumnValue = "Married", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+47},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Marital Status", LovColumnValue = "Partnered", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+48},


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Rolling/Retention Option", LovColumnValue = "Next grade at current school", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+49},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Rolling/Retention Option", LovColumnValue = "Retain", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+50},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Rolling/Retention Option", LovColumnValue = "Do not enroll after this school year", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+51},


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Relationship", LovColumnValue = "Mother", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+52},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Relationship", LovColumnValue = "Father", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+53},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Relationship", LovColumnValue = "Legal Guardian", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+54},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Relationship", LovColumnValue = "Other", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+55},


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Enrollment Type", LovColumnValue = "Add", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+56},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Enrollment Type", LovColumnValue = "Drop", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+57},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Enrollment Type", LovColumnValue = "Rolled Over", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+58},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Enrollment Type", LovColumnValue = "Drop (Transfer)", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+59},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Enrollment Type", LovColumnValue = "Enroll (Transfer)", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+60},


            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Field Type", LovColumnValue = "Dropdown", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+61},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Field Type", LovColumnValue = "Editable Dropdown", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+62},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Field Type", LovColumnValue = "Text", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+63},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Field Type", LovColumnValue = "Checkbox", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+64},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Field Type", LovColumnValue = "Number", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+65},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Field Type", LovColumnValue = "Multiple SelectBox", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+66},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Field Type", LovColumnValue = "Date", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+67},
            //        new DpdownValuelist() { UpdatedOn = DateTime.UtcNow, UpdatedBy = school.ModifiedBy, TenantId = school.TenantId, SchoolId = school.SchoolId, LovName = "Field Type", LovColumnValue = "Textarea", CreatedBy = school.CreatedBy, CreatedOn = DateTime.UtcNow,Id=(long)Dp+68},
            //    };
            //    this.context?.DpdownValuelist.AddRange(DpdownValuelist);
            //    this.context?.SaveChanges();
            //}            
            return Ok();
        }

        [HttpPost]
        public IActionResult InsertEnrollmentCode()
        {
        //    //var schoolList = this.context?.SchoolMaster.ToList();
        //    //foreach (var school in schoolList)
        //    //{
        //    //    var enrollmentCode = new List<StudentEnrollmentCode>()
        //    //    {
        //    //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=1, Title="New", ShortName="NEW", Type="Add", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy },
        //    //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=2, Title="Dropped Out", ShortName="DROP", Type="Drop", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy },
        //    //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=3, Title="Rolled Over", ShortName="ROLL", Type="Rolled Over", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy },
        //    //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=4, Title="Transferred In", ShortName="TRAN", Type="Enroll (Transfer)", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy },
        //    //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=5, Title="Transferred Out", ShortName="TRAN", Type="Drop (Transfer)", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy }
        //    //    };

        //    //    this.context?.StudentEnrollmentCode.AddRange(enrollmentCode);
        //    //    this.context?.SaveChanges();
        //    //}
            return Ok();
        }

        [HttpPost("insertSchool")]
        public IActionResult InsertSchool()
        {
            Guid tenantId = new Guid("1e93c7bf-0fae-42bb-9e09-a1cedc8c0355");


            for (int i = 1; i < 3; i++)
            {
                int? schoolId = Utility.GetMaxPK(this.context, new Func<SchoolMaster, int>(x => x.SchoolId));
                int? schoolDetailId = Utility.GetMaxPK(this.context, new Func<SchoolDetail, int>(x => x.Id));
                long? dpdownValueId = Utility.GetMaxLongPK(this.context, new Func<DpdownValuelist, long>(x => x.Id));
                int? gradeId = Utility.GetMaxPK(this.context, new Func<Gradelevels, int>(x => x.GradeId));
                Guid GuidId = Guid.NewGuid();

                var school = new List<SchoolMaster>()
                { new SchoolMaster() {TenantId=tenantId,SchoolId=(int)schoolId,SchoolInternalId="SC-00"+i,SchoolGuid=GuidId,SchoolName="Test School"+i,SchoolAltId="SAC-OO"+i,SchoolStateId="California",SchoolDistrictId="Sacramento",SchoolLevel="1",AlternateName="TS"+i,StreetAddress1="XYZ",StreetAddress2="ABC",City="Compton",Country="USA",County="Los Angeles County",SchoolClassification="Primary",Division="1ST",State="California",District="Sacramento",Zip="90224",CurrentPeriodEnds=Convert.ToDateTime("2021-01-15"),MaxApiChecks=1,Features="Feature 1",CreatedBy="Sayan Das",DateCreated=DateTime.UtcNow,ModifiedBy="Sayan Das",DateModifed=DateTime.UtcNow,Longitude=00.00,Latitude=11.00,Membership=new List<Membership>()
                    {
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Super Administrator",Title= "Super Administrator",MembershipId= 1},
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Administrator",Title= "Administrator",MembershipId= 2},
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Teacher",Title= "Teacher",MembershipId= 3 },
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Student",Title= "Student",MembershipId= 4},
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Parent",Title= "Parent",MembershipId= 5},
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Admin Assistant",Title= "Admin Assistant",MembershipId= 6},
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Administrator w/Custom",Title= "Administrator w/Custom",MembershipId= 7},
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Teacher w/Custom",Title= "Teacher w/Custom",MembershipId= 8},
                        new Membership(){LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,Profile= "Parent w/Custom",Title= "Parent w/Custom",MembershipId= 9}
                    },
                    SchoolDetail =new List<SchoolDetail>()
                    {
                         new SchoolDetail(){Id=(int)schoolDetailId, TenantId=tenantId, SchoolId=(int)schoolId, NameOfPrincipal="Principal",Affiliation="",Associations="",Locale="",LowestGradeLevel="PK",HighestGradeLevel="5",DateSchoolOpened=Convert.ToDateTime("2020-01-15"),DateSchoolClosed=Convert.ToDateTime("2020-11-15"),Status=true,Gender="Mixed",Internet=true,Electricity=true,Telephone="022-3456-67",Fax="675432",Website="www.school.com",Email="school@email.com",Twitter="www.Twitter.com",Facebook="www.facebook.com",Instagram="www.Instagram,com",Youtube="www.Youtube,com",LinkedIn="www.LinkedIn.com",NameOfAssistantPrincipal="Assistant Principal",RunningWater=true,MainSourceOfDrinkingWater="Water Source-1",CurrentlyAvailable=true,TotalFemaleToilets=10,TotalFemaleToiletsUsable=9,TotalMaleToilets=11,TotalMaleToiletsUsable=10,TotalCommonToilets=11,TotalCommonToiletsUsable=11,HandwashingAvailable=true,SoapAndWaterAvailable=true,HygeneEducation="HE"}
                    },
                    DpdownValuelist=new List<DpdownValuelist>() {
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="PK",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="K",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+1},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="1",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+2},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="2",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+3},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="3",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+4},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="4",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+5},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="5",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+6},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="6",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+7},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="7",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+8},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="8",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+9},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="9",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+10},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="10",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+11},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="11",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+12},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="12",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+13},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="13",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+14},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="14",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+15},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="15",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+16},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="16",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+17},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="17",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+18},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="18",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+19},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="19",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+20},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Grade Level",LovColumnValue="20",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+21},


                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="School Gender",LovColumnValue="Boys",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+22},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="School Gender",LovColumnValue="Girls",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+23},
                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="School Gender",LovColumnValue="Mixed",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+24},


                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Mr.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+25},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Miss.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+26},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Mrs.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+27},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Ms.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+28},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Dr.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+29},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Rev.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+30},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Prof.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+31},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Sir.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+32},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Salutation",LovColumnValue="Lord ",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+33},


                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Suffix",LovColumnValue="Jr.",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+34},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Suffix",LovColumnValue="Sr",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+35},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Suffix",LovColumnValue="Sr",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+36},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Suffix",LovColumnValue="II",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+37},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Suffix",LovColumnValue="III",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+38},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Suffix",LovColumnValue="IV",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+39},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Suffix",LovColumnValue="V",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+40},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Suffix",LovColumnValue="PhD",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+41},


                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Gender",LovColumnValue="Male",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+42},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Gender",LovColumnValue="Female",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+43},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Gender",LovColumnValue="Other",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+44},


                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Marital Status",LovColumnValue="Single",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+45},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Marital Status",LovColumnValue="Married",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+46},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Marital Status",LovColumnValue="Partnered",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+47},


                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Rolling/Retention Option",LovColumnValue="Next grade at current school",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+48},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Rolling/Retention Option",LovColumnValue="Retain",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+49},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Rolling/Retention Option",LovColumnValue="Do not enroll after this school year",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+50},


                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Relationship",LovColumnValue="Mother",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+51},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Relationship",LovColumnValue="Father",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+52},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Relationship",LovColumnValue="Legal Guardian",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+53},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Relationship",LovColumnValue="Other",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+54},


                    new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Enrollment Type",LovColumnValue="Add",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+55},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Enrollment Type",LovColumnValue="Drop",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+56},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Enrollment Type",LovColumnValue="Rolled Over",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+57},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Enrollment Type",LovColumnValue="Drop (Transfer)",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+58},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Enrollment Type",LovColumnValue="Enroll (Transfer)",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+59},


                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Field Type",LovColumnValue="Dropdown",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+60},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Field Type",LovColumnValue="Editable Dropdown",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+61},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Field Type",LovColumnValue="Text",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+62},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Field Type",LovColumnValue="Checkbox",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+63},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Field Type",LovColumnValue="Number",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+64},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Field Type",LovColumnValue="Multiple SelectBox",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+65},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Field Type",LovColumnValue="Date",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+66},
                     new DpdownValuelist(){UpdatedOn=DateTime.UtcNow,UpdatedBy="Sayan Das", TenantId= tenantId,SchoolId=(int)schoolId,LovName="Field Type",LovColumnValue="Textarea",CreatedBy="Sayan Das",CreatedOn=DateTime.UtcNow,Id=(long)dpdownValueId+67},
                    },

                    FieldsCategory=new List<FieldsCategory>()
                {
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="General Information",Module="School",SortOrder=1,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=1},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Wash Information",Module="School",SortOrder=2,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=2},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="General Info",Module="Student",SortOrder=1,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=3},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Enrollment Info",Module="Student",SortOrder=2,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=4},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Address & Contact",Module="Student",SortOrder=3,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=5},

                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Family Info",Module="Student",SortOrder=4,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=6},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Medical Info",Module="Student",SortOrder=5,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=7},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Comments",Module="Student",SortOrder=6,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=8},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Documents",Module="Student",SortOrder=7,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=9},

                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="General Info",Module="Parent",SortOrder=1,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=10},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Address Info",Module="Parent",SortOrder=2,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=11},

                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="General Info",Module="Staff",SortOrder=1,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=12},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="School Info",Module="Staff",SortOrder=2,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=13},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Address & Contact",Module="Staff",SortOrder=3,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=14},
                    new FieldsCategory(){ TenantId=tenantId,SchoolId=(int)schoolId,IsSystemCategory=true,Search=true, Title="Certification Info",Module="Staff",SortOrder=4,Required=true,Hide=false,LastUpdate=DateTime.UtcNow,UpdatedBy="Sayan Das",CategoryId=15}
                },
                   StudentEnrollmentCode= new List<StudentEnrollmentCode>()
                {
                     new StudentEnrollmentCode(){TenantId=tenantId, SchoolId=(int)schoolId, EnrollmentCode=1, Title="New", ShortName="NEW", SortOrder=1, Type="Add", LastUpdated=DateTime.UtcNow, UpdatedBy="Sayan Das" },
                     new StudentEnrollmentCode(){TenantId=tenantId, SchoolId=(int)schoolId, EnrollmentCode=2, Title="Dropped Out", ShortName="DROP", SortOrder=2, Type="Drop", LastUpdated=DateTime.UtcNow, UpdatedBy="Sayan Das" },
                     new StudentEnrollmentCode(){TenantId=tenantId, SchoolId=(int)schoolId, EnrollmentCode=3, Title="Rolled Over", ShortName="ROLL", SortOrder=3, Type="Rolled Over", LastUpdated=DateTime.UtcNow, UpdatedBy="Sayan Das" },
                     new StudentEnrollmentCode(){TenantId=tenantId, SchoolId=(int)schoolId, EnrollmentCode=4, Title="Transferred In", ShortName="TRAN", SortOrder=4, Type="Enroll (Transfer)", LastUpdated=DateTime.UtcNow, UpdatedBy="Sayan Das" },
                     new StudentEnrollmentCode(){TenantId=tenantId, SchoolId=(int)schoolId, EnrollmentCode=5, Title="Transferred Out", ShortName="TRAN", SortOrder=5, Type="Drop (Transfer)", LastUpdated=DateTime.UtcNow, UpdatedBy="Sayan Das" }
                },Block=new List<Block>()
                {
                     new Block(){TenantId=tenantId, SchoolId=(int)schoolId, BlockId=1, BlockTitle="All Day", BlockSortOrder=1, CreatedOn=DateTime.UtcNow, CreatedBy="Sayan Das" }
                }
            },
                }.ToList();
                this.context?.SchoolMaster.AddRange(school);

                var gradelevels = new List<Gradelevels>()
                {
                    new Gradelevels(){TenantId=tenantId,SchoolId=(int)schoolId,GradeId=(int)gradeId,ShortName="G-6",Title="Grade-6",SortOrder=1,LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das",IscedGradeLevel="ISCED 2"},
                    new Gradelevels(){TenantId=tenantId,SchoolId=(int)schoolId,GradeId=(int)gradeId+1,ShortName="G-11",Title="Grade-11",SortOrder=2,LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das",IscedGradeLevel="ISCED 3"},
                }.ToList();
                this.context?.Gradelevels.AddRange(gradelevels);
                this.context?.SaveChanges();

                var schoolCalender = new SchoolCalendars()
                {
                    TenantId = tenantId,
                    SchoolId = (int)schoolId,
                    CalenderId = 1,
                    Title = "Calender 1",
                    AcademicYear = 2020,
                    DefaultCalender = true,
                    LastUpdated = DateTime.UtcNow,
                    UpdatedBy = "Sayan Das"
                };
                this.context?.SchoolCalendars.Add(schoolCalender);
                this.context?.SaveChanges();

                var schoolYear = new List<SchoolYears>()
                {
                    new SchoolYears(){TenantId=tenantId,SchoolId=(int)schoolId,MarkingPeriodId=1,AcademicYear=2020,Title="Year2020",ShortName="SY-20",SortOrder=1,StartDate=Convert.ToDateTime("2020-01-18"),EndDate=Convert.ToDateTime("2020-12-29"),LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das"},
                }.ToList();
                this.context?.SchoolYears.AddRange(schoolYear);
                this.context?.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("insertStudent")]
        public IActionResult InsertStudent(int schoolId)
        {
            Guid tenantId = new Guid("1e93c7bf-0fae-42bb-9e09-a1cedc8c0355");

            for (int i = 1; i < 7; i++)
            {
                int? MasterStudentId = 1;

                var studentData = this.context?.StudentMaster.Where(x => x.SchoolId == schoolId && x.TenantId == tenantId).OrderByDescending(x => x.StudentId).FirstOrDefault();

                if (studentData != null)
                {
                    MasterStudentId = studentData.StudentId + 1;
                }

                Guid GuidId = Guid.NewGuid();
                var student = new List<StudentMaster>()
                {
                    new StudentMaster(){TenantId=tenantId,SchoolId=schoolId,StudentId=(int)MasterStudentId,AlternateId="SA"+i,DistrictId="Sacramento",StateId="california",AdmissionNumber="AD"+i,RollNumber="Roll"+i,Salutation="Mr.",FirstGivenName="Buster" ,MiddleName=null,LastFamilyName="Keaton",Suffix="Jr.",PreferredName="PF Name",PreviousName="PV Name",SocialSecurityNumber="1800",OtherGovtIssuedNumber="1000",Dob=Convert.ToDateTime("1994-10-06").Date,Gender="Male",MaritalStatus="Single",CountryOfBirth=1,Nationality=1,FirstLanguageId=1,SecondLanguageId=2,ThirdLanguageId=3,HomePhone="03222234765",MobilePhone="4537890325",PersonalEmail="admin@email.com",SchoolEmail="school@email.com",Twitter="www.twitter.com",Facebook="www.Facebook.com",Instagram="www.Instagram.com",Youtube="www.Youtube.com",Linkedin="www.Youtube.com",HomeAddressLineOne="abc",HomeAddressLineTwo="xyz",HomeAddressCity="Compton",HomeAddressState="Compton",HomeAddressZip="90224",BusNo="US-2038",SchoolBusPickUp=true,SchoolBusDropOff=true,MailingAddressSameToHome=true,MailingAddressLineOne="abc",MailingAddressLineTwo="xyz",MailingAddressCity="Compton",MailingAddressState="Compton",MailingAddressZip="90224",MailingAddressCountry="USA",HomeAddressCountry="USA",StudentPortalId="P"+i,AlertDescription="XYZ",CriticalAlert="ABC",Dentist="",DentistPhone="7643435366",InsuranceCompany="I-Company",InsuranceCompanyPhone="8753366477",MedicalFacility="MDF",MedicalFacilityPhone="875446655575",PolicyHolder="Arun Roy",PolicyNumber="12334324",PrimaryCarePhysician="S.B.Pastur",PrimaryCarePhysicianPhone="7655476384",Vision="A.K.Daniels",VisionPhone="8975645654",EconomicDisadvantage=false,Eligibility504=true,EstimatedGradDate=Convert.ToDateTime("2020-10-02").Date,FreeLunchEligibility=true,LepIndicator=true,SpecialEducationIndicator=true,StudentInternalId="ST-00"+i,LastUpdated=DateTime.UtcNow,UpdatedBy="Sayan Das",EnrollmentType="Internal",IsActive=true,StudentGuid=GuidId }
                }.ToList();
                this.context?.StudentMaster.AddRange(student);
                this.context?.SaveChanges();

                int? calenderId = null;
                string enrollmentCode = null;
                var schoolName = this.context?.SchoolMaster.Where(x => x.TenantId == tenantId && x.SchoolId == schoolId).Select(s => s.SchoolName).FirstOrDefault();

                var defaultCalender = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == tenantId && x.SchoolId == schoolId && x.AcademicYear.ToString() == "2020" && x.DefaultCalender == true);

                if (defaultCalender != null)
                {
                    calenderId = defaultCalender.CalenderId;
                }

                var enrollmentType = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == tenantId && x.SchoolId == schoolId && x.Type.ToLower() == "New".ToLower());

                if (enrollmentType != null)
                {
                    enrollmentCode = enrollmentType.Title;
                }

                var gradeLevel = this.context?.Gradelevels.Where(x => x.SchoolId == schoolId).OrderBy(x => x.GradeId).FirstOrDefault();
                var StudentEnrollmentData = new StudentEnrollment() { TenantId = tenantId, SchoolId = schoolId, StudentId = (int)MasterStudentId, EnrollmentId = 1, SchoolName = schoolName, RollingOption = "Next grade at current school", EnrollmentCode = enrollmentCode, CalenderId = calenderId, GradeLevelTitle = (gradeLevel != null) ? gradeLevel.Title : null, EnrollmentDate = DateTime.UtcNow, StudentGuid = GuidId,IsActive=true };

                this.context?.StudentEnrollment.Add(StudentEnrollmentData);
            }
            this.context?.SaveChanges();
            return Ok();
        }
    }
}
