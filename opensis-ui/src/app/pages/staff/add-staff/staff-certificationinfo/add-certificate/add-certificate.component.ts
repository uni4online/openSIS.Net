

import { StaffService } from '../../../../../services/staff.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import  icWarning  from '@iconify/icons-ic/warning';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';
import { MatSnackBar } from '@angular/material/snack-bar';
import {WashInfoEnum} from '../../../../../enums/wash-info.enum'
import { TranslateService } from '@ngx-translate/core';
import { StaffCertificateModel } from '../../../../../models/staffModel';

@Component({
  selector: 'vex-add-certificate',
  templateUrl: './add-certificate.component.html',
  styleUrls: ['./add-certificate.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class AddCertificateComponent implements OnInit {
  icClose = icClose;
  icWarning= icWarning;
  form:FormGroup;
  staffCertificateTitle: string;
  primaryCertificationStatusEnum=Object.keys(WashInfoEnum)
  staffCertificateModel:StaffCertificateModel= new StaffCertificateModel();
  washinfo = WashInfoEnum;
  formvalidstatas: boolean=true;
  buttonType: string;
  constructor
    (
      private dialogRef: MatDialogRef<AddCertificateComponent>, 
      @Inject(MAT_DIALOG_DATA) public data:any,
      private fb: FormBuilder,
      private snackbar:MatSnackBar,
      private staffService:StaffService
    ) 
    {
      this.form=fb.group({
        id: [0],
        certificationName:[],
        shortName:[],
        certificationCode:[],
        primaryCertification:[],
        certificationDate:[],
        certificationExpiryDate:[],
        certificationDescription:[]
      });
      if(data==null){
        this.staffCertificateTitle="addNewCertificate";
        this.buttonType="submit";
      }
      else{
        this.staffCertificateTitle="editCertificate";
        this.buttonType="update";
        this.form.controls.id.patchValue(data.id);
        this.form.controls.certificationName.patchValue(data.certificationName);
        this.form.controls.shortName.patchValue(data.shortName);
        this.form.controls.certificationCode.patchValue(data.certificationCode);
        this.form.controls.primaryCertification.patchValue(data.primaryCertification);
        this.form.controls.certificationDate.patchValue(data.certificationDate);
        this.form.controls.certificationExpiryDate.patchValue(data.certificationExpiryDate);
        this.form.controls.certificationDescription.patchValue(data.certificationDescription);
      }
    }

  ngOnInit(): void {
    
  }
  submit(){
    
    if (this.form.valid) {
      if(
        (this.form.controls.certificationCode.value!=null) && 
        (this.form.controls.certificationDate.value!=null) &&
        (this.form.controls.certificationDescription.value!=null) &&
        (this.form.controls.certificationExpiryDate.value!=null) &&
        (this.form.controls.certificationName.value!=null) &&
        (this.form.controls.primaryCertification.value!=null) &&
        (this.form.controls.shortName.value!=null)
        ){
          if(this.form.controls.id.value==0){
            this.staffCertificateModel.staffCertificateInfo.staffId=this.staffService.getStaffId();
            this.staffCertificateModel.staffCertificateInfo.certificationName=this.form.controls.certificationName.value;
            this.staffCertificateModel.staffCertificateInfo.shortName=this.form.controls.shortName.value;
            this.staffCertificateModel.staffCertificateInfo.certificationCode=this.form.controls.certificationCode.value;
            this.staffCertificateModel.staffCertificateInfo.primaryCertification=this.form.controls.primaryCertification.value;
            this.staffCertificateModel.staffCertificateInfo.certificationDate=this.form.controls.certificationDate.value;
            this.staffCertificateModel.staffCertificateInfo.certificationExpiryDate=this.form.controls.certificationExpiryDate.value;
            this.staffCertificateModel.staffCertificateInfo.certificationDescription=this.form.controls.certificationDescription.value;
            console.log(this.form.value)
            console.log(this.staffCertificateModel)
            this.staffService.addStaffCertificateInfo(this.staffCertificateModel).subscribe(
              (res:StaffCertificateModel)=>{
                if(typeof(res)=='undefined'){
                  this.snackbar.open('Staff Certificate insertion failed. ' + sessionStorage.getItem("httpError"), '', {
                    duration: 10000
                  });
                }
                else{
                  if (res._failure) {
                    this.snackbar.open('Staff Certificate insertion failed. ' + res._message, 'LOL THANKS', {
                      duration: 10000
                    });
                  } 
                  else { 
                    this.snackbar.open('Staff Certificate inserted Successfully. ' + res._message, 'LOL THANKS', {
                      duration: 10000
                    });
                    this.dialogRef.close('submited');
                  }
                }
              }
            );
              
          }
          else{
            this.staffCertificateModel.staffCertificateInfo.staffId=this.staffService.getStaffId();
            this.staffCertificateModel.staffCertificateInfo.id=this.form.controls.id.value;
            this.staffCertificateModel.staffCertificateInfo.certificationName=this.form.controls.certificationName.value;
            this.staffCertificateModel.staffCertificateInfo.shortName=this.form.controls.shortName.value;
            this.staffCertificateModel.staffCertificateInfo.certificationCode=this.form.controls.certificationCode.value;
            this.staffCertificateModel.staffCertificateInfo.primaryCertification=this.form.controls.primaryCertification.value;
            this.staffCertificateModel.staffCertificateInfo.certificationDate=this.form.controls.certificationDate.value;
            this.staffCertificateModel.staffCertificateInfo.certificationExpiryDate=this.form.controls.certificationExpiryDate.value;
            this.staffCertificateModel.staffCertificateInfo.certificationDescription=this.form.controls.certificationDescription.value;
            this.staffService.updateStaffCertificateInfo(this.staffCertificateModel).subscribe(
              (res:StaffCertificateModel)=>{
                if(typeof(res)=='undefined'){
                  this.snackbar.open('Staff Certificate update failed. ' + sessionStorage.getItem("httpError"), '', {
                    duration: 10000
                  });
                }
                else{
                  if (res._failure) {
                    this.snackbar.open('Staff Certificate update failed. ' + res._message, 'LOL THANKS', {
                      duration: 10000
                    });
                  } 
                  else { 
                    this.snackbar.open('Staff Certificate updateed Successfully. ' + res._message, 'LOL THANKS', {
                      duration: 10000
                    });
                    this.dialogRef.close('submited');
                  }
                }
              }
            )
          }
      }
      else{
        this.formvalidstatas=false;
      }
      
    }
  }
  cancel(){
    this.dialogRef.close();
  }

}
