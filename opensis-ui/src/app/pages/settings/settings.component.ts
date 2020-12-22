import { Component, OnInit } from '@angular/core';
import icSchool from '@iconify/icons-ic/twotone-account-balance';
import icStudents from '@iconify/icons-ic/twotone-school';
import icUsers from '@iconify/icons-ic/twotone-people';
import icSchedule from '@iconify/icons-ic/twotone-date-range';
import icGrade from '@iconify/icons-ic/twotone-leaderboard';
import icAttendance from '@iconify/icons-ic/twotone-access-alarm';
import icParents from '@iconify/icons-ic/baseline-escalator-warning';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

  icSchool = icSchool;
  icStudents = icStudents;
  icUsers = icUsers;
  icSchedule = icSchedule;
  icGrade = icGrade;
  icAttendance = icAttendance;
  icParents = icParents;

  constructor(public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }
  setPageId(pageId = 'Period'){
    localStorage.setItem("pageId", pageId);
  }
}
