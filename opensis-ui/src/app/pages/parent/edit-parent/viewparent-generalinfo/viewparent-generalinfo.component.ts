import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/edit';
import icAdd from '@iconify/icons-ic/add';
import icDelete from '@iconify/icons-ic/delete';
import icRemove from '@iconify/icons-ic/remove-circle';

@Component({
  selector: 'vex-viewparent-generalinfo',
  templateUrl: './viewparent-generalinfo.component.html',
  styleUrls: ['./viewparent-generalinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class ViewparentGeneralinfoComponent implements OnInit {
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icRemove = icRemove;

  constructor(public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

  openViewDetails() {
    
  }

}
