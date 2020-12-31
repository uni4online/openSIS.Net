using opensis.data.Helper;
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
                var CityList = this.context?.City.Where(x => x.StateId == city.StateId).ToList();
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
                var StateList = this.context?.State.Where(x => x.CountryId == state.CountryId).ToList();
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
                var CountryList = this.context?.Country.ToList();
                if (CountryList.Count > 0)
                {
                    int? StateCount = this.context?.State.Count();
                    countryListModel.TableCountry = CountryList;
                    countryListModel.StateCount = StateCount;
                    countryListModel._failure = false;
                }
                else
                {
                    countryListModel._failure = true;
                    countryListModel._message = NORECORDFOUND;
                }
                countryListModel._tenantName = country._tenantName;
                countryListModel._token = country._token;

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

        /// <summary>
        /// Add Language 
        /// </summary>
        /// <param name="languageAdd"></param>
        /// <returns></returns>
        public LanguageAddModel AddLanguage(LanguageAddModel languageAdd)
        {
            var getLanguage = this.context?.Language.FirstOrDefault(x => x.Locale.ToLower() == languageAdd.Language.Locale.ToLower());

            if (getLanguage != null)
            {
                languageAdd._message = "This Language Name Already Exist";
                languageAdd._failure = true;
            }
            else
            {
                int? languageId = Utility.GetMaxPK(this.context, new Func<Language, int>(x => x.LangId));
                languageAdd.Language.LangId = (int)languageId;
                languageAdd.Language.CreatedOn = DateTime.UtcNow;
                this.context?.Language.Add(languageAdd.Language);
                this.context?.SaveChanges();

            }
            return languageAdd;
        }

        /// <summary>
        /// View Language 
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public LanguageAddModel ViewLanguage(LanguageAddModel language)
        {
            try
            {
                LanguageAddModel languageAddModel = new LanguageAddModel();
                var getLanguageValue = this.context?.Language.FirstOrDefault(x => x.LangId == language.Language.LangId);
                if (getLanguageValue != null)
                {
                    languageAddModel.Language = getLanguageValue;
                    languageAddModel._failure = false;
                    return languageAddModel;
                }
                else
                {
                    languageAddModel._failure = true;
                    languageAddModel._message = NORECORDFOUND;
                    return languageAddModel;
                }
            }
            catch (Exception es)
            {

                throw;
            }
        }


        /// <summary>
        /// Language Update
        /// </summary>
        /// <param name="languageUpdate"></param>
        /// <returns></returns>
        public LanguageAddModel UpdateLanguage(LanguageAddModel languageUpdate)
        {
            var getLanguage = this.context?.Language.Where(x => x.Locale.ToLower() == languageUpdate.Language.Locale.ToLower() && x.LangId != languageUpdate.Language.LangId).ToList();

            if (getLanguage.Count > 0)
            {
                languageUpdate._message = "This Language Name Already Exist";
                languageUpdate._failure = true;
            }
            else
            {
                var getLanguageData = this.context?.Language.FirstOrDefault(x => x.LangId == languageUpdate.Language.LangId);
                getLanguageData.LanguageCode = languageUpdate.Language.LanguageCode;
                getLanguageData.Lcid = languageUpdate.Language.Lcid;
                getLanguageData.Locale = languageUpdate.Language.LanguageCode;
                getLanguageData.UpdatedOn = DateTime.UtcNow;
                getLanguageData.UpdatedBy = languageUpdate.Language.UpdatedBy;
                this.context?.SaveChanges();
                languageUpdate._failure = false;
            }
            return languageUpdate;
        }



        public LanguageListModel GetAllLanguage(LanguageListModel language)
        {
            LanguageListModel languageListModel = new LanguageListModel();
            try
            {
                languageListModel.TableLanguage = null;
                var languages = this.context?.Language.ToList();
                if (languages.Count > 0)
                {
                    languageListModel.TableLanguage = languages;
                    languageListModel._failure = false;
                }
                else
                {
                    languageListModel._failure = true;
                    languageListModel._message = NORECORDFOUND;
                }
                languageListModel._tenantName = language._tenantName;
                languageListModel._token = language._token;

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

        /// <summary>
        /// Delete Language
        /// </summary>
        /// <param name="languageAddModel"></param>
        /// <returns></returns>
        public LanguageAddModel DeleteLanguage(LanguageAddModel languageAddModel)
        {
            LanguageAddModel deleteLanguageModel = new LanguageAddModel();
            try
            {
                var getLanguageValue = this.context?.Language.FirstOrDefault(x => x.LangId == languageAddModel.Language.LangId);

                if (getLanguageValue != null)
                {

                    var getStudentLanguageData = this.context?.StudentMaster.Where(x => x.FirstLanguageId == languageAddModel.Language.LangId || x.SecondLanguageId == languageAddModel.Language.LangId || x.ThirdLanguageId == languageAddModel.Language.LangId).ToList();
                    if (getStudentLanguageData.Count > 0)
                    {
                        deleteLanguageModel._message = "Language cannot be deleted because it has its association";
                        deleteLanguageModel._failure = true;
                        return deleteLanguageModel;
                    }
                    var getStaffLanguageData = this.context?.StaffMaster.Where(x => x.FirstLanguage == languageAddModel.Language.LangId || x.SecondLanguage == languageAddModel.Language.LangId || x.ThirdLanguage == languageAddModel.Language.LangId).ToList();
                    if (getStaffLanguageData.Count > 0)
                    {
                        deleteLanguageModel._message = "Language cannot be deleted because it has its association";
                        deleteLanguageModel._failure = true;
                        return deleteLanguageModel;
                    }
                    var getUserLanguageData = this.context?.UserMaster.Where(x => x.LangId == languageAddModel.Language.LangId).ToList();
                    if (getUserLanguageData.Count > 0)
                    {
                        deleteLanguageModel._message = "Language cannot be deleted because it has its association";
                        deleteLanguageModel._failure = true;
                        return deleteLanguageModel;
                    }


                    this.context?.Language.Remove(getLanguageValue);
                    this.context?.SaveChanges();
                    deleteLanguageModel._failure = false;
                    deleteLanguageModel._message = "Deleted";

                }

            }
            catch (Exception ex)
            {
                deleteLanguageModel._failure = true;
                deleteLanguageModel._message = ex.Message;
            }
            return deleteLanguageModel;
        }



        /// <summary>
        /// Dropdown value Add
        /// </summary>
        /// <param name="dpdownValue"></param>
        /// <returns></returns>
        public DropdownValueAddModel AddDropdownValue(DropdownValueAddModel dpdownValue)
        {
            var getDpvalue = this.context?.DpdownValuelist.FirstOrDefault(x => x.LovColumnValue.ToLower() == dpdownValue.DropdownValue.LovColumnValue.ToLower() && x.LovName.ToLower() == dpdownValue.DropdownValue.LovName.ToLower() && x.SchoolId == dpdownValue.DropdownValue.SchoolId);

            if (getDpvalue != null)
            {
                dpdownValue._message = "This Title Already Exist";
                dpdownValue._failure = true;
            }
            else
            {
                long? dpdownValueId = Utility.GetMaxLongPK(this.context, new Func<DpdownValuelist, long>(x => x.Id));
                dpdownValue.DropdownValue.Id = (long)dpdownValueId;
                dpdownValue.DropdownValue.CreatedOn = DateTime.UtcNow;
                this.context?.DpdownValuelist.Add(dpdownValue.DropdownValue);
                this.context?.SaveChanges();
            }
            return dpdownValue;
        }

        /// <summary>
        /// Dropdown value Update
        /// </summary>
        /// <param name="dpdownValue"></param>
        /// <returns></returns>
        public DropdownValueAddModel UpdateDropdownValue(DropdownValueAddModel dpdownValue)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var getDpdownData = this.context?.DpdownValuelist.Where(x => x.LovColumnValue.ToLower() == dpdownValue.DropdownValue.LovColumnValue.ToLower() && x.Id != dpdownValue.DropdownValue.Id && x.SchoolId == dpdownValue.DropdownValue.SchoolId).ToList();

                    if (getDpdownData.Count > 0)
                    {
                        dpdownValue._message = "This Title Already Exist";
                        dpdownValue._failure = true;
                    }
                    else
                    {

                        var getDpdownValue = this.context?.DpdownValuelist.FirstOrDefault(x => x.TenantId == dpdownValue.DropdownValue.TenantId && x.SchoolId == dpdownValue.DropdownValue.SchoolId && x.Id == dpdownValue.DropdownValue.Id);

                        if (getDpdownValue != null)
                        {

                            switch (getDpdownValue.LovName)
                            {
                                case "Race":
                                    var raceValueStaff = this.context?.StaffMaster.Where(x => x.Race.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();
                                    var raceValueStudent = this.context?.StudentMaster.Where(x => x.Race.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();

                                    if (raceValueStaff.Count > 0)
                                    {
                                        foreach (var staff in raceValueStaff)
                                        {
                                            staff.Race = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.StaffMaster.UpdateRange(raceValueStaff);
                                    }
                                    if (raceValueStudent.Count > 0)
                                    {
                                        foreach (var student in raceValueStudent)
                                        {
                                            student.Race = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.StudentMaster.UpdateRange(raceValueStudent);
                                    }
                                    break;
                                case "School Level":
                                    var schoolLevelValue = this.context?.SchoolMaster.Where(x => x.SchoolLevel.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();

                                    if (schoolLevelValue.Count > 0)
                                    {
                                        foreach (var schoolLevel in schoolLevelValue)
                                        {
                                            schoolLevel.SchoolLevel = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.SchoolMaster.UpdateRange(schoolLevelValue);

                                    }

                                    break;
                                case "School Classification":
                                    var schoolClassificationValue = this.context?.SchoolMaster.Where(x => x.SchoolClassification.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();

                                    if (schoolClassificationValue.Count > 0)
                                    {
                                        foreach (var schoolClassification in schoolClassificationValue)
                                        {
                                            schoolClassification.SchoolClassification = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.SchoolMaster.UpdateRange(schoolClassificationValue);
                                    }

                                    break;
                                case "Female Toilet Type":
                                    var femaleToiletTypeValue = this.context?.SchoolDetail.Where(x => x.FemaleToiletType.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();

                                    if (femaleToiletTypeValue.Count > 0)
                                    {
                                        foreach (var femaleToilet in femaleToiletTypeValue)
                                        {
                                            femaleToilet.FemaleToiletType = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.SchoolDetail.UpdateRange(femaleToiletTypeValue);
                                    }

                                    break;
                                case "Male Toilet Type":
                                    var maleToiletTypeValue = this.context?.SchoolDetail.Where(x => x.MaleToiletType.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();

                                    if (maleToiletTypeValue.Count > 0)
                                    {
                                        foreach (var maleToilet in maleToiletTypeValue)
                                        {
                                            maleToilet.MaleToiletType = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.SchoolDetail.UpdateRange(maleToiletTypeValue);
                                    }

                                    break;
                                case "Common Toilet Type":
                                    var commonToiletTypeValue = this.context?.SchoolDetail.Where(x => x.ComonToiletType.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();

                                    if (commonToiletTypeValue.Count > 0)
                                    {
                                        foreach (var ComonToilet in commonToiletTypeValue)
                                        {
                                            ComonToilet.ComonToiletType = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.SchoolDetail.UpdateRange(commonToiletTypeValue);
                                    }

                                    break;
                                case "Ethnicity":
                                    var ethnicityValueStaff = this.context?.StaffMaster.Where(x => x.Ethnicity.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();
                                    var ethnicityValueStudent = this.context?.StudentMaster.Where(x => x.Ethnicity.ToLower() == getDpdownValue.LovColumnValue.ToLower()).ToList();

                                    if (ethnicityValueStaff.Count > 0)
                                    {
                                        foreach (var staff in ethnicityValueStaff)
                                        {
                                            staff.Ethnicity = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.StaffMaster.UpdateRange(ethnicityValueStaff);
                                    }
                                    if (ethnicityValueStudent.Count > 0)
                                    {
                                        foreach (var student in ethnicityValueStudent)
                                        {
                                            student.Ethnicity = dpdownValue.DropdownValue.LovColumnValue;
                                        }
                                        this.context?.StudentMaster.UpdateRange(ethnicityValueStudent);
                                    }
                                    break;
                            }
                        }

                        getDpdownValue.LovColumnValue = dpdownValue.DropdownValue.LovColumnValue;
                        getDpdownValue.UpdatedOn = DateTime.UtcNow;
                        getDpdownValue.UpdatedBy = dpdownValue.DropdownValue.UpdatedBy;
                        this.context?.SaveChanges();

                        dpdownValue._failure = false;
                        transaction.Commit();

                    }
                    return dpdownValue;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    dpdownValue.DropdownValue = null;
                    dpdownValue._failure = true;
                    dpdownValue._message = ex.Message;
                    return dpdownValue;
                }
            }
        }


        /// <summary>
        /// Get DropdownValue By Id
        /// </summary>
        /// <param name="dpdownValue"></param>
        /// <returns></returns>
        public DropdownValueAddModel ViewDropdownValue(DropdownValueAddModel dpdownValue)
        {
            try
            {
                DropdownValueAddModel dropdownValueAddModel = new DropdownValueAddModel();
                var getDpdownValue = this.context?.DpdownValuelist.FirstOrDefault(x => x.TenantId == dpdownValue.DropdownValue.TenantId && x.SchoolId == dpdownValue.DropdownValue.SchoolId && x.Id == dpdownValue.DropdownValue.Id);
                if (getDpdownValue != null)
                {
                    dropdownValueAddModel.DropdownValue = getDpdownValue;
                    dropdownValueAddModel._tenantName = dpdownValue._tenantName;
                    dropdownValueAddModel._failure = false;
                    return dropdownValueAddModel;
                }
                else
                {
                    dropdownValueAddModel._failure = true;
                    dropdownValueAddModel._message = NORECORDFOUND;
                    return dropdownValueAddModel;
                }
            }
            catch (Exception es)
            {

                throw;
            }
        }


        /// <summary>
        /// Get All Dropdown Value
        /// </summary>
        /// <param name="dpdownList"></param>
        /// <returns></returns>
        public DropdownValueListModel GetAllDropdownValues(DropdownValueListModel dpdownList)
        {
            DropdownValueListModel dpdownListModel = new DropdownValueListModel();
            try
            {

                var dropdownList = this.context?.DpdownValuelist.Where(x => x.TenantId == dpdownList.TenantId && x.SchoolId == dpdownList.SchoolId && x.LovName.ToLower() == dpdownList.LovName.ToLower()).ToList();


                if (dropdownList.Count > 0)
                {
                    dpdownListModel.dropdownList = dropdownList;
                    dpdownListModel._failure = false;
                }
                else
                {
                    dpdownListModel.dropdownList = null;
                    dpdownListModel._failure = true;
                    dpdownListModel._message = NORECORDFOUND;
                }
                dpdownListModel._tenantName = dpdownList._tenantName;
                dpdownListModel._token = dpdownList._token;

            }
            catch (Exception es)
            {
                dpdownListModel._message = es.Message;
                dpdownListModel._failure = true;
                dpdownListModel._tenantName = dpdownList._tenantName;
                dpdownListModel._token = dpdownList._token;
            }
            return dpdownListModel;

        }

        /// <summary>
        /// Add Country
        /// </summary>
        /// <param name="countryAddModel"></param>
        /// <returns></returns>
        public CountryAddModel AddCountry(CountryAddModel countryAddModel)
        {
            try
            {
                var getCountry = this.context?.Country.FirstOrDefault(x => x.Name.ToLower() == countryAddModel.country.Name.ToLower());

                if (getCountry != null)
                {
                    countryAddModel._message = "This Country Name Already Exist";
                    countryAddModel._failure = true;
                }
                else
                {
                    int? CountryId = Utility.GetMaxPK(this.context, new Func<Country, int>(x => x.Id));
                    countryAddModel.country.Id = (int)CountryId;
                    countryAddModel.country.CreatedOn = DateTime.UtcNow;
                    this.context?.Country.Add(countryAddModel.country);
                    this.context?.SaveChanges();
                }
            }
            catch (Exception es)
            {
                countryAddModel._message = es.Message;
                countryAddModel._failure = true;
                countryAddModel._tenantName = countryAddModel._tenantName;
                countryAddModel._token = countryAddModel._token;
            }
            return countryAddModel;
        }

        /// <summary>
        /// Update Country
        /// </summary>
        /// <param name="countryAddModel"></param>
        /// <returns></returns>
        public CountryAddModel UpdateCountry(CountryAddModel countryAddModel)
        {
            try
            {
                var getCountry = this.context?.Country.Where(x => x.Name.ToLower() == countryAddModel.country.Name.ToLower() && x.Id != countryAddModel.country.Id).ToList();

                if (getCountry.Count > 0)
                {
                    countryAddModel._message = "This Country Name Already Exist";
                    countryAddModel._failure = true;
                }
                else
                {
                    var getCountryData = this.context?.Country.FirstOrDefault(x => x.Id == countryAddModel.country.Id);
                    getCountryData.Name = countryAddModel.country.Name;
                    getCountryData.CountryCode = countryAddModel.country.CountryCode;
                    getCountryData.UpdatedOn = DateTime.UtcNow;
                    getCountryData.UpdatedBy = countryAddModel.country.UpdatedBy;
                    this.context?.SaveChanges();
                    countryAddModel._failure = false;
                    countryAddModel._message = "Updated Successfully";
                }
            }
            catch (Exception es)
            {
                countryAddModel._message = es.Message;
                countryAddModel._failure = true;
                countryAddModel._tenantName = countryAddModel._tenantName;
                countryAddModel._token = countryAddModel._token;
            }
            return countryAddModel;
        }

        /// <summary>
        /// Delete Country
        /// </summary>
        /// <param name="countryDeleteModel"></param>
        /// <returns></returns>
        public CountryAddModel DeleteCountry(CountryAddModel countryDeleteModel)
        {
            CountryAddModel deleteCountryModel = new CountryAddModel();
            try
            {
                var getCountryValue = this.context?.Country.FirstOrDefault(x => x.Id == countryDeleteModel.country.Id);

                if (getCountryValue != null)
                {
                    var getPatrentCountryData = this.context?.ParentAddress.Where(x => x.Country == countryDeleteModel.country.Id.ToString()).ToList();
                    if (getPatrentCountryData.Count > 0)
                    {
                        deleteCountryModel._message = "Country cannot be deleted because it has its association";
                        deleteCountryModel._failure = true;
                        return deleteCountryModel;
                    }
                    var getStudentCountryData = this.context?.StudentMaster.Where(x => x.CountryOfBirth == countryDeleteModel.country.Id || x.HomeAddressCountry == countryDeleteModel.country.Id.ToString() || x.MailingAddressCountry == countryDeleteModel.country.Id.ToString()).ToList();
                    if (getStudentCountryData.Count > 0)
                    {
                        deleteCountryModel._message = "Country cannot be deleted because it has its association";
                        deleteCountryModel._failure = true;
                        return deleteCountryModel;
                    }
                    var getStaffCountryData = this.context?.StaffMaster.Where(x => x.CountryOfBirth == countryDeleteModel.country.Id || x.HomeAddressCountry == countryDeleteModel.country.Id.ToString() || x.MailingAddressCountry == countryDeleteModel.country.Id.ToString()).ToList();
                    if (getStaffCountryData.Count > 0)
                    {
                        deleteCountryModel._message = "Country cannot be deleted because it has its association";
                        deleteCountryModel._failure = true;
                        return deleteCountryModel;
                    }
                    var getSchoolCountryData = this.context?.SchoolMaster.Where(x => x.Country == countryDeleteModel.country.Id.ToString()).ToList();
                    if (getSchoolCountryData.Count > 0)
                    {
                        deleteCountryModel._message = "Country cannot be deleted because it has its association";
                        deleteCountryModel._failure = true;
                        return deleteCountryModel;
                    }
                    var getSateData = this.context?.State.Where(x => x.CountryId == countryDeleteModel.country.Id).ToList();
                    if (getSateData.Count > 0)
                    {
                        deleteCountryModel._message = "Country cannot be deleted because it has its association";
                        deleteCountryModel._failure = true;
                        return deleteCountryModel;
                    }

                    this.context?.Country.Remove(getCountryValue);
                    this.context?.SaveChanges();
                    deleteCountryModel._failure = false;
                    deleteCountryModel._message = "Deleted";

                }

            }
            catch (Exception ex)
            {
                deleteCountryModel._failure = true;
                deleteCountryModel._message = ex.Message;
            }
            return deleteCountryModel;
        }

        /// <summary>
        /// Delete Dropdown Value
        /// </summary>
        /// <param name="dpdownValue"></param>
        /// <returns></returns>
        public DropdownValueAddModel DeleteDropdownValue(DropdownValueAddModel dpdownValue)
        {
            DropdownValueAddModel dropdownValueAddModel = new DropdownValueAddModel();
            try
            {
                var getDpValue = this.context?.DpdownValuelist.FirstOrDefault(x => x.Id == dpdownValue.DropdownValue.Id);

                if (getDpValue != null)
                {

                    switch (getDpValue.LovName)
                    {
                        case "Race":

                            var raceValueStaff = this.context?.StaffMaster.Where(x => x.Race.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();
                            if (raceValueStaff.Count > 0)
                            {
                                dropdownValueAddModel._message = "Race cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }

                            var raceValueStudent = this.context?.StudentMaster.Where(x => x.Race.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();

                            if (raceValueStudent.Count > 0)
                            {
                                dropdownValueAddModel._message = "Race cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }


                            break;
                        case "School Level":
                            var schoolLevelValue = this.context?.SchoolMaster.Where(x => x.SchoolLevel.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();

                            if (schoolLevelValue.Count > 0)
                            {
                                dropdownValueAddModel._message = "School Level cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }

                            break;
                        case "School Classification":
                            var schoolClassificationValue = this.context?.SchoolMaster.Where(x => x.SchoolClassification.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();

                            if (schoolClassificationValue.Count > 0)
                            {
                                dropdownValueAddModel._message = "School Classification cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }

                            break;
                        case "Female Toilet Type":
                            var femaleToiletTypeValue = this.context?.SchoolDetail.Where(x => x.FemaleToiletType.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();

                            if (femaleToiletTypeValue.Count > 0)
                            {
                                dropdownValueAddModel._message = "Female Toilet Type cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }

                            break;
                        case "Male Toilet Type":
                            var maleToiletTypeValue = this.context?.SchoolDetail.Where(x => x.MaleToiletType.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();

                            if (maleToiletTypeValue.Count > 0)
                            {
                                dropdownValueAddModel._message = "Male Toilet Type cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }

                            break;
                        case "Common Toilet Type":
                            var commonToiletTypeValue = this.context?.SchoolDetail.Where(x => x.ComonToiletType.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();

                            if (commonToiletTypeValue.Count > 0)
                            {
                                dropdownValueAddModel._message = "Common Toilet Type cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }

                            break;
                        case "Ethnicity":
                            var ethnicityValueStaff = this.context?.StaffMaster.Where(x => x.Ethnicity.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();
                            if (ethnicityValueStaff.Count > 0)
                            {
                                dropdownValueAddModel._message = "Ethnicity cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }
                            var ethnicityValueStudent = this.context?.StudentMaster.Where(x => x.Ethnicity.ToLower() == getDpValue.LovColumnValue.ToLower() && x.SchoolId == getDpValue.SchoolId).ToList();

                            if (ethnicityValueStudent.Count > 0)
                            {
                                dropdownValueAddModel._message = "Ethnicity cannot be deleted because it has its association";
                                dropdownValueAddModel._failure = true;
                                return dropdownValueAddModel;
                            }

                            break;
                    }
                    this.context?.DpdownValuelist.Remove(getDpValue);
                    this.context?.SaveChanges();
                    dropdownValueAddModel._failure = false;
                    dropdownValueAddModel._message = "Deleted";
                }

            }
            catch (Exception es)
            {
                dropdownValueAddModel._failure = true;
                dropdownValueAddModel._message = es.Message;
            }
            return dropdownValueAddModel;

        }

    }
}
