using opensis.data.ViewModels.CommonModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Common.Interfaces
{
    public interface ICommonService
    {
        public CountryListModel GetAllCountries(CountryListModel countryListModel);
        public StateListModel GetAllStatesByCountry(StateListModel stateListModel);
        public CityListModel GetAllCitiesByState(CityListModel cityListModel);
        public LanguageListModel GetAllLanguage(LanguageListModel languageListModel);
    }
}
