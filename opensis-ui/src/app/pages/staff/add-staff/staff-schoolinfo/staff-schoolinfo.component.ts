import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icAdd from '@iconify/icons-ic/baseline-add';
import icClear from '@iconify/icons-ic/baseline-clear';

@Component({
  selector: 'vex-staff-schoolinfo',
  templateUrl: './staff-schoolinfo.component.html',
  styleUrls: ['./staff-schoolinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StaffSchoolinfoComponent implements OnInit {

  icAdd = icAdd;
  icClear = icClear;


  constructor(private fb: FormBuilder, public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
    
  }

}
