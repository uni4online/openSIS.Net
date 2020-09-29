using Microsoft.AspNetCore.Http;
using opensis.data.Models;

namespace opensis.data.ViewModels.School
{
    public class SchoolAddViewModel : CommonFields
    {
        public TableSchoolMaster tblSchoolMaster { get; set; }
    }
}
