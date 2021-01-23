import { Component, OnInit } from '@angular/core';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';

@Component({
  selector: 'vex-grade-settings',
  templateUrl: './grade-settings.component.html',
  styleUrls: ['./grade-settings.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})
export class GradeSettingsComponent implements OnInit {
  pages=['Standard Grade Setup', 'Effort Grade Setup', 'Report Card Grades', 'Honor Roll Setup']
  parentSettings=true;
  pageTitle = 'US Common Core Standards';
  pageId: string = '';

  constructor() { }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
  }

  getSelectedPage(pageId){
    this.pageId = pageId;
    localStorage.setItem("pageId", pageId);
  }

}
