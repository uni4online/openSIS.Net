using opensis.data.Models;
using opensis.data.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opensis.core.School.Interfaces
{
    public interface ISchoolRegisterService
    {
        public SchoolListViewModel getAllSchools(SchoolViewModel objModel);

        public SchoolListViewModel SaveSchool(Schools school);
        public bool IsMandatoryFieldsArePresent(Schools schools);


        Task<SchoolAddViewMopdel> SaveSchool(SchoolAddViewMopdel schools);
        Task<SchoolAddViewMopdel> UpdateSchool(SchoolAddViewMopdel schools);

        Task<SchoolAddViewMopdel> ViewSchool(SchoolAddViewMopdel schools);

        Task<SchoolAddViewMopdel> EditSchool(SchoolAddViewMopdel schools);

        //Task<SchoolLogoUpdateModel> updateSchoolLogo(Guid guid, SchoolLogoUpdateModel schoolLogoUpdateModel);

        //public List<Schools> getAllSchools(opensisContext context);

        //public List<Schools> SaveSchool(Schools school, opensisContext context);
    }
}
