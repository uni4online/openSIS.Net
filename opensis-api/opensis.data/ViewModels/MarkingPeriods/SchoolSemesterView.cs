﻿using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.MarkingPeriods
{
    public class SchoolSemesterView
    {
        public SchoolSemesterView()
        {
            Children = new List<SchoolQuarterView>();
        }
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int MarkingPeriodId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public int YearId { get; set; }
        public bool IsParent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PostStartDate { get; set; }
        public DateTime? PostEndDate { get; set; }
        public bool? DoesGrades { get; set; }
        public bool? DoesExam { get; set; }
        public bool? DoesComments { get; set; }
        public List<SchoolQuarterView> Children { get; set; }
    }
}
