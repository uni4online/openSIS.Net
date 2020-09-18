using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public CommonRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }
        public CityListModel GetAllCitiesByState(CityListModel city)
        {
            CityListModel cityListModel = new CityListModel();
            try
            {
                cityListModel.TableCity = null;
                var CityList = this.context?.TableCity.Where(x => x.StateId == city.StateId).ToList();
                if (CityList.Count > 0)
                {
                    cityListModel.TableCity = CityList;
                }
                cityListModel._tenantName = city._tenantName;
                cityListModel._token = city._token;
                cityListModel._failure = false;
            }
            catch (Exception es)
            {
                cityListModel._message = es.Message;
                cityListModel._failure = true;
                cityListModel._tenantName = city._tenantName;
                cityListModel._token = city._token;
            }
            return cityListModel;

        }
        public StateListModel GetAllStatesByCountry(StateListModel state)
        {
            StateListModel stateListModel = new StateListModel();
            try
            {
                stateListModel.TableState = null;
                var StateList = this.context?.TableState.Where(x => x.CountryId == state.CountryId).ToList();
                if (StateList.Count > 0)
                {
                    stateListModel.TableState = StateList;
                }
                stateListModel._tenantName = state._tenantName;
                stateListModel._token = state._token;
                stateListModel._failure = false;
            }
            catch (Exception es)
            {
                stateListModel._message = es.Message;
                stateListModel._failure = true;
                stateListModel._tenantName = state._tenantName;
                stateListModel._token = state._token;
            }
            return stateListModel;

        }
        public CountryListModel GetAllCountries(CountryListModel country)
        {
            CountryListModel countryListModel = new CountryListModel();
            try
            {
                countryListModel.TableCountry = null;
                var CountryList = this.context?.TableCountry.ToList();
                if (CountryList.Count > 0)
                {
                    countryListModel.TableCountry = CountryList;
                }
                countryListModel._tenantName = country._tenantName;
                countryListModel._token = country._token;
                countryListModel._failure = false;
            }
            catch (Exception es)
            {
                countryListModel._message = es.Message;
                countryListModel._failure = true;
                countryListModel._tenantName = country._tenantName;
                countryListModel._token = country._token;
            }
            return countryListModel;

        }

        public LanguageListModel GetAllLanguage(LanguageListModel language)
        {
            LanguageListModel languageListModel = new LanguageListModel();
            try
            {
                languageListModel.TableLanguage = null;
                var languages = this.context?.TableLanguage.ToList();
                if (languages.Count > 0)
                {
                    languageListModel.TableLanguage = languages;
                }
                languageListModel._tenantName = language._tenantName;
                languageListModel._token = language._token;
                languageListModel._failure = false;
            }
            catch (Exception es)
            {
                languageListModel._message = es.Message;
                languageListModel._failure = true;
                languageListModel._tenantName = language._tenantName;
                languageListModel._token = language._token;
            }
            return languageListModel;

        }
    }
}
