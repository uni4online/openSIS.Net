import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-edit-parent-fields',
  templateUrl: './edit-parent-fields.component.html',
  styleUrls: ['./edit-parent-fields.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditParentFieldsComponent implements OnInit {
  icClose = icClose;
  form: FormGroup;

  constructor(private dialogRef: MatDialogRef<EditParentFieldsComponent>, private fb: FormBuilder) { }

  ngOnInit(): void {
  }

}
