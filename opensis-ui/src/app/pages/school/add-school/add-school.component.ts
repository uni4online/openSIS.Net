import { Component, OnInit,ViewChildren, AfterViewInit, QueryList } from '@angular/core';

import {fadeInRight400ms} from '../../../../@vex/animations/fade-in-right.animation';

@Component({
  selector: 'vex-add-school',
  templateUrl: './add-school.component.html',
  styleUrls: ['./add-school.component.scss'],
  animations: [
  
    fadeInRight400ms
    
  ]
})
export class AddSchoolComponent implements AfterViewInit {
  displayGeneral = true;
  displayWash = false;
  generalFlag: boolean = true;
  washFlag: boolean = false;
  constructor() { 
    
  }

  ngAfterViewInit() { }
  
  showGeneral(){
    this.displayGeneral = true;
    this.displayWash = false;
    this.generalFlag=true;
    this.washFlag=false;
  }
  showWash(){
    this.displayWash = true;
    this.displayGeneral = false;
    this.generalFlag=false;
    this.washFlag=true;
   

  }

}
