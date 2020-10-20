import { Component, OnInit } from '@angular/core';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';

@Component({
  selector: 'vex-school-settings',
  templateUrl: './school-settings.component.html',
  styleUrls: ['./school-settings.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})
export class SchoolSettingsComponent implements OnInit {
  pages=['Period', 'Grade Levels', 'Sections', 'Rooms','School Fields','Hierarchy','Preference']
  schoolSettings=true;
  pageTitle = 'Grade Levels';
  pageId: string = '';
  displayPeriod = false;
  displayGradeLevels = false;
  displaySections = false;
  displayRooms = false;
  displaySchoolFields = false;
  displayHierarchy = false;
  displayPreference = false;

  periodFlag: boolean = false;
  gradeLevelsFlag: boolean = false;
  sectionsFlag: boolean = false;
  roomsFlag: boolean = false;
  schoolFieldsFlag: boolean = false;
  hierarchyFlag: boolean = false;
  preferenceFlag: boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
    this.showPage(this.pageId);
  }

  getSelectedPage(event){
    this.showPage(event);
  }

  showPage(pageId = 'Period') {
    this.pageTitle = pageId;
    if (pageId === 'Period') {
      this.displayPeriod = true;
      this.periodFlag = true;
    } else {
      this.displayPeriod = false;
      this.periodFlag = false;
    }
    if (pageId === 'Grade Levels') {
      this.displayGradeLevels = true;
      this.gradeLevelsFlag = true;
    } else {
      this.displayGradeLevels = false;
      this.gradeLevelsFlag = false;
    }
    if (pageId === 'Sections') {
      this.displaySections = true;
      this.sectionsFlag = true;
    } else {
      this.displaySections = false;
      this.sectionsFlag = false;
    }
    if (pageId === 'Rooms') {
      this.displayRooms = true;
      this.roomsFlag = true;
    } else {
      this.displayRooms = false;
      this.roomsFlag = false;
    }
    if (pageId === 'School Fields') {
      this.displaySchoolFields = true;
      this.schoolFieldsFlag = true;
    } else {
      this.displaySchoolFields = false;
      this.schoolFieldsFlag = false;
    }
    if (pageId === 'Hierarchy') {
      this.displayHierarchy = true;
      this.hierarchyFlag = true;
    } else {
      this.displayHierarchy = false;
      this.hierarchyFlag = false;
    }
    if (pageId === 'Preference') {
      this.displayPreference = true;
      this.preferenceFlag = true;
    } else {
      this.displayPreference = false;
      this.preferenceFlag = false;
    }
  }

}
