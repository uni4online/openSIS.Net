using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class GradeEquivalency
    {
        public string Country { get; set; }
        public int? IscedGradeLevel { get; set; }
        public string GradeDescription { get; set; }
        public string AgeRange { get; set; }
        public string EducationalStage { get; set; }
    }
}
