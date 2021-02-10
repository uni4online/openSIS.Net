import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-view-details',
  templateUrl: './view-details.component.html',
  styleUrls: ['./view-details.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class ViewDetailsComponent implements OnInit {

  icClose = icClose;
  viewDetails;
  constructor(private dialogRef: MatDialogRef<ViewDetailsComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,) {
    this.viewDetails=this.data.details;
   }

  ngOnInit(): void {
    
  }

}
