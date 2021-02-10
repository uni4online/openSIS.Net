using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<City> City { get; set; }
    }
}
