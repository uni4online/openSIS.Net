using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CourseManager
{
    public class GetCourseSectionForView
    {
        public GetCourseSectionForView()
        {
            courseVariableSchedule = new List<CourseVariableSchedule>();
            courseBlockSchedule = new List<CourseBlockSchedule>();
        }
        public CourseSection courseSection { get; set; }
        public CourseFixedSchedule courseFixedSchedule { get; set; }
        public List<CourseVariableSchedule> courseVariableSchedule { get; set; }
        public CourseCalendarSchedule courseCalendarSchedule { get; set; }
        public List<CourseBlockSchedule> courseBlockSchedule { get; set; }
    }
}
