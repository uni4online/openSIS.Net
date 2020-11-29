import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';

@Component({
  selector: 'vex-staff-addressinfo',
  templateUrl: './staff-addressinfo.component.html',
  styleUrls: ['./staff-addressinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StaffAddressinfoComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
