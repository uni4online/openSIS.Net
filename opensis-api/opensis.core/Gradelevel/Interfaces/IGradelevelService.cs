using opensis.data.ViewModels.GradeLevel;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.GradeLevel.Interfaces
{
    public interface IGradelevelService
    {
        public GradelevelViewModel AddGradelevel(GradelevelViewModel gradelevel);
        public GradelevelViewModel ViewGradelevel(GradelevelViewModel gradelevel);
        public GradelevelViewModel UpdateGradelevel(GradelevelViewModel gradelevel);
        public GradelevelViewModel DeleteGradelevel(GradelevelViewModel gradelevel);
        public GradelevelListViewModel GetAllGradeLevels(GradelevelListViewModel gradelevel);
        public GradeEquivalencyListViewModel GetAllGradeEquivalency(GradeEquivalencyListViewModel gradeEquivalencyListModel);
    }
}
