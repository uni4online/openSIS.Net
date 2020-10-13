using opensis.data.ViewModels.SchoolPeriod;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface ISchoolPeriodRepository
    {
        public SchoolPeriodAddViewModel AddSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod);
        public SchoolPeriodAddViewModel UpdateSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod);
        public SchoolPeriodAddViewModel ViewSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod);
        public SchoolPeriodAddViewModel DeleteSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod);

    }
}
