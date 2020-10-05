import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { GetAllGradeLevelsModel,AddGradeLevelModel } from '../../../../models/gradeLevelModel';
import { GradeLevelService } from '../../../../services/grade-level.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'vex-edit-grade-levels',
  templateUrl: './edit-grade-levels.component.html',
  styleUrls: ['./edit-grade-levels.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditGradeLevelsComponent implements OnInit {
  icClose = icClose;
  form: FormGroup;
  addGradeLevel:AddGradeLevelModel = new AddGradeLevelModel();
  updateGradeLevel:AddGradeLevelModel = new AddGradeLevelModel();
  nextGradeLevelList:[];
  editMode:boolean;
  editDetails;
  nextGradeIdInString:string;
  constructor(private dialogRef: MatDialogRef<EditGradeLevelsComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
     private fb: FormBuilder,
     private gradeLevelService:GradeLevelService,
     private snackbar: MatSnackBar) {
     if(data.editMode){
      this.editMode = data.editMode;
      this.editDetails = data.editDetails;
      this.nextGradeLevelList=data.gradeLevels;
     }else{
       this.nextGradeLevelList=data.gradeLevels;
     }
     this.form = this.fb.group(
      {
        title: ['',Validators.required],
        shortName: ['',Validators.required],
        sortOrder: ['',Validators.required],
        nextGradeId: ['null',Validators.required],
      });
    }

  ngOnInit(): void {
    if(this.editMode){
      this.callGradeLevelView();
    }
  }

  callGradeLevelView(){
      if(this.editDetails.nextGradeId!=null){
      this.nextGradeIdInString = this.editDetails.nextGradeId.toString();
        }else{
          this.nextGradeIdInString="null";
        }
      this.form.patchValue({
        title:this.editDetails.title,
        shortName:this.editDetails.shortName,
        sortOrder:this.editDetails.sortOrder,
        nextGradeId:this.nextGradeIdInString
      })
   
  }

  submit(){
    if(this.editMode){
      this.UpdateGradeLevel();
    }else{
      if(this.form.valid){
        this.AddGradeLevel();
      }
    }
  }

  AddGradeLevel(){
    this.addGradeLevel.tblGradelevel.schoolId=+sessionStorage.getItem("selectedId");;
    this.addGradeLevel.tblGradelevel.title = this.form.value.title;
    if(this.form.value.nextGradeId=="null"){
      this.addGradeLevel.tblGradelevel.nextGradeId = null;
    }else{
      this.addGradeLevel.tblGradelevel.nextGradeId = +this.form.value.nextGradeId;
    }
    this.addGradeLevel.tblGradelevel.shortName =this.form.value.shortName;
    this.addGradeLevel.tblGradelevel.sortOrder=this.form.value.sortOrder;
    this.addGradeLevel._token=sessionStorage.getItem("token");
    this.addGradeLevel._tenantName=sessionStorage.getItem("tenant");
    this.gradeLevelService.AddGradeLevel(this.addGradeLevel).subscribe((res)=>{
      if (res._failure) {
        this.snackbar.open('Failed to Add Grade Level ' + res._message, 'LOL THANKS', {
          duration: 10000
        });
      } else {
        this.snackbar.open('Grade Level Added Successfully.', '', {
          duration: 10000
        });
        this.dialogRef.close()
        this.gradeLevelService.updateGradeLevelTable(true);
      }
    })
  }

  UpdateGradeLevel(){
    this.updateGradeLevel.tblGradelevel.schoolId = this.editDetails.schoolId;
    this.updateGradeLevel.tblGradelevel.gradeId = this.editDetails.gradeId;
    this.updateGradeLevel._tenantName=sessionStorage.getItem("tenant");
    this.updateGradeLevel._token=sessionStorage.getItem("token");
    this.updateGradeLevel.tblGradelevel.title=this.form.value.title;
    this.updateGradeLevel.tblGradelevel.shortName=this.form.value.shortName;
    this.updateGradeLevel.tblGradelevel.sortOrder=this.form.value.sortOrder;
    if(this.form.value.nextGradeId=="null"){
      this.updateGradeLevel.tblGradelevel.nextGradeId = null;
    }else{
      this.updateGradeLevel.tblGradelevel.nextGradeId=+this.form.value.nextGradeId;
    }
    this.gradeLevelService.UpdateGradeLevel(this.updateGradeLevel).subscribe((res)=>{
      if (res._failure) {
        this.snackbar.open('Failed to Update Grade Level ' + res._message, 'LOL THANKS', {
          duration: 10000
        });
      } else {
        this.snackbar.open('Grade Level Updated Successfully.', '', {
          duration: 10000
        }
        );
        this.dialogRef.close();
        this.gradeLevelService.updateGradeLevelTable(true);
      }
    })
  }

}
