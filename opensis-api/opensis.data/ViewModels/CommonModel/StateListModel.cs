using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CommonModel
{
    public class StateListModel : CommonFields
    {
        public List<State> TableState { get; set; }
        public int CountryId { get; set; }
    }
}
