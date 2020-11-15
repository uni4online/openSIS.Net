import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';

@Component({
  selector: 'vex-viewstudent-medicalinfo',
  templateUrl: './viewstudent-medicalinfo.component.html',
  styleUrls: ['./viewstudent-medicalinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class ViewstudentMedicalinfoComponent implements OnInit {

  icEdit = icEdit;

  constructor(public translateService:TranslateService) { }

  ngOnInit(): void {
  }

}
