using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace opensis.data.ViewModels.User
{
    public class MasterUserViewModel : CommonFields
    {
        public tblUserMaster user { get; set; }
        [Required]
        public string password { get; set; }

    }
}
