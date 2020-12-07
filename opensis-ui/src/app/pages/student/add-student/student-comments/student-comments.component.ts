import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/baseline-add';
import icPrint from '@iconify/icons-ic/baseline-print';
import icComment from '@iconify/icons-ic/twotone-comment';
import { MatDialog } from '@angular/material/dialog';
import { EditCommentComponent } from './edit-comment/edit-comment.component';
import {StudentService} from '../../../../services/student.service';
import {StudentCommentsListViewModel, StudentCommentsAddView} from '../../../../models/studentCommentsModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmDialogComponent } from '../../../../pages/shared-module/confirm-dialog/confirm-dialog.component';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { SharedFunction } from '../../../../pages/shared/shared-function';


@Component({
  selector: 'vex-student-comments',
  templateUrl: './student-comments.component.html',
  styleUrls: ['./student-comments.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StudentCommentsComponent implements OnInit {

  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;
  icComment = icComment;
  icPrint = icPrint;
  StudentCreate=SchoolCreate;
  @Input() studentCreateMode:SchoolCreate;
  @Input() studentDetailsForViewAndEdit;
  studentCommentsListViewModel:StudentCommentsListViewModel= new StudentCommentsListViewModel();
  studentCommentsAddView:StudentCommentsAddView=new StudentCommentsAddView();

  constructor(
    private fb: FormBuilder, 
    private dialog: MatDialog, 
    public translateService:TranslateService,
    private snackbar: MatSnackBar,
    private studentService:StudentService,
    private commonFunction:SharedFunction
    ) {
    translateService.use('en');
  }

  ngOnInit(): void {
    this.getAllComments();
  }

  openAddNew() {
    this.dialog.open(EditCommentComponent, {
      data:{studentId:this.studentDetailsForViewAndEdit.studentMaster.studentId},
      width: '800px'
    }).afterClosed().subscribe((data)=>{
      if(data==='submited'){
        this.getAllComments();
      }
    })
  }
  getAllComments(){
    this.studentCommentsListViewModel.studentId=this.studentDetailsForViewAndEdit.studentMaster.studentId
    this.studentService.getAllStudentCommentsList(this.studentCommentsListViewModel).subscribe(
      (res:StudentCommentsListViewModel)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Student Comments Not Found. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {     
            this.snackbar.open('Student Comments Not Found. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          }
          else {       
            this.studentCommentsListViewModel.studentCommentsList=res.studentCommentsList
            this.studentCommentsListViewModel.studentCommentsList.map(n=>{
              n.lastUpdated=this.commonFunction.serverToLocalDateAndTime(n.lastUpdated)
            })
          }
        }
      }
    );
  }

  confirmDelete(element){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete "+element.title+"."}
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
      if(dialogResult){
        this.deleteStudentComment(element);
      }
   });
  }
  deleteStudentComment(element){
    this.studentCommentsAddView.studentComments=element
    this.studentService.deleteStudentComment(this.studentCommentsAddView).subscribe(
      (res:StudentCommentsAddView)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Student Comments Not Found. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {     
            this.snackbar.open('Student Comments Not Found. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          }
          else {       
            this.getAllComments();
          }
        }
      }
    )
  }
  editComment(element){
    this.dialog.open(EditCommentComponent, {
      data:{studentId:this.studentDetailsForViewAndEdit.studentMaster.studentId,information:element},
      width: '800px'
    }).afterClosed().subscribe((data)=>{
      if(data==='submited'){
        this.getAllComments();
      }
    })
  }
}
