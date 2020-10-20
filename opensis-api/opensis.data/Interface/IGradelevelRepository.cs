using opensis.data.ViewModels.GradeLevel;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface IGradelevelRepository
    {
        public GradelevelViewModel AddGradelevel(GradelevelViewModel gradelevel);
        public GradelevelViewModel ViewGradelevel(GradelevelViewModel gradelevel);
        public GradelevelViewModel UpdateGradelevel(GradelevelViewModel gradelevel);
        public GradelevelViewModel DeleteGradelevel(GradelevelViewModel gradelevel);
        public GradelevelListViewModel GetAllGradeLevels(GradelevelListViewModel gradelevelList);
        public GradeEquivalencyListViewModel GetAllGradeEquivalency(GradeEquivalencyListViewModel gradeEquivalencyList);
    }
}
