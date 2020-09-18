using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CommonModel
{
    public class CountryListModel : CommonFields
    {
        public List<TableCountry> TableCountry { get; set; }
    }
}
