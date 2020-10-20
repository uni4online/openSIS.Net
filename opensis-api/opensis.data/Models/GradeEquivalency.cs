using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class GradeEquivalency
    {
        public GradeEquivalency()
        {
            Gradelevels = new HashSet<Gradelevels>();
        }

        public string IscedGradeLevel { get; set; }
        public string GradeDescription { get; set; }
        public string AgeRange { get; set; }

        public virtual ICollection<Gradelevels> Gradelevels { get; set; }
    }
}
