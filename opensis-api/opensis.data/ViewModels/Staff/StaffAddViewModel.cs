using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Staff
{
   public class StaffAddViewModel : CommonFields
    {
        public StaffMaster staffMaster { get; set; }
        public List<FieldsCategory> fieldsCategoryList { get; set; }
        public string PasswordHash { get; set; }
        public int? SelectedCategoryId { get; set; }
    }
}
