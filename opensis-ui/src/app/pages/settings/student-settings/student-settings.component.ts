import { Component, OnInit } from '@angular/core';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';

@Component({
  selector: 'vex-student-settings',
  templateUrl: './student-settings.component.html',
  styleUrls: ['./student-settings.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})
export class StudentSettingsComponent implements OnInit {
  pages=['Student Fields', 'Enrollment Codes']
  studentSettings=true;
  pageTitle = 'Grade Levels';
  pageId: string = '';
  displayStudentFields = false;
  displayEnrollmentCodes = false;

  studentFieldsFlag: boolean = false;
  enrollmentCodesFlag: boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
    this.showPage(this.pageId);
  }

  getSelectedPage(event){
    this.showPage(event);
  }

  showPage(pageId = 'Student Fields') {
    this.pageTitle = pageId;
    if (pageId === 'Student Fields') {
      this.displayStudentFields = true;
      this.studentFieldsFlag = true;
    } else {
      this.displayStudentFields = false;
      this.studentFieldsFlag = false;
    }
    if (pageId === 'Enrollment Codes') {
      this.displayEnrollmentCodes = true;
      this.enrollmentCodesFlag = true;
    } else {
      this.displayEnrollmentCodes = false;
      this.enrollmentCodesFlag = false;
    }
  }

}
