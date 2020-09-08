using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace opensis.data.ViewModels.User
{
    public class LoginViewModel : CommonFields
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public Guid? Tenant_Id { get; set; }

        public int? User_Id { get; set; }
    }
}
