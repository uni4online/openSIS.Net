import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddTeacherComponent } from './add-teacher/add-teacher.component';
import { AddCourseSectionComponent } from './add-course-section/add-course-section.component';

@Component({
  selector: 'vex-schedule-teacher',
  templateUrl: './schedule-teacher.component.html',
  styleUrls: ['./schedule-teacher.component.scss']
})
export class ScheduleTeacherComponent implements OnInit {

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  selectTeacher(){
    this.dialog.open(AddTeacherComponent, {
      width: '900px'
    });
  }

  selectCourseSection(){
    this.dialog.open(AddCourseSectionComponent, {
      width: '900px'
    });
  }

}
