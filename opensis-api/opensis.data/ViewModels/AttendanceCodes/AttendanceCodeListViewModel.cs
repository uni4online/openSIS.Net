using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.AttendanceCodes
{
    public class AttendanceCodeListViewModel :CommonFields
    {
        public List<AttendanceCode> attendanceCodeList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? AttendanceCategoryId { get; set; }
    }
}
