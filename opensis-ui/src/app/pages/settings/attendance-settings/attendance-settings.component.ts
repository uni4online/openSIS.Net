import { Component, OnInit } from '@angular/core';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';

@Component({
  selector: 'vex-attendance-settings',
  templateUrl: './attendance-settings.component.html',
  styleUrls: ['./attendance-settings.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})
export class AttendanceSettingsComponent implements OnInit {
  pages=['Attendance Codes']
  attendanceSettings=true;
  pageTitle = 'Attendance Codes';
  pageId: string = '';
  displayAttendanceCodes = false;

  AttendanceCodesFlag: boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
    this.showPage(this.pageId);
  }

  getSelectedPage(event){
    this.showPage(event);
  }

  showPage(pageId = 'Attendance Codes') {
    this.pageTitle = pageId;
    if (pageId === 'Attendance Codes') {
      this.displayAttendanceCodes = true;
      this.AttendanceCodesFlag = true;
    } else {
      this.displayAttendanceCodes = false;
      this.AttendanceCodesFlag = false;
    }
  }

}
