import { Component, Input, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { StaffAddModel } from '../../../../models/staffModel';
import icCheckBox from '@iconify/icons-ic/check-box';
import icCheckBoxOutlineBlank from '@iconify/icons-ic/check-box-outline-blank';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-view-staff-addressinfo',
  templateUrl: './view-staff-addressinfo.component.html',
  styleUrls: ['./view-staff-addressinfo.component.scss'],
  animations: [
    stagger60ms
  ]
})


export class ViewStaffAddressinfoComponent implements OnInit {
  @Input() staffCreateMode
  @Input() categoryId
  @Input() staffViewDetails:StaffAddModel
  @Input() nameOfMiscValues
  module="Staff"
  icCheckBox = icCheckBox;
  icCheckBoxOutlineBlank = icCheckBoxOutlineBlank;
  constructor(public translateService: TranslateService) { 
    translateService.use('en');
  }

  ngOnInit(): void {
  }

}
