using Microsoft.AspNetCore.Http;
using opensis.data.Models;

namespace opensis.data.ViewModels.School
{
    public class SchoolAddViewModel : CommonFields
    {
        public TableSchoolDetail tblSchoolDetail { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }
}
