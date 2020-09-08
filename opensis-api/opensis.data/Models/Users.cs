using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace opensis.data.Models
{
    public class Users
    {
        [Key]
        public Int64 user_id { get; set; }       
        public string tenant_id { get; set; }
        public long school_id { get; set; }
        public string school_name { get; set; }        
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        public bool isactive { get; set; }
    }
}
