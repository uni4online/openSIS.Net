import { LovAddView } from './../../../../models/lovModel';
import { CommonService } from './../../../../services/common.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component,Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'vex-edit-male-toilet-type',
  templateUrl: './edit-male-toilet-type.component.html',
  styleUrls: ['./edit-male-toilet-type.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditMaleToiletTypeComponent implements OnInit {
  icClose = icClose;
  form:FormGroup;
  maleToiletTypeTitle: string;
  lovAddView:LovAddView=new LovAddView();
  buttonType:string;

  constructor(
    private dialogRef: MatDialogRef<EditMaleToiletTypeComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private snackbar:MatSnackBar,
    private commonService:CommonService,
    fb:FormBuilder
    ) { 
      this.form=fb.group({
        id:[0],
        lovName:["Male Toilet Type"],
        lovColumnValue:['',[Validators.required]],
      })
      if(data==null){
        this.maleToiletTypeTitle="addMaleToiletType";
        this.buttonType="submit";
      }  
      else{
        
        this.lovAddView.dropdownValue=data;
        this.maleToiletTypeTitle="editMaleToiletType";
        this.buttonType="update";
        this.form.controls.id.patchValue(data.id)
        this.form.controls.lovColumnValue.patchValue(data.lovColumnValue)
        this.form.controls.lovName.patchValue(data.lovName)
      }
    }

  ngOnInit(): void {
  }
  submit(){
    
    if (this.form.valid) { 
      if(this.form.controls.id.value==0){
        this.lovAddView.dropdownValue.lovColumnValue=this.form.controls.lovColumnValue.value;
        this.lovAddView.dropdownValue.lovName=this.form.controls.lovName.value;
        this.commonService.addDropdownValue(this.lovAddView).subscribe(
          (res:LovAddView)=>{
            if(typeof(res)=='undefined'){
              this.snackbar.open('Male Toilet Type insertion failed. ' + sessionStorage.getItem("httpError"), '', {
                duration: 10000
              });
            }
            else{
              if (res._failure) {
                this.snackbar.open('Male Toilet Type insertion failed. ' + res._message, 'LOL THANKS', {
                  duration: 10000
                });
              } 
              else { 
                this.snackbar.open('Male Toilet Type inserted Successfully.' + res._message, 'LOL THANKS', {
                  duration: 10000
                });
                this.dialogRef.close('submited');
              }
            }
          }
        );
      }
      else{
        this.lovAddView.dropdownValue.id=this.form.controls.id.value;
        this.lovAddView.dropdownValue.lovName=this.form.controls.lovName.value;
        this.lovAddView.dropdownValue.lovColumnValue=this.form.controls.lovColumnValue.value;

        this.commonService.updateDropdownValue(this.lovAddView).subscribe(
          (res:LovAddView)=>{
            if(typeof(res)=='undefined'){
              this.snackbar.open('Male Toilet Type Update failed. ' + sessionStorage.getItem("httpError"), '', {
                duration: 10000
              });
            }
            else{
             
              if (res._failure) {
                this.snackbar.open('Male Toilet Type Update failed. ' + res._message, 'LOL THANKS', {
                  duration: 10000
                });
              } 
              else { 
               
                this.snackbar.open('Male Toilet Type Updated Successfully.' + res._message, 'LOL THANKS', {
                  duration: 10000
                });
                this.dialogRef.close('submited');
              }
            }
          }
        );
        
      }
    }
  }

}
