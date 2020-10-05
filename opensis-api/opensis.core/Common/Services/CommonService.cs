using opensis.core.Common.Interfaces;
using opensis.core.helper;
using opensis.data.Interface;
using opensis.data.ViewModels.CommonModel;
using opensis.data.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Common.Services
{
    public class CommonService : ICommonService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public ICommonRepository commonRepository;
        public CommonService(ICommonRepository commonRepository)
        {
            this.commonRepository = commonRepository;
        }
        public CountryListModel GetAllCountries(CountryListModel country)
        {
            CountryListModel countryListModel = new CountryListModel();
            if (TokenManager.CheckToken(country._tenantName, country._token))
            {
                countryListModel = this.commonRepository.GetAllCountries(country);
                return countryListModel;
            }
            else
            {
                countryListModel._failure = true;
                countryListModel._message = TOKENINVALID;
                return countryListModel;
            }

        }

        public StateListModel GetAllStatesByCountry(StateListModel state)
        {
            StateListModel stateListModel = new StateListModel();
            if (TokenManager.CheckToken(state._tenantName, state._token))
            {
                stateListModel = this.commonRepository.GetAllStatesByCountry(state);
                return stateListModel;
            }
            else
            {
                stateListModel._failure = true;
                stateListModel._message = TOKENINVALID;
                return stateListModel;
            }

        }

        public CityListModel GetAllCitiesByState(CityListModel city)
        {
            CityListModel cityListModel = new CityListModel();
            if (TokenManager.CheckToken(city._tenantName, city._token))
            {
                cityListModel = this.commonRepository.GetAllCitiesByState(city);
                return cityListModel;
            }
            else
            {
                cityListModel._failure = true;
                cityListModel._message = TOKENINVALID;
                return cityListModel;
            }

        }

        public LanguageListModel GetAllLanguage(LanguageListModel language)
        {
            LanguageListModel languageListModel = new LanguageListModel();
            try
            {
                languageListModel = this.commonRepository.GetAllLanguage(language);
                return languageListModel;
            }
            catch (Exception ex)
            {

                languageListModel._failure = true;
                languageListModel._message = null;
                return languageListModel;
            }

        }
    }
}
