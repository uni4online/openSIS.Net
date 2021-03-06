import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/edit';
import icDelete from '@iconify/icons-ic/delete';
import icCheckBox from '@iconify/icons-ic/check-box';
import icCheckCircle from '@iconify/icons-ic/twotone-done';
import icCheckBoxOutlineBlank from '@iconify/icons-ic/check-box-outline-blank';

@Component({
  selector: 'vex-viewparent-addressinfo',
  templateUrl: './viewparent-addressinfo.component.html',
  styleUrls: ['./viewparent-addressinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class ViewparentAddressinfoComponent implements OnInit {
  icEdit = icEdit;
  icDelete = icDelete;
  icCheckBox = icCheckBox;
  icCheckBoxOutlineBlank = icCheckBoxOutlineBlank;
  icCheckCircle = icCheckCircle;

  constructor(public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

}
