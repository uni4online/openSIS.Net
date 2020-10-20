import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { GetAllMembersList } from 'src/app/models/membershipModel';
import { CalendarAddViewModel, Weeks } from 'src/app/models/calendarModel';
import { MembershipService } from '../../../../services/membership.service';
import { CalendarService } from '../../../../services/calendar.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ValidationService } from '../../../shared/validation.service';
import * as moment from 'moment';


@Component({
  selector: 'vex-add-calendar',
  templateUrl: './add-calendar.component.html',
  styleUrls: ['./add-calendar.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class AddCalendarComponent implements OnInit {

  calendarTitle: string;
  getAllMembersList: GetAllMembersList = new GetAllMembersList();
  calendarAddViewModel = new CalendarAddViewModel();
  weekArray: number[] = [];
  membercount: number;
  memberArray: number[] = [];
  @Output() afterClosed = new EventEmitter<boolean>();
  form: FormGroup;
  icClose = icClose;
  weeks: Weeks[] = [
    { name: 'sunday', id: 0 },
    { name: 'monday', id: 1 },
    { name: 'tuesday', id: 2 },
    { name: 'wednesday', id: 3 },
    { name: 'thursday', id: 4 },
    { name: 'friday', id: 5 },
    { name: 'saturday', id: 6 }
  ];
  constructor(private dialogRef: MatDialogRef<AddCalendarComponent>,
    private fb: FormBuilder, private _membershipService: MembershipService,
    private _calendarService: CalendarService, @Inject(MAT_DIALOG_DATA) public data: any, private snackbar: MatSnackBar) {


  }

  ngOnInit(): void {

    this.form = this.fb.group({
      title: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: [''],
      isDefaultCalendar: [false]
    });
   
    if (this.data == null) {
      this.snackbar.open('Null vallue occur. ', '', {
        duration: 1000
      });

    }
    else {
      this.membercount = this.data.membercount
      this.getAllMembersList = this.data.allMembers
      if (this.data.calendar == null) {
        this.calendarTitle = "addCalendar";
      }
      else {
        this.calendarTitle = "editCalendar";
        this.calendarAddViewModel.schoolCalendar = this.data.calendar;
        this.form.patchValue({isDefaultCalendar: this.calendarAddViewModel.schoolCalendar.defaultCalender});
        this.weekArray = this.calendarAddViewModel.schoolCalendar.days.split('').map(x => +x);
        if (this.calendarAddViewModel.schoolCalendar.visibleToMembershipId != null) {
          var membershipIds: string[] = this.calendarAddViewModel.schoolCalendar.visibleToMembershipId.split(',');
          this.memberArray = membershipIds.map(Number);
        }

      }

    }

  }

  dateCompare() {
    let openingDate = new Date(this.form.controls.startDate.value);
    let closingDate = new Date(this.form.controls.endDate.value);
   
    if (ValidationService.compareValidation(openingDate, closingDate) === false) {
      this.form.controls.endDate.setErrors({ compareError: true });
      
    }
  }

  private formatDate(date: string): string {
    if (date === undefined || date === null) {
      return undefined;
    } else {
      return moment(date).format('YYYY-MM-DDTHH:mm:ss.SSS');
    }
  }

  submitCalendar() {
    this.calendarAddViewModel.schoolCalendar.title = this.form.value.title;
    this.calendarAddViewModel.schoolCalendar.defaultCalender = this.form.value.isDefaultCalendar;
    this.calendarAddViewModel.schoolCalendar.academicYear = new Date(this.form.value.startDate).getFullYear();
    this.calendarAddViewModel.schoolCalendar.days = this.weekArray.toString().replace(/,/g, "");
    this.calendarAddViewModel.schoolCalendar.visibleToMembershipId = this.memberArray.toString();
    this.calendarAddViewModel.schoolCalendar.startDate = this.formatDate(this.form.value.startDate);
    this.calendarAddViewModel.schoolCalendar.endDate = this.formatDate(this.form.value.endDate);
    if (this.form.valid && this.weekArray.length > 0) {
      if (this.calendarAddViewModel.schoolCalendar.calenderId > 0) {
        this._calendarService.updateCalendar(this.calendarAddViewModel).subscribe(data => {
          if (data._failure) {
            this.snackbar.open('Calendar updating failed. ' + data._message, '', {
              duration: 10000
            });
          } else {
            this.snackbar.open('Calendar updated successfully. ', '', {
              duration: 10000
            });
            this.dialogRef.close('submited');
          }

        });
      }
      else {
        this._calendarService.addCalendar(this.calendarAddViewModel).subscribe(data => {
          if (data._failure) {
            this.snackbar.open('Calendar saving failed. ' + data._message, '', {
              duration: 10000
            });
          } else {
            this.snackbar.open('Calendar saved successfully. ', '', {
              duration: 10000
            });
            this.dialogRef.close('submited');
          }
        });
      }
    }
    
  }
  selectDays(event, id) {
    event.preventDefault();
    var index = this.weekArray.indexOf(id);
    if (index > -1) {
      this.weekArray.splice(index, 1);
    }
    else {
      this.weekArray.push(id);
    }

  }

  updateCheck(event) {
    if (this.memberArray.length === this.getAllMembersList.getAllMemberList.length) {
      for (let i = 1; i <= this.getAllMembersList.getAllMemberList.length; i++) {
        var index = this.memberArray.indexOf(i);
        if (index > -1) {
          this.memberArray.splice(index, 1);
        }
        else {
          this.memberArray.push(i);
        }
      }
    }
    else if (this.memberArray.length === 0) {
      for (let i = 1; i <= this.getAllMembersList.getAllMemberList.length; i++) {
        var index = this.memberArray.indexOf(i);
        if (index > -1) {
          this.memberArray.splice(index, 1);
        }
        else {
          this.memberArray.push(i);
        }
      }
    }
    else {
      for (let i = 1; i <= this.getAllMembersList.getAllMemberList.length; i++) {
        let index = this.memberArray.indexOf(i);
        if (index > -1) {
          continue;
        }
        else {
          this.memberArray.push(i);
        }
      }
    }


  }
  selectChildren(event, id) {
    event.preventDefault();
    var index = this.memberArray.indexOf(id);
    if (index > -1) {
      this.memberArray.splice(index, 1);
    }
    else {
      this.memberArray.push(id);
    }

  }


}
