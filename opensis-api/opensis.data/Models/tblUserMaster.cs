using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace opensis.data.Models
{
    [Table("Table_User_Master")]
    public class tblUserMaster
    {
        [Required]
        //[Key, Column(Order = 0)]
        public Guid Tenant_Id { get; set; }
        //[Key, Column(Order = 1)]
        public int School_id { get; set; }
        //[Key, Column(Order = 2)]
        public int User_id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}