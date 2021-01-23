import { Component, OnInit,Inject } from '@angular/core';
import { MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import {LovAddView} from '../../../../models/lovModel';
import {CommonService} from '../../../../services/common.service';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'vex-edit-school-classification',
  templateUrl: './edit-school-classification.component.html',
  styleUrls: ['./edit-school-classification.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditSchoolClassificationComponent implements OnInit {
  icClose = icClose;
  lovModel = new LovAddView();
  form: FormGroup;
  classificationModalTitle="addSchoolClassification";
  classificationModalActionTitle="submit";
  constructor(private dialogRef: MatDialogRef<EditSchoolClassificationComponent>,
    private fb: FormBuilder,
    private commonService:CommonService,
    private snackbar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data) { }

  ngOnInit(): void {
    this.form = this.fb.group(
      {
        lovColumnValue: ['', Validators.required]       
      });
      this.lovModel.dropdownValue.lovName="School Classification";
      if(this.data.mode === "edit"){
        this.lovModel.dropdownValue = this.data.editDetails;
        this.classificationModalTitle="editSchoolClassification";
        this.classificationModalActionTitle="update";
      }
  }

  get f() {
    return this.form.controls;
  }
  closeDialog(){
    this.dialogRef.close(false);
  }
  submit() {    
    if (this.form.valid) {   
      if(this.data.mode === "edit"){
        this.lovModel.dropdownValue.updatedBy = sessionStorage.getItem("email");
        this.lovModel.dropdownValue.createdBy = sessionStorage.getItem("email");
        this.commonService.updateDropdownValue(this.lovModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('Classification Updation failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('Classification Updation failed. '+ data._message, '', {
                duration: 10000
              });
            } else {
              this.snackbar.open('School Classification Updated Successfully.', '', {
                duration: 10000
              });              
              this.dialogRef.close(true);
            }
          }
  
        })
      }else{
        this.lovModel.dropdownValue.updatedBy = "";
        this.lovModel.dropdownValue.createdBy = sessionStorage.getItem("email");
        this.commonService.addDropdownValue(this.lovModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('Classification Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('Classification Submission failed. ' + data._message, '', {
                duration: 10000
              });
            } else {
              this.snackbar.open('School Classification Inserted Successfully.', '', {
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
