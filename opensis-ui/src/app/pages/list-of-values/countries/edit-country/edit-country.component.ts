import { Component, OnInit,Inject } from '@angular/core';
import { MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import {CountryAddModel} from '../../../../models/countryModel';
import {CommonService} from '../../../../services/common.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'vex-edit-country',
  templateUrl: './edit-country.component.html',
  styleUrls: ['./edit-country.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})

export class EditCountryComponent implements OnInit {
  icClose = icClose;
  countryAddModel = new CountryAddModel();
  form: FormGroup;
  countryModalTitle="addCountry";
  countryModalActionTitle="submit";
  constructor(private dialogRef: MatDialogRef<EditCountryComponent>,
    private fb: FormBuilder,
    private commonService:CommonService,
    private snackbar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data) { }

  ngOnInit(): void {
    this.form = this.fb.group(
      {
        name: ['', Validators.required],
        countryCode: ['']    
      });
      
      if(this.data.mode === "edit"){
        this.countryAddModel.country = this.data.editDetails;
        this.countryModalTitle="editCountry";
        this.countryModalActionTitle="update";
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
        this.countryAddModel.country.updatedBy = sessionStorage.getItem("email");
        this.countryAddModel.country.createdBy = sessionStorage.getItem("email");
        this.commonService.UpdateCountry(this.countryAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('Country Updation failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('Country Updation failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {
              this.snackbar.open('Country Updation Successful.', '', {
                duration: 10000
              });              
              this.dialogRef.close(true);
            }
          }
  
        })
      }else{
        this.countryAddModel.country.updatedBy = "";
        this.countryAddModel.country.createdBy = sessionStorage.getItem("email");
        this.commonService.AddCountry(this.countryAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('Country Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('Country Submission failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {
              this.snackbar.open('Country Submission Successful.', '', {
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
