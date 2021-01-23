using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CourseManager
{
    public class CourseAddViewModel : CommonFields
    {
        public Course course { get; set; }
        public int? ProgramId { get; set; }
        public int? SubjectId { get; set; }

    }
}
