import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';

export const dateTimeCustomFormats = {
};

@Component({
  selector: 'vex-edit-period',
  templateUrl: './edit-period.component.html',
  styleUrls: ['./edit-period.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditPeriodComponent implements OnInit {
  icClose = icClose;
  public dateTime2: Date;
  public dateTime1: Date;
  constructor(private dialogRef: MatDialogRef<EditPeriodComponent>, public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

}
