import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';

@Component({
  selector: 'vex-view-school',
  templateUrl: './view-school.component.html',
  styleUrls: ['./view-school.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class ViewSchoolComponent implements OnInit {

  icEdit = icEdit;

  constructor() { }

  ngOnInit(): void {
  }

}
