using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Language
    {
        public Language()
        {
            StaffMasterFirstLanguageNavigation = new HashSet<StaffMaster>();
            StaffMasterSecondLanguageNavigation = new HashSet<StaffMaster>();
            StaffMasterThirdLanguageNavigation = new HashSet<StaffMaster>();
            StudentMasterFirstLanguage = new HashSet<StudentMaster>();
            StudentMasterSecondLanguage = new HashSet<StudentMaster>();
            StudentMasterThirdLanguage = new HashSet<StudentMaster>();
            UserMaster = new HashSet<UserMaster>();
        }

        public int LangId { get; set; }
        public string Lcid { get; set; }
        public string Locale { get; set; }
        public string LanguageCode { get; set; }

        public virtual ICollection<UserMaster> UserMaster { get; set; }
        public virtual ICollection<StudentMaster> StudentMasterFirstLanguage { get; set; }
        public virtual ICollection<StudentMaster> StudentMasterSecondLanguage { get; set; }
        public virtual ICollection<StudentMaster> StudentMasterThirdLanguage { get; set; }
        public virtual ICollection<StaffMaster> StaffMasterFirstLanguageNavigation { get; set; }
        public virtual ICollection<StaffMaster> StaffMasterSecondLanguageNavigation { get; set; }
        public virtual ICollection<StaffMaster> StaffMasterThirdLanguageNavigation { get; set; }
    }
}
