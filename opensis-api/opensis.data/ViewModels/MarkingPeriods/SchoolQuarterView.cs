using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.MarkingPeriods
{
    public class SchoolQuarterView
    {
        public SchoolQuarterView()
        {
            Children = new List<SchoolProgressPeriodView>();
        }
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int MarkingPeriodId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public int SemesterId { get; set; }
        public bool IsParent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PostStartDate { get; set; }
        public DateTime? PostEndDate { get; set; }
        public bool? DoesGrades { get; set; }
        public bool? DoesExam { get; set; }
        public bool? DoesComments { get; set; }
        public List<SchoolProgressPeriodView> Children { get; set; }
    }
}
