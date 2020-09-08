using opensis.data.Models;
using opensis.data.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opensis.data.Interface
{
    public interface ISchoolRepository
    {
        public List<Schools> GetAllSchools();
        public List<Schools> AddSchools(Schools school);

        Task<SchoolAddViewMopdel> AddSchool(SchoolAddViewMopdel school);
        Task<SchoolAddViewMopdel> UpdateSchool(SchoolAddViewMopdel school);
        Task<SchoolAddViewMopdel> ViewSchool(SchoolAddViewMopdel school);
        Task<SchoolAddViewMopdel> EditSchool(SchoolAddViewMopdel school);
        //Task<SchoolLogoUpdateModel> updateSchoolLogo(Guid guid, SchoolLogoUpdateModel schoolLogoUpdateModel);
    }
}
