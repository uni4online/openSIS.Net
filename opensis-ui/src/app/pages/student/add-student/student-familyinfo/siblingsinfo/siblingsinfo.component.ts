import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/baseline-add';
import { MatDialog } from '@angular/material/dialog';
import { AddSiblingComponent } from '../add-sibling/add-sibling.component';
import { ViewSiblingComponent } from '../view-sibling/view-sibling.component';

@Component({
  selector: 'vex-siblingsinfo',
  templateUrl: './siblingsinfo.component.html',
  styleUrls: ['./siblingsinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class SiblingsinfoComponent implements OnInit {

  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;

  constructor(private fb: FormBuilder, private dialog: MatDialog,
    public translateService:TranslateService) { }

  ngOnInit(): void {

  }

  openAddNew() {
    this.dialog.open(AddSiblingComponent, {
      width: '600px'
    });
  }

  openViewDetails() {
    this.dialog.open(ViewSiblingComponent, {
      width: '600px'
    });
  }

}
