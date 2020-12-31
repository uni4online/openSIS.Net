import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-edit-grade-scale',
  templateUrl: './edit-grade-scale.component.html',
  styleUrls: ['./edit-grade-scale.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditGradeScaleComponent implements OnInit {
  icClose = icClose;

  constructor(private dialogRef: MatDialogRef<EditGradeScaleComponent>, public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

}
