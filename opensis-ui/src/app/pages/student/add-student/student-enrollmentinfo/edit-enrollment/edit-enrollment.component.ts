import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-edit-enrollment',
  templateUrl: './edit-enrollment.component.html',
  styleUrls: ['./edit-enrollment.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ],
})
export class EditEnrollmentComponent implements OnInit {

  icClose = icClose;

  constructor(private dialogRef: MatDialogRef<EditEnrollmentComponent>) { }

  ngOnInit(): void {
  }

}
