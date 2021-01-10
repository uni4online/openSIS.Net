import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/twotone-add';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import {GetAllSubjectModel,AddSubjectModel,UpdateSubjectModel,SubjectModel} from '../../../../models/courseManagerModel';
import {CourseManagerService} from '../../../../services/course-manager.service';
import {MatSnackBar} from  '@angular/material/snack-bar';
import { FormBuilder,NgForm,FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmDialogComponent } from '../../../shared-module/confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
@Component({
  selector: 'vex-manage-subjects',
  templateUrl: './manage-subjects.component.html',
  styleUrls: ['./manage-subjects.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class ManageSubjectsComponent implements OnInit {

  icClose = icClose;
  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;
  subjectList=[];
  form: FormGroup;
  f: NgForm;
  editMode=false;
  editSubjectId;
  subjectListArr=[];
  subjectNameArr =[];
  getAllSubjectModel: GetAllSubjectModel = new GetAllSubjectModel();
  addSubjectModel: AddSubjectModel = new AddSubjectModel();
  updateSubjectModel: UpdateSubjectModel = new UpdateSubjectModel();
  
  constructor(
    private dialogRef: MatDialogRef<ManageSubjectsComponent>,
    private courseManager:CourseManagerService,
    private snackbar: MatSnackBar,
    private fb: FormBuilder,
    public translateService:TranslateService,
    private dialog: MatDialog, ) { 
      translateService.use('en');   
    }

  ngOnInit(): void {  
    this.getAllSubjectList();
  }
  
  getAllSubjectList(){   
    this.courseManager.GetAllSubjectList(this.getAllSubjectModel).subscribe(data => {
      if(data._failure){
        if(data._message==="NO RECORD FOUND"){
          this.snackbar.open('NO RECORD FOUND. ', '', {
            duration: 10000
          });        
        }
      }else{      
        this.subjectList=data.subjectList;
        this.subjectList.map(val=>{
          this.updateSubjectModel.subject.push(new SubjectModel());    
        })
         
      }
    });
  }
  confirmDelete(deleteDetails){
    
    // call our modal window
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete "+deleteDetails.subjectName+"."}
    });
    // listen to response
    dialogRef.afterClosed().subscribe(dialogResult => {
      // if user pressed yes dialogResult will be true, 
      // if user pressed no - it will be false
      if(dialogResult){
        this.deleteSubject(deleteDetails);
      }
   });
  }
  deleteSubject(deleteDetails){
  this.addSubjectModel.subject.subjectId=deleteDetails.subjectId;    
  this.courseManager.DeleteSubject(this.addSubjectModel).subscribe(data => {
    if (typeof (data) == 'undefined') {
      this.snackbar.open('Subject Deletion failed. ' + sessionStorage.getItem("httpError"), '', {
        duration: 10000
      });
    }
    else {
      if (data._failure) {
        this.snackbar.open('Subject Deletion failed. ' + data._message, 'LOL THANKS', {
          duration: 10000
        });
      } else {
     
        this.snackbar.open('Subject Deletion Successful.', '', {
          duration: 10000
        }).afterOpened().subscribe(data => {
          this.getAllSubjectList();
        });
        
      }
    }

  })
}
removeByAttr(arr, attr, value){
  var i = arr.length;
  while(i--){
     if( arr[i] 
         && arr[i].hasOwnProperty(attr) 
         && (arguments.length > 2 && arr[i][attr] === value ) ){ 

         arr.splice(i,1);

     }
  }
  return arr;
}
updateSubject(element,index){
  
  let obj = {};
  obj["subjectId"] = element.subjectId;
  obj["subjectName"] = element.subjectName;
  
  this.subjectListArr.push(obj);  
  this.removeByAttr(this.subjectList, 'subjectId', element.subjectId);  
  let obj1 ={};
  
  obj1["subjectName"] = element.subjectName;
  obj1["subjectId"] = element.subjectId;
  this.subjectNameArr.push(obj1); 
  
  this.subjectNameArr.map((val,index)=>{
  
    this.updateSubjectModel.subject[index].subjectName = val.subjectName;
    this.updateSubjectModel.subject[index].subjectId = val.subjectId;
  })
  
  
  
  
}


  submit(){
    if(this.addSubjectModel.subject.hasOwnProperty("subjectName")){
      this.courseManager.AddSubject(this.addSubjectModel).subscribe(data => {     
        if(typeof(data)=='undefined'){
          this.snackbar.open('Subject Submission failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (data._failure) {
            this.snackbar.open('Subject Submission failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          } 
          else{
            this.snackbar.open('Subject Submission Successful.', '', {
              duration: 10000
            }).afterOpened().subscribe(data => {
              this.getAllSubjectList();
              this.addSubjectModel.subject=new SubjectModel()
            });
          }        
        }      
      });
    }
    this.updateSubjectModel.subject.map(val=>{
      if(val.hasOwnProperty('subjectName')){
        this.addSubjectModel.subject.subjectId = val.subjectId;
        this.addSubjectModel.subject.subjectName = val.subjectName;
        this.courseManager.UpdateSubject(this.addSubjectModel).subscribe(data => {     
          if(typeof(data)=='undefined'){
            this.snackbar.open('Subject Updation failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else{
            if (data._failure) {
              this.snackbar.open('Subject Updation failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } 
            else{
              this.snackbar.open('Subject Updation Successful.', '', {
                duration: 10000
              }).afterOpened().subscribe(data => {
                this.getAllSubjectList();
                this.subjectListArr=[];
                this.addSubjectModel.subject=new SubjectModel()
              });
            }        
          }      
        });

          
      }
    })
   
  
  } 
  
}
