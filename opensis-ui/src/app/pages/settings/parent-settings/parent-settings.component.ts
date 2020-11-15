import { Component, OnInit } from '@angular/core';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';

@Component({
  selector: 'vex-parent-settings',
  templateUrl: './parent-settings.component.html',
  styleUrls: ['./parent-settings.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})
export class ParentSettingsComponent implements OnInit {
  pages=['Parent Fields']
  parentSettings=true;
  pageTitle = 'Parent Fields';
  pageId: string = '';
  displayParentFields = true;

  ParentFieldsFlag: boolean = true;

  constructor() { }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
    this.showPage(this.pageId);
  }

  getSelectedPage(event){
    this.showPage(event);
  }

  showPage(pageId = 'Parent Fields') {
    this.pageTitle = pageId;
    if (pageId === 'Parent Fields') {
      this.displayParentFields = true;
      this.ParentFieldsFlag = true;
    } else {
      this.displayParentFields = false;
      this.ParentFieldsFlag = false;
    }
  }

}
