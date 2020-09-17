using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class TableLanguage
    {
        public TableLanguage()
        {
            TableUserMaster = new HashSet<TableUserMaster>();
        }

        public int LangId { get; set; }
        public string Lcid { get; set; }
        public string Locale { get; set; }
        public string LanguageCode { get; set; }

        public virtual ICollection<TableUserMaster> TableUserMaster { get; set; }
    }
}
