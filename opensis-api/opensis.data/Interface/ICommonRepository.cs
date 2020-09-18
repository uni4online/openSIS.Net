using opensis.data.ViewModels.CommonModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface ICommonRepository
    {
        public CountryListModel GetAllCountries(CountryListModel country);

        public StateListModel GetAllStatesByCountry(StateListModel state);
        public CityListModel GetAllCitiesByState(CityListModel city);
        public LanguageListModel GetAllLanguage(LanguageListModel language);
    }
}
