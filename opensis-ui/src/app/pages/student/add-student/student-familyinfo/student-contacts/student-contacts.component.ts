import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/baseline-add';
import { MatDialog } from '@angular/material/dialog';
import { EditContactComponent } from '../edit-contact/edit-contact.component';
import { ViewContactComponent } from '../view-contact/view-contact.component';
import { GetAllParentInfoModel,AddParentInfoModel  } from '../../../../../models/studentModel';
import { StudentService  } from '../../../../../services/student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmDialogComponent } from '../../../../shared-module/confirm-dialog/confirm-dialog.component';
@Component({
  selector: 'vex-student-contacts',
  templateUrl: './student-contacts.component.html',
  styleUrls: ['./student-contacts.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StudentContactsComponent implements OnInit {
  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;
  parentListArray=[];
  contactType = "Primary";
  getAllParentInfoModel : GetAllParentInfoModel = new GetAllParentInfoModel();
  addParentInfoModel : AddParentInfoModel = new AddParentInfoModel();
  constructor(
    private fb: FormBuilder, private dialog: MatDialog,
    public translateService:TranslateService,
    public studentService:StudentService,
    private snackbar: MatSnackBar) { }

  ngOnInit(): void {
    this.parentListArray = this.getAllParentInfoModel.parentInfoList;    
    this.viewParentListForStudent();
  }

  openAddNew() {
    this.dialog.open(EditContactComponent, {
      data: {
        contactType:this.contactType,
        mode:'add' },
      width: '600px'
    }).afterClosed().subscribe(data => {
      if(data){
        this.viewParentListForStudent();
      }      
    });
  }

  openViewDetails(parentInfo) {
    this.dialog.open(EditContactComponent, {
      data: {
        parentInfo:parentInfo,
        mode:'view'},
      width: '600px'
    });
  }

  editParentInfo(studentId){

  }
  confirmDelete(deleteDetails){    
    // call our modal window
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete "+deleteDetails.firstname+" "+deleteDetails.lastname+"."}
    });
    // listen to response
    dialogRef.afterClosed().subscribe(dialogResult => {
      // if user pressed yes dialogResult will be true, 
      // if user pressed no - it will be false
      if(dialogResult){
        this.deleteParentInfo(deleteDetails.parentId);
      }
   });
  }
  deleteParentInfo(parentId){
    this.addParentInfoModel.parentInfo.parentId=parentId;
    this.studentService.deleteParentInfo(this.addParentInfoModel).subscribe(
      data => { 
        if(typeof(data)=='undefined'){
          this.snackbar.open('Parent Information failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (data._failure) {  
            
            this.snackbar.open('Parent Information failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          } 
          else {       
            this.snackbar.open('Parent Deletion Successful.', '', {
              duration: 10000
            }).afterOpened().subscribe(data => {
              this.viewParentListForStudent();
            });
            
          }
        }
      })
  }
  viewParentListForStudent(){
    this.studentService.viewParentListForStudent(this.getAllParentInfoModel).subscribe(
      data => { 
        if(typeof(data)=='undefined'){
          this.snackbar.open('Parent Information failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          this.parentListArray=[];
          this.contactType="Primary";  
          if (data._failure) {     
            this.snackbar.open('Parent Information failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          } 
          else { 
            this.parentListArray= data.parentInfoList;  
            if(this.parentListArray.length === 1){
              this.contactType = "Secondary"
            }else{
              this.contactType = "Other"
            }          
          }
        }
      })
  }
}
