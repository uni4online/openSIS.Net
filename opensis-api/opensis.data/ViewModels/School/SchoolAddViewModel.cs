using Microsoft.AspNetCore.Http;
using opensis.data.Models;

namespace opensis.data.ViewModels.School
{
    public class SchoolAddViewModel : CommonFields
    {
        public SchoolMaster schoolMaster { get; set; }
    }
}
