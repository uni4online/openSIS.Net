import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import icBack from '@iconify/icons-ic/baseline-arrow-back';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';
import { MatDialog } from '@angular/material/dialog';
import { fadeInRight400ms } from 'src/@vex/animations/fade-in-right.animation';
import { StudentSiblingAssociation, StudentSiblingSearch } from '../../../../../models/studentModel';
import { StudentService } from '../../../../../services/student.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'vex-add-sibling',
  templateUrl: './add-sibling.component.html',
  styleUrls: ['./add-sibling.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class AddSiblingComponent implements OnInit {
  icClose = icClose;
  icBack = icBack;
  form: FormGroup;
  studentSiblingSearch:StudentSiblingSearch=new StudentSiblingSearch();
  studentSiblingAssociation:StudentSiblingAssociation = new StudentSiblingAssociation();
  hideSearchBoxAfterSearch=true;
  constructor(private dialogRef: MatDialogRef<AddSiblingComponent>,
    private fb: FormBuilder,
    private _studentService:StudentService,
    private snackbar: MatSnackBar) { }

  ngOnInit(): void {
    this.form = this.fb.group(
      {
        firstGivenName: [null,Validators.required],
        lastFamilyName: [null,Validators.required],
        gradeLevel: [null],
        studentId:[null],
        dob:[null],
        searchAllSchool: [false]
      });
  }

  search(){
    this.form.markAllAsTouched();
    if(this.form.valid){
      if(this.form.value.searchAllSchool){
        this.studentSiblingSearch.schoolId=null
      }else{
        this.studentSiblingSearch.schoolId=+sessionStorage.getItem("selectedSchoolId")
      }
      if(this.studentSiblingSearch.studentInternalId==""){
        this.studentSiblingSearch.studentInternalId=null;
      }
      this._studentService.siblingSearch(this.studentSiblingSearch).subscribe((res)=>{
        if (typeof (res) == 'undefined') {
          this.snackbar.open('Student Search failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (res._failure) {
            this.snackbar.open('Student Search failed. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          } else {
            this.hideSearchBoxAfterSearch=false;
            this.studentSiblingSearch.getStudentForView=res.getStudentForView;
          }
        }
      })
    }
  }

  associateStudent(studentDetails){
    this.studentSiblingAssociation.studentMaster.studentId=studentDetails.studentId;
    this.studentSiblingAssociation.studentMaster.schoolId=studentDetails.schoolId;
    this.studentSiblingAssociation.studentId=this._studentService.getStudentId();
    this._studentService.associationSibling(this.studentSiblingAssociation).subscribe((res)=>{
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Association failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          this.snackbar.open('Association failed. ' + res._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {  
          this.dialogRef.close(true);  
          this.snackbar.open('Sibling has been associated','Thanks', {
            duration: 10000
          });
        }
      }
    })
  }
}
