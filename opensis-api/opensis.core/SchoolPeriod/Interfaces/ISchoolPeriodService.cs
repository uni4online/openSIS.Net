using opensis.data.ViewModels.SchoolPeriod;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.SchoolPeriod.Interfaces
{
    public interface ISchoolPeriodService
    {
        public SchoolPeriodAddViewModel SaveSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod);
        public SchoolPeriodAddViewModel UpdateSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod);
        public SchoolPeriodAddViewModel ViewSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod);
        public SchoolPeriodAddViewModel DeleteSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod);

    }
}
