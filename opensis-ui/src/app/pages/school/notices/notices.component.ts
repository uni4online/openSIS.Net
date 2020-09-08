import { Component, OnInit } from '@angular/core';
import icArrowDropDown from '@iconify/icons-ic/arrow-drop-down';
import icAdd from '@iconify/icons-ic/add';
import { MatDialog } from '@angular/material/dialog';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import { EditNoticeComponent } from '../notices/edit-notice/edit-notice.component';

@Component({
  selector: 'vex-notices',
  templateUrl: './notices.component.html',
  styleUrls: ['./notices.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class NoticesComponent implements OnInit {

  icPreview = icArrowDropDown;
  icAdd = icAdd;

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openAddNew() {
    this.dialog.open(EditNoticeComponent, {
      data: null,
      width: '800px'
    });
  }

}
