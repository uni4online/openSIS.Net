using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Language
    {
        public Language()
        {
            UserMaster = new HashSet<UserMaster>();
        }

        public int LangId { get; set; }
        public string Lcid { get; set; }
        public string Locale { get; set; }
        public string LanguageCode { get; set; }

        public virtual ICollection<UserMaster> UserMaster { get; set; }
    }
}
