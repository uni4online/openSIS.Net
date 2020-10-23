using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.AttendanceCodes
{
    public class AttendanceCodeCategoriesListViewModel : CommonFields
    {
        public List<AttendanceCodeCategories> attendanceCodeCategoriesList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
    }
}
