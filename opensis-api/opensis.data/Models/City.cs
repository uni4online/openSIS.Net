using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual State State { get; set; }
    }
}
