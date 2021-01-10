using System;
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
        public IActionResult InserEnrollmentCode()
        {
            //var schoolList = this.context?.SchoolMaster.ToList();
            //foreach (var school in schoolList)
            //{
            //    var enrollmentCode = new List<StudentEnrollmentCode>()
            //    {
            //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=1, Title="New", ShortName="NEW", Type="Add", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy },
            //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=2, Title="Dropped Out", ShortName="DROP", Type="Drop", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy },
            //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=3, Title="Rolled Over", ShortName="ROLL", Type="Rolled Over", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy },
            //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=4, Title="Transferred In", ShortName="TRAN", Type="Enroll (Transfer)", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy },
            //         new StudentEnrollmentCode(){TenantId=school.TenantId, SchoolId=school.SchoolId, EnrollmentCode=5, Title="Transferred Out", ShortName="TRAN", Type="Drop (Transfer)", LastUpdated=DateTime.UtcNow, UpdatedBy=school.CreatedBy }
            //    };

            //    this.context?.StudentEnrollmentCode.AddRange(enrollmentCode);
            //    this.context?.SaveChanges();
            //}
            return Ok();
        }
    }
}
