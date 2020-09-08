import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-edit-marking-period',
  templateUrl: './edit-marking-period.component.html',
  styleUrls: ['./edit-marking-period.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditMarkingPeriodComponent implements OnInit {

  icClose = icClose;

  constructor(private dialogRef: MatDialogRef<EditMarkingPeriodComponent>, private fb: FormBuilder) { }

  ngOnInit(): void {
  }

}
