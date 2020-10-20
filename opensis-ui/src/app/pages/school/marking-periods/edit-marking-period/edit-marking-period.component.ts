import { Component, OnInit,Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { MarkingPeriodAddModel,SemesterAddModel ,QuarterAddModel,ProgressPeriodAddModel} from '../../../../models/markingPeriodModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MarkingPeriodService } from '../../../../services/marking-period.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MY_FORMATS } from '../../../shared/format-datepicker';
import * as _moment from 'moment';
import { default as _rollupMoment } from 'moment';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { SharedFunction } from '../../../shared/shared-function';
import { ValidationService } from '../../../shared/validation.service';
const moment = _moment;
@Component({
  selector: 'vex-edit-marking-period',
  templateUrl: './edit-marking-period.component.html',
  styleUrls: ['./edit-marking-period.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ]
})
export class EditMarkingPeriodComponent implements OnInit {

  icClose = icClose;
  markingPeriodLevel;
  form: FormGroup;
  isEdit;
  doesGrades=false;
  markingPeriodAddModel: MarkingPeriodAddModel = new MarkingPeriodAddModel();
  semesterAddModel:SemesterAddModel=new SemesterAddModel();
  quarterAddModel:QuarterAddModel=new QuarterAddModel();
  progressPeriodAddModel:ProgressPeriodAddModel=new ProgressPeriodAddModel();
  parentStartDate;
  parentEndDate;
  obj = {
    "doesComments": false,
    "doesExam": false,
    "doesGrades": false, 
    "endDate": "",
    "postEndDate": "",
    "postStartDate": "",   
    "shortName": "",
    "startDate": "",
    "title": ""
  }
  constructor(private dialogRef: MatDialogRef<EditMarkingPeriodComponent>,
     private fb: FormBuilder,
     private markingPeriodService:MarkingPeriodService,
     private snackbar: MatSnackBar,
     private commonFunction:SharedFunction,
     @Inject(MAT_DIALOG_DATA) public data) { }

  ngOnInit(): void {
    this.form = this.fb.group(
      {
        title: ['',Validators.required],
        shortName: ['',Validators.required],
        startDate: ['',Validators.required],
        endDate: ['',Validators.required],
        postStartDate: [''],
        postEndDate: [''],
        doesGrades: [''],
        doesExam: [''],
        doesComments: ['']       
      });
    
      if (this.data && (Object.keys(this.data).length !== 0 || Object.keys(this.data).length > 0) ){
       
        if(this.data.isAdd === true && this.data.isEdit === false){
          this.isEdit=false;
          this.parentStartDate=this.commonFunction.formatDateInEditMode(this.data.details.startDate);
          this.parentEndDate=this.commonFunction.formatDateInEditMode(this.data.details.endDate);
          if(this.data.details.isParent){
            this.markingPeriodLevel = "Year";   
            this.assignFieldsValue("semesterAddModel","tableSemesters","yearId","","","markingPeriodId");          
                    
          }else{
            if(this.data.details.yearId > 0){
              this.markingPeriodLevel = "Semester";
              this.assignFieldsValue("quarterAddModel","tableQuarter","semesterId","","","markingPeriodId");
             
            }else{
              this.markingPeriodLevel = "Quarter";
              this.assignFieldsValue("progressPeriodAddModel","tableProgressPeriods","quarterId","","","markingPeriodId");
             
            }
          }
        }else{
          this.isEdit=true;   
          this.parentStartDate=this.commonFunction.formatDateInEditMode(this.data.editDetails.startDate);
          this.parentEndDate=this.commonFunction.formatDateInEditMode(this.data.editDetails.endDate); 
          if(this.data.editDetails.doesGrades){
            this.doesGrades=true;
          }     
          if(this.data.editDetails.yearId > 0){
            this.markingPeriodLevel = "Year";   
            this.assignFieldsValue("semesterAddModel","tableSemesters","markingPeriodId","","",""); 
            this.assignFieldsValue("semesterAddModel","tableSemesters","yearId","","","");
                      
          }else if(this.data.editDetails.semesterId > 0){ 
            this.markingPeriodLevel = "Semester";  
            this.assignFieldsValue("quarterAddModel","tableQuarter","markingPeriodId","","",""); 
            this.assignFieldsValue("quarterAddModel","tableQuarter","semesterId","","","");                
          }else if(this.data.editDetails.quarterId > 0){
            this.markingPeriodLevel = "Quarter";
            this.assignFieldsValue("progressPeriodAddModel","tableProgressPeriods","markingPeriodId","","",""); 
            this.assignFieldsValue("progressPeriodAddModel","tableProgressPeriods","quarterId","","","");            
          }
          let arrList = Object.keys(this.data.editDetails);
          for (let i of arrList) {
            this.assignFieldsValue("markingPeriodAddModel","tableSchoolYears",i,"","",""); 
          } 
          this.markingPeriodAddModel.tableSchoolYears.startDate=this.commonFunction.formatDateInEditMode(this.markingPeriodAddModel.tableSchoolYears.startDate);
          this.markingPeriodAddModel.tableSchoolYears.endDate=this.commonFunction.formatDateInEditMode(this.markingPeriodAddModel.tableSchoolYears.endDate);
          this.markingPeriodAddModel.tableSchoolYears.postStartDate=this.commonFunction.formatDateInEditMode(this.markingPeriodAddModel.tableSchoolYears.postStartDate);
          this.markingPeriodAddModel.tableSchoolYears.postEndDate=this.commonFunction.formatDateInEditMode(this.markingPeriodAddModel.tableSchoolYears.postEndDate);         
        }        
        
      }
        
  }
  dateCompare() {      
    let openingDate = moment(this.form.controls.startDate.value).format('YYYY-MM-DD');
    let closingDate = moment(this.form.controls.endDate.value).format('YYYY-MM-DD');
   
    if (ValidationService.compareValidation(openingDate, closingDate) === false) {
      this.form.controls.endDate.setErrors({ compareError: true })
      
    }    
    let startDate = this.parentStartDate;
    let endDate = this.parentEndDate;  
    if(startDate!== "" && endDate !== ""){
      if (ValidationService.compareValidation(startDate, closingDate) === false) {
        this.form.controls.endDate.setErrors({ compareParentError: true })      
      }
      if (ValidationService.compareValidation(closingDate, endDate) === false) {
        this.form.controls.endDate.setErrors({ compareParentError: true })      
      }
    }
    
  }

  parentStartDateCompare() {     
    let openingDate = moment(this.form.controls.startDate.value).format('YYYY-MM-DD');
    let closingDate = this.parentStartDate;
    let endDate=this.parentEndDate;
    if(closingDate !== "" && endDate !== ""){
      if (ValidationService.compareValidation(closingDate, openingDate) === false) {
        this.form.controls.startDate.setErrors({ compareParentError: true })      
      }
      if (ValidationService.compareValidation(openingDate, endDate) === false) {
        this.form.controls.startDate.setErrors({ compareParentError: true })      
      }
    }
    
  }

 
  gradeDateCompare(){
    let gradeOpeningDate = this.form.controls.postStartDate.value;
    let gradeClosingDate = this.form.controls.postEndDate.value;
    if (ValidationService.compareValidation(gradeOpeningDate, gradeClosingDate) === false) {
      this.form.controls.postEndDate.setErrors({ compareGradeError: true })      
    }
  }
  checkGrade(data){
    if(data === false || data === undefined){
      this.doesGrades=true;
    }else{
      this.doesGrades=false;
    }
  }

  assignFieldsValue(model,table,field,model1,table1,field1){
    
    if(field1 !==""){
      this[model][table][field]=this.data.details[field1];
    }else{
      if(model1 !== "" && table1 !== ""){
       
        this[model][table][field]=this[model1][table1][field];
      }else{
        
        this[model][table][field]=this.data.editDetails[field];
      }
    } 
  }
  
 submit(){
 
  if (this.form.valid) {   
    if (this.isEdit ){
      if(this.markingPeriodLevel === "Year"){ 
        let arrList = Object.keys(this.obj);
        for (let i of arrList) {
          this.assignFieldsValue("semesterAddModel","tableSemesters",i,"markingPeriodAddModel","tableSchoolYears",""); 
        } 
                
        this.markingPeriodService.UpdateSemester(this.semesterAddModel).subscribe(data => {          
          if (typeof (data) == 'undefined') {
            this.snackbar.open('School Semester Updation failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('School Semester Updation failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {
      
              this.snackbar.open('School Semester Updation Successful.', '', {
                duration: 10000
              });
              this.dialogRef.close(true);  
            }
          }
      
        })
      }else if(this.markingPeriodLevel === "Semester"){
        let arrList = Object.keys(this.obj);
        for (let i of arrList) {
          this.assignFieldsValue("quarterAddModel","tableQuarter",i,"markingPeriodAddModel","tableSchoolYears",""); 
        }  
        this.markingPeriodService.UpdateQuarter(this.quarterAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('School Quarter Updation failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('School Quarter Updation failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {
      
              this.snackbar.open('School Quarter Updation Successful.', '', {
                duration: 10000
              });
              this.dialogRef.close(true);  
            }
          }
      
        })
      }else if(this.markingPeriodLevel === "Quarter"){
        let arrList = Object.keys(this.obj);
        for (let i of arrList) {
          this.assignFieldsValue("progressPeriodAddModel","tableProgressPeriods",i,"markingPeriodAddModel","tableSchoolYears",""); 
        }  
        this.markingPeriodService.UpdateProgressPeriod(this.progressPeriodAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('School Progress Period Updation failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('School Progress Period  Updation failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {
      
              this.snackbar.open('School Progress Period  Updation Successful.', '', {
                duration: 10000
              });
              this.dialogRef.close(true);  
            }
          }
      
        })
      }else{       
      this.markingPeriodService.UpdateSchoolYear(this.markingPeriodAddModel).subscribe(data => {
        if (typeof (data) == 'undefined') {
          this.snackbar.open('School Year Updation failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (data._failure) {
            this.snackbar.open('School Year Updation failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          } else {
    
            this.snackbar.open('School Year Updation Successful.', '', {
              duration: 10000
            });
            this.dialogRef.close(true);  
          }
        }    
      })
    }
      
    }else{
      if(this.markingPeriodLevel === "Year"){
        let arrList = Object.keys(this.obj);
        for (let i of arrList) {
          this.assignFieldsValue("semesterAddModel","tableSemesters",i,"markingPeriodAddModel","tableSchoolYears",""); 
        }       
        this.markingPeriodService.AddSemester(this.semesterAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('School Semester Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('School Semester Submission failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {
      
              this.snackbar.open('School Semester Submission Successful.', '', {
                duration: 10000
              });
              this.dialogRef.close(true);  
            }
          }
      
        })
      }else if(this.markingPeriodLevel === "Semester"){
        let arrList = Object.keys(this.obj);
        for (let i of arrList) {
          this.assignFieldsValue("quarterAddModel","tableQuarter",i,"markingPeriodAddModel","tableSchoolYears",""); 
        }   
       
        this.markingPeriodService.AddQuarter(this.quarterAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('School Quarter Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('School Quarter Submission failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {
      
              this.snackbar.open('School Quarter Submission Successful.', '', {
                duration: 10000
              });
              this.dialogRef.close(true);  
            }
          }
      
        })
      }else if(this.markingPeriodLevel === "Quarter"){
        let arrList = Object.keys(this.obj);
        for (let i of arrList) {
          this.assignFieldsValue("progressPeriodAddModel","tableProgressPeriods",i,"markingPeriodAddModel","tableSchoolYears",""); 
        }   
        this.markingPeriodService.AddProgressPeriod(this.progressPeriodAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('School Progress Period Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('School Progress Period Submission failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {      
              this.snackbar.open('School Progress Period Submission Successful.', '', {
                duration: 10000
              });
              this.dialogRef.close(true);  
            }
          }
      
        })
      }else{
        this.markingPeriodService.AddSchoolYear(this.markingPeriodAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('School Year Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('School Year Submission failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {
      
              this.snackbar.open('School Year Submission Successful.', '', {
                duration: 10000
              });
              this.dialogRef.close(true);  
            }
          }
      
        })
      }     

    }
  }
  
 }
}
