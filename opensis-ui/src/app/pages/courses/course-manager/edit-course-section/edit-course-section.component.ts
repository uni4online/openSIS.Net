import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, Subject } from 'rxjs';
import * as moment from 'moment';
import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarMonthViewBeforeRenderEvent, CalendarMonthViewDay, CalendarView, DAYS_OF_WEEK } from 'angular-calendar';
import icClose from '@iconify/icons-ic/twotone-close';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/twotone-add';
import icList from '@iconify/icons-ic/twotone-list-alt';
import icInfo from '@iconify/icons-ic/info';
import icRemove from '@iconify/icons-ic/remove-circle';
import icBack from '@iconify/icons-ic/baseline-arrow-back';
import icExpand from '@iconify/icons-ic/outline-add-box';
import icCollapse from '@iconify/icons-ic/outline-indeterminate-check-box';
import icPlusCircle from '@iconify/icons-ic/add-circle-outline';
import icLeftArrow from '@iconify/icons-ic/baseline-west';
import icRightArrow from '@iconify/icons-ic/baseline-east';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-edit-course-section',
  templateUrl: './edit-course-section.component.html',
  styleUrls: ['./edit-course-section.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditCourseSectionComponent implements OnInit {

  icClose = icClose;
  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;
  icList = icList;
  icInfo = icInfo;
  icRemove = icRemove;
  icBack = icBack;
  icExpand = icExpand;
  icCollapse = icCollapse;
  icPlusCircle = icPlusCircle;
  icLeftArrow = icLeftArrow;
  icRightArrow = icRightArrow;
  useStandardGrades = false;
  isChecked = false;
  scheduleType = '1';
  durationType = '1';
  addCalendarDay = 0;
  selectedDate;

  view: CalendarView = CalendarView.Month;
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];

  constructor(private dialogRef: MatDialogRef<EditCourseSectionComponent>) {
   }

  ngOnInit(): void {
  }

  setScheduleType(mrChange) {
    this.scheduleType = mrChange.value;
    this.addCalendarDay = 0;
  }

  setDuration(mrChange) {
    this.durationType = mrChange.value;
  }

  private formatDate(date: Date): string {
    if (date === undefined || date === null) {
      return undefined;
    } else {
      return moment(date).format('YYYY-MM-DDTHH:mm:ss.SSS');
    }
  }

  openAddNewEvent(event) {
    this.addCalendarDay = 1;
    this.selectedDate = event.date;
  }

  cancelAddClass() {
    this.addCalendarDay = 0;
  }

}
