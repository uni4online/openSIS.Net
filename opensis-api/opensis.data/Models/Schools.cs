using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace opensis.data.Models
{
    public class Schools
    {
        [Key]
        public long school_id { get; set; }
        public string tenant_id { get; set; }
        public string school_name { get; set; }
        public string school_address { get; set; }
        public bool isactive { get; set; }
    }
}
