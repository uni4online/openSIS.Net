import { Component, OnInit,Inject } from '@angular/core';
import { MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import {LanguageAddModel} from '../../../../models/languageModel';
import {CommonService} from '../../../../services/common.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ValidationService } from 'src/app/pages/shared/validation.service';

@Component({
  selector: 'vex-edit-language',
  templateUrl: './edit-language.component.html',
  styleUrls: ['./edit-language.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})

export class EditLanguageComponent implements OnInit {
  icClose = icClose;
  languageModel = new LanguageAddModel();
  form: FormGroup;
  languageModalTitle="addLanguage";
  languageModalActionTitle="submit";
  constructor(private dialogRef: MatDialogRef<EditLanguageComponent>,
    private fb: FormBuilder,
    private commonService:CommonService,
    private snackbar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data) { }

  ngOnInit(): void {
    this.form = this.fb.group(
      {
        locale: ['', ValidationService.noWhitespaceValidator],
        languageCode: ['',[ValidationService.noWhitespaceValidator,ValidationService.lowerCaseValidator]]    
      });
      
      if(this.data.mode === "edit"){
        this.languageModel.language = this.data.editDetails;
        this.languageModalTitle="editLanguage";
        this.languageModalActionTitle="update";
      }
  }

  get f() {
    return this.form.controls;
  }
  closeDialog(){
    this.dialogRef.close(false);
  }
  submit() {    
    this.form.markAllAsTouched()
    if (this.form.valid) {   
      if(this.data.mode === "edit"){
        this.languageModel.language.updatedBy = sessionStorage.getItem("email");
        this.languageModel.language.createdBy = sessionStorage.getItem("email");
        this.languageModel._tenantName = sessionStorage.getItem("tenant");
        this.languageModel._token = sessionStorage.getItem("token");
        this.commonService.UpdateLanguage(this.languageModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('Language Updation failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('Language Updation failed. ' + data._message, '', {
                duration: 10000
              });
            } else {
              this.snackbar.open('Language Updation Successful.', '', {
                duration: 10000
              });              
              this.dialogRef.close(true);
            }
          }
  
        })
      }else{
        this.languageModel.language.updatedBy = '';
        this.languageModel.language.createdBy = sessionStorage.getItem("email");
        this.languageModel._tenantName = sessionStorage.getItem("tenant");
        this.languageModel._token = sessionStorage.getItem("token");
        this.commonService.AddLanguage(this.languageModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('Language Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('Language Submission failed. ' + data._message, '', {
                duration: 10000
              });
            } else {
              this.snackbar.open('Language Submission Successful.', '', {
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
