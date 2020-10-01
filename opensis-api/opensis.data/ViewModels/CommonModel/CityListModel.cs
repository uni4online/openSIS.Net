using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CommonModel
{
    public class CityListModel : CommonFields
    {
        public List<City> TableCity { get; set; }
        public int StateId { get; set; }
    }
}
