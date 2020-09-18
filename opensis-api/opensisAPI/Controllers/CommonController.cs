using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.Common.Interfaces;
using opensis.data.ViewModels.CommonModel;
using opensis.data.ViewModels.School;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Common")]
    [ApiController]
    public class CommonController : ControllerBase
    {

        private ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }
        [HttpPost("getAllCountries")]

        public ActionResult<CountryListModel> GetAllCountries(CountryListModel country)
        {
            CountryListModel countryListModel = new CountryListModel();
            try
            {
                countryListModel = _commonService.GetAllCountries(country);
            }
            catch (Exception es)
            {
                countryListModel._failure = true;
                countryListModel._message = es.Message;
            }
            return countryListModel;
        }

        [HttpPost("getAllStatesByCountry")]

        public ActionResult<StateListModel> GetAllStatesByCountry(StateListModel state)
        {
            StateListModel stateListModel = new StateListModel();
            try
            {
                stateListModel = _commonService.GetAllStatesByCountry(state);
            }
            catch (Exception es)
            {
                stateListModel._failure = true;
                stateListModel._message = es.Message;
            }
            return stateListModel;
        }

        [HttpPost("getAllCitiesByState")]

        public ActionResult<CityListModel> GetAllCitiesByState(CityListModel city)
        {
            CityListModel cityListModel = new CityListModel();
            try
            {
                cityListModel = _commonService.GetAllCitiesByState(city);
            }
            catch (Exception es)
            {
                cityListModel._failure = true;
                cityListModel._message = es.Message;
            }
            return cityListModel;
        }

        [HttpPost("getAllLanguage")]

        public ActionResult<LanguageListModel> GetAllLanguage(LanguageListModel language)
        {
            LanguageListModel languageListModel = new LanguageListModel();
            try
            {
                languageListModel = _commonService.GetAllLanguage(language);
            }
            catch (Exception es)
            {
                languageListModel._failure = true;
                languageListModel._message = es.Message;
            }
            return languageListModel;
        }
    }
}
