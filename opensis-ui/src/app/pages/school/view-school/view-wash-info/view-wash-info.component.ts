import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';

@Component({
  selector: 'vex-view-wash-info',
  templateUrl: './view-wash-info.component.html',
  styleUrls: ['./view-wash-info.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class ViewWashInfoComponent implements OnInit {

  icEdit = icEdit;

  constructor() { }

  ngOnInit(): void {
  }

}
