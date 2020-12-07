using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.User
{
    public class CheckUserEmailAddressViewModel : CommonFields
    {
        public Guid TenantId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsValidEmailAddress { get; set; }
    }
}
