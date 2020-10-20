using opensis.data.ViewModels.AttendanceCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface IAttendanceCodeRepository
    {
        public AttendanceCodeAddViewModel AddAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel);
        public AttendanceCodeAddViewModel ViewAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel);
        public AttendanceCodeAddViewModel UpdateAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel);
        public AttendanceCodeAddViewModel DeleteAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel);
    }
}
