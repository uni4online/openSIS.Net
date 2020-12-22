import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-view-sibling',
  templateUrl: './view-sibling.component.html',
  styleUrls: ['./view-sibling.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class ViewSiblingComponent implements OnInit {
  icClose = icClose;
  address:string="";
  schoolName;
  constructor(private dialogRef: MatDialogRef<ViewSiblingComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any) {}


  ngOnInit(): void { 
    if(this.data.flag === "Parent"){
      this.schoolName = this.data.siblingDetails.schoolName;
      this.address = this.data.siblingDetails.address;
    }else{
      this.schoolName = this.data.siblingDetails.schoolMaster.schoolName;
      this.address=this.data.siblingDetails.homeAddressLineOne
                  +","+this.data.siblingDetails.homeAddressCity
                  +","+this.data.siblingDetails.homeAddressState
                  +","+this.data.siblingDetails.homeAddressZip+" "
    } 
    
  }

}
