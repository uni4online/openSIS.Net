import { Component, OnInit} from '@angular/core';
import icArrowDropDown from '@iconify/icons-ic/arrow-drop-down';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icCheckBox from '@iconify/icons-ic/check-box';
import icCheckBoxOutlineBlank from '@iconify/icons-ic/check-box-outline-blank';
import icMoreVert from '@iconify/icons-ic/more-vert';
import icMenu from '@iconify/icons-ic/menu';
import icAdd from '@iconify/icons-ic/add';
import icClose from '@iconify/icons-ic/close';
import { MatDialog } from '@angular/material/dialog';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import { EditMarkingPeriodComponent } from '../marking-periods/edit-marking-period/edit-marking-period.component';

@Component({
  selector: 'vex-marking-periods',
  templateUrl: './marking-periods.component.html',
  styleUrls: ['./marking-periods.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class MarkingPeriodsComponent implements OnInit {

  icArrowDropDown = icArrowDropDown;
  icEdit = icEdit;
  icCheckBox = icCheckBox;
  icCheckBoxOutlineBlank = icCheckBoxOutlineBlank;
  icMoreVert = icMoreVert;
  icMenu = icMenu;
  icAdd = icAdd;
  icClose = icClose;
  
  menuOpen = false;

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openAddNew() {
    this.dialog.open(EditMarkingPeriodComponent, {
      data: null,
      width: '600px'
    });
  }

  setData() {
    this.menuOpen = false;
  }

  openMenu() {
    this.menuOpen = true;
  }

}
