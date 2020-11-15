import { Component, OnInit } from '@angular/core';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';


@Component({
  selector: 'vex-staff-settings',
  templateUrl: './staff-settings.component.html',
  styleUrls: ['./staff-settings.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})
export class StaffSettingsComponent implements OnInit {
  pages=['Staff Fields']
  staffSettings=true;
  pageTitle = 'Staff Fields';
  pageId: string = '';
  displayStaffFields = true;

  StaffFieldsFlag: boolean = true;

  constructor() { }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
    this.showPage(this.pageId);
  }

  getSelectedPage(event){
    this.showPage(event);
  }

  showPage(pageId = 'Staff Fields') {
    this.pageTitle = pageId;
    if (pageId === 'Staff Fields') {
      this.displayStaffFields = true;
      this.StaffFieldsFlag = true;
    } else {
      this.displayStaffFields = false;
      this.StaffFieldsFlag = false;
    }
  }

}
