import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-edit-report-card-grade',
  templateUrl: './edit-report-card-grade.component.html',
  styleUrls: ['./edit-report-card-grade.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditReportCardGradeComponent implements OnInit {
  icClose = icClose;

  constructor(private dialogRef: MatDialogRef<EditReportCardGradeComponent>, public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

}
