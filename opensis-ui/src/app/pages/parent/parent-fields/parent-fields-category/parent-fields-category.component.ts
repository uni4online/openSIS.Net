import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-parent-fields-category',
  templateUrl: './parent-fields-category.component.html',
  styleUrls: ['./parent-fields-category.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class ParentFieldsCategoryComponent implements OnInit {
  icClose = icClose;
  form: FormGroup;

  constructor(private dialogRef: MatDialogRef<ParentFieldsCategoryComponent>, private fb: FormBuilder) { }

  ngOnInit(): void {
  }

}
