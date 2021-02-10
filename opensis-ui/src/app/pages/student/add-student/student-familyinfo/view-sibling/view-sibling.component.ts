import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';
import { ParentInfoService } from '../../../../../services/parent-info.service';
import { AddParentInfoModel } from '../../../../../models/parentInfoModel';
import { MatSnackBar } from '@angular/material/snack-bar';
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
  address:string;
  schoolName;
  getStudentForView=[];
  gradeLevel;
  mapUrl:string;
  addParentInfoModel: AddParentInfoModel = new AddParentInfoModel(); 
  constructor(private dialogRef: MatDialogRef<ViewSiblingComponent>,
    private snackbar: MatSnackBar,
    private parentInfoService:ParentInfoService,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any) {}


  ngOnInit(): void { 
    if(this.data.flag === "Parent"){
      this.addParentInfoModel.parentInfo.parentId = this.data.siblingDetails.parentId;     
      this.parentInfoService.viewParentInfo(this.addParentInfoModel).subscribe( (res) => {
        this.getStudentForView = res.getStudentForView;
        this.getStudentForView.map(val=>{          
          if(val.studentId === this.data.studentDetails.studentId){
            this.data.siblingDetails=val
          }
        })   
        
        this.schoolName =this.data.siblingDetails.schoolName;
        if(this.data.siblingDetails.address !== ""){
          this.address = this.data.siblingDetails.address;
        }else{
          this.address = null;
        }      
        this.gradeLevel = this.data.siblingDetails.gradeLevelTitle;      
      })  
      
    }else{
      this.schoolName = this.data.siblingDetails.schoolMaster.schoolName;
      if(this.data.siblingDetails.homeAddressLineOne !== null){
        this.address=
        this.data.siblingDetails.homeAddressLineOne === null?null:this.data.siblingDetails.homeAddressLineOne
        +","+this.data.siblingDetails.homeAddressCity === null?null:this.data.siblingDetails.homeAddressCity
                 +","+this.data.siblingDetails.homeAddressState === null?null:this.data.siblingDetails.homeAddressState
                 +","+this.data.siblingDetails.homeAddressZip+"  ";
      }else{
        this.address = null;
      }
     
      
    } 

    
    
  }

  showOnGoogleMap(){    
    if(this.address !== null){
      let address = this.address;
      this.mapUrl=`https://maps.google.com/?q=${address}`
      window.open(this.mapUrl,'_blank');
    }else{
      this.snackbar.open('Invalid  Address', 'Ok', {
        duration: 5000
      });
    }
  }

}
