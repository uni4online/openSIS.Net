import { Component, Inject, OnInit,AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import icClose from '@iconify/icons-ic/twotone-close';
import { HonorRollAddViewModel } from '../../../../models/grades.model';
import { ValidationService } from '../../../shared/validation.service';
import { GradesService } from '../../../../services/grades.service';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-edit-honor-roll',
  templateUrl: './edit-honor-roll.component.html',
  styleUrls: ['./edit-honor-roll.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditHonorRollComponent implements OnInit,AfterViewInit {
  icClose = icClose;
  form:FormGroup
  editmod;
  honorRollAddViewModel:HonorRollAddViewModel=new HonorRollAddViewModel()
  honorRollTitle: string;
  buttonType: string;

  constructor(
    private dialogRef: MatDialogRef<EditHonorRollComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private snackbar:MatSnackBar,
    private gradesService:GradesService,
    fb:FormBuilder
    ) { 
      this.editmod=data.mod
      this.form= fb.group({
        honorRollId:[0],
        honorRoll:['',[ValidationService.noWhitespaceValidator]],
        breakoff:['',[ValidationService.noWhitespaceValidator,Validators.min(0)]]
      })
    }

  ngOnInit(): void {
    if(this.editmod===1){
      this.honorRollTitle="editHonorRoll";
      this.buttonType="update";
      this.patchDataToForm();
    }
    else{
      this.honorRollTitle="addNewHonorRoll";
      this.buttonType="submit";
    }
  }
  ngAfterViewInit(){

  }
  patchDataToForm(){
    this.form.controls.honorRollId.patchValue(this.data.element.honorRollId)
    this.form.controls.honorRoll.patchValue(this.data.element.honorRoll)
    this.form.controls.breakoff.patchValue(this.data.element.breakoff)
  }
  submit(){
    this.form.markAllAsTouched();
    if(this.form.valid){
      if(this.editmod==0){
        this.addHonorRoll()
      }
      else{
        this.editHonorRoll()
      }
    }
   
  }
  addHonorRoll(){
    this.honorRollAddViewModel.honorRolls.honorRoll=this.form.get("honorRoll").value;
    this.honorRollAddViewModel.honorRolls.breakoff=this.form.get("breakoff").value;
    this.gradesService.addHonorRoll(this.honorRollAddViewModel).subscribe(
      (res)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }else{
          if (res._failure) {
            this.snackbar.open('' + res._message, '', {
              duration: 10000
            });
          } 
          else{
            this.snackbar.open('' + res._message, '', {
              duration: 10000
            });
            this.dialogRef.close('submited');
          }
        }
      }
    );
  }
  editHonorRoll(){
    this.honorRollAddViewModel.honorRolls.honorRollId=this.form.get("honorRollId").value;
    this.honorRollAddViewModel.honorRolls.honorRoll=this.form.get("honorRoll").value;
    this.honorRollAddViewModel.honorRolls.breakoff=this.form.get("breakoff").value;
    this.gradesService.updateHonorRoll(this.honorRollAddViewModel).subscribe(
      (res)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open(sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }else{
          if (res._failure) {
            this.snackbar.open( res._message, '', {
              duration: 10000
            });
          } 
          else{
            this.snackbar.open( res._message, '', {
              duration: 10000
            });
            this.dialogRef.close('submited');
          }
        }
      }
    );
  }

}
