using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace opensis.data.Models
{
    [Table("Table_Plans")]
    public class tblPlans
    {
        [Required]
        [Column(Order = 0)]
        public Guid Tenant_id { get; set; }
        [Column(Order = 1)]
        public int School_id { get; set; }
        [Column(Order = 2)]
        public int id { get; set; }
        public string name { get; set; }
        public int? max_api_checks { get; set; }
        public string features { get; set; }
    }
}
