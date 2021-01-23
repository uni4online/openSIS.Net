using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Grades
{
    public class GetGradeUsStandardForView
    {
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public string StandardRefNo { get; set; }
        public int? GradeStandardId { get; set; }
        public string GradeLevel { get; set; }
        public string Domain { get; set; }
        public string Subject { get; set; }
        public string Course { get; set; }
        public string Topic { get; set; }
        public string StandardDetails { get; set; }
    }
}
