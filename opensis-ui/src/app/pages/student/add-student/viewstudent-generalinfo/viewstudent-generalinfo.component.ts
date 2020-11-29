import { Component, OnInit,Input } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/edit';
import icCheckbox from '@iconify/icons-ic/baseline-check-box';
import icCheckboxOutline from '@iconify/icons-ic/baseline-check-box-outline-blank';

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
  @Input() data;
  icEdit = icEdit;
  icCheckbox = icCheckbox;
  icCheckboxOutline = icCheckboxOutline;
  
  constructor(public translateService:TranslateService) {
    translateService.use('en');
   }

  ngOnInit(): void {
   
  }

}
