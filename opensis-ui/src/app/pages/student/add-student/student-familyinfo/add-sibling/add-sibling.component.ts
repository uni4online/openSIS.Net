import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-add-sibling',
  templateUrl: './add-sibling.component.html',
  styleUrls: ['./add-sibling.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class AddSiblingComponent implements OnInit {

  icClose = icClose;
  form: FormGroup;
  constructor(private dialogRef: MatDialogRef<AddSiblingComponent>, private fb: FormBuilder) { }

  ngOnInit(): void {
  }

}
