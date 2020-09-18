using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CommonModel
{
    public class StateListModel : CommonFields
    {
        public List<TableState> TableState { get; set; }
        public int CountryId { get; set; }
    }
}
