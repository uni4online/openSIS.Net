using opensis.data.ViewModels.AttendanceCodes;
using opensis.data.ViewModels.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.AttendanceCode.Interfaces
{
    public interface IAttendanceCodeRegisterService
    {
        public AttendanceCodeAddViewModel SaveAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel);
        public AttendanceCodeAddViewModel ViewAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel);
        public AttendanceCodeAddViewModel UpdateAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel);
        public AttendanceCodeAddViewModel DeleteAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel);
    }
}
