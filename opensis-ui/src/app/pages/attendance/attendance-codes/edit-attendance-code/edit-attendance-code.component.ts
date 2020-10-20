import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-edit-attendance-code',
  templateUrl: './edit-attendance-code.component.html',
  styleUrls: ['./edit-attendance-code.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditAttendanceCodeComponent implements OnInit {
  icClose = icClose;
  form: FormGroup;

  constructor(private dialogRef: MatDialogRef<EditAttendanceCodeComponent>, private fb: FormBuilder) { }

  ngOnInit(): void {
  }

}
