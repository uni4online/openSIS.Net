import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-viewstudent-generalinfo',
  templateUrl: './viewstudent-generalinfo.component.html',
  styleUrls: ['./viewstudent-generalinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ],
})
export class ViewstudentGeneralinfoComponent implements OnInit {

  constructor(public translateService:TranslateService) { }

  ngOnInit(): void {
  }

}
