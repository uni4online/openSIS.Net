import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/edit';
import icCheckbox from '@iconify/icons-ic/baseline-check-box';
import icCheckboxOutline from '@iconify/icons-ic/baseline-check-box-outline-blank';
import icPromoted from '@iconify/icons-ic/baseline-north';
import icExternal from '@iconify/icons-ic/baseline-undo';
import icRetained from '@iconify/icons-ic/baseline-replay';
import icHomeSchool from '@iconify/icons-ic/baseline-home';
import icExpand from '@iconify/icons-ic/baseline-arrow-drop-up';
import icTrasnferIn from '@iconify/icons-ic/baseline-call-received';
import icTrasnferOut from '@iconify/icons-ic/baseline-call-made';

@Component({
  selector: 'vex-viewstudent-enrollmentinfo',
  templateUrl: './viewstudent-enrollmentinfo.component.html',
  styleUrls: ['./viewstudent-enrollmentinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class ViewstudentEnrollmentinfoComponent implements OnInit {

  icAdd = icAdd;
  icEdit = icEdit;
  icCheckbox = icCheckbox;
  icCheckboxOutline = icCheckboxOutline;
  icPromoted = icPromoted;
  icExternal = icExternal;
  icHomeSchool = icHomeSchool;
  icExpand = icExpand;
  icRetained = icRetained;
  icTrasnferIn = icTrasnferIn;
  icTrasnferOut = icTrasnferOut;

  constructor() { }

  ngOnInit(): void {
  }

}
