import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomFieldService } from '../../../../services/custom-field.service';
import {CustomFieldAddView} from '../../../../models/customFieldModel';
import { CustomFieldOptionsEnum } from '../../../../enums/custom-field-options.enum';
import { ValidationService } from '../../../shared/validation.service';

@Component({
  selector: 'vex-edit-school-fields',
  templateUrl: './edit-school-fields.component.html',
  styleUrls: ['./edit-school-fields.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditSchoolFieldsComponent implements OnInit {
  icClose = icClose;
  form: FormGroup;
  customFieldTitle;
  buttonType;
  customFieldOptionsEnum=Object.keys(CustomFieldOptionsEnum)
  customFieldAddView:CustomFieldAddView= new CustomFieldAddView()
  formfieldcheck=['Dropdown','Editable Dropdown','Multiple SelectBox']

  constructor(
    private dialogRef: MatDialogRef<EditSchoolFieldsComponent>, 
    @Inject(MAT_DIALOG_DATA) public data:any,
    private fb: FormBuilder,
    private snackbar:MatSnackBar,
    private customFieldService:CustomFieldService
    ) {
      this.form=fb.group({
        fieldId:[0],
        title:['',[Validators.required]],
        fieldType:[],
        selectOptions:[''],
        defaultSelection:[,[ValidationService.defaultSelectionValidator]],
        sortOrder:[,[Validators.required,Validators.min(1)]],
        required:[false],
        hide:[false],
        systemField:[false]

      })
      if(data==null){
        this.customFieldTitle="addCustomField";
        this.buttonType="submit";
      }
      else{
        this.buttonType="update";
        this.customFieldTitle="editRoom";
        this.customFieldAddView.customFields=data
        this.form.controls.fieldId.patchValue(data.fieldId)
        this.form.controls.title.patchValue(data.title)
        this.form.controls.selectOptions.patchValue(data.selectOptions.replaceAll(",","\n"))
        this.form.controls.defaultSelection.patchValue(data.defaultSelection)
        this.form.controls.sortOrder.patchValue(data.sortOrder)
        this.form.controls.required.patchValue(data.required)
        this.form.controls.fieldType.patchValue(data.type)
        this.form.controls.hide.patchValue(data.hide)
        this.form.controls.systemField.patchValue(data.systemField)
      }
     }

  ngOnInit(): void {
  }
  submit(){
    if(this.form.valid){

      if(this.form.controls.fieldId.value==0){
        //temp will replace with category id from selected category id 
        this.customFieldAddView.customFields.categoryId=1;


        this.customFieldAddView.customFields.fieldId=this.form.controls.fieldId.value;
        this.customFieldAddView.customFields.title=this.form.controls.title.value;
        this.customFieldAddView.customFields.selectOptions=this.form.controls.selectOptions.value.replaceAll("\n",",");
        this.customFieldAddView.customFields.defaultSelection=this.form.controls.defaultSelection.value;
        this.customFieldAddView.customFields.sortOrder=this.form.controls.sortOrder.value;
        this.customFieldAddView.customFields.required=this.form.controls.required.value;
        this.customFieldAddView.customFields.hide=this.form.controls.hide.value;
        this.customFieldAddView.customFields.systemField=this.form.controls.systemField.value;
        this.customFieldAddView.customFields.type=this.form.controls.fieldType.value;
        //this.customFieldAddView.customFields.type="Custom";
         this.customFieldService.addCustomField(this.customFieldAddView).subscribe(
          (res:CustomFieldAddView)=>{
            if(typeof(res)=='undefined'){
              this.snackbar.open('School field failed. ' + sessionStorage.getItem("httpError"), '', {
                duration: 10000
              });
            }
            else{
              if (res._failure) {
                this.snackbar.open('School field failed. ' + res._message, 'LOL THANKS', {
                  duration: 10000
                });
              } 
              else { 
                this.snackbar.open('School field Successful Created.', '', {
                  duration: 10000
                }); 
                this.dialogRef.close('submited');
              }
            }
          }
        ); 
      }
      else{
        this.customFieldAddView.customFields.fieldId=this.form.controls.fieldId.value;
        this.customFieldAddView.customFields.title=this.form.controls.title.value;
        this.customFieldAddView.customFields.selectOptions=this.form.controls.selectOptions.value.replaceAll("\n",",");
        this.customFieldAddView.customFields.defaultSelection=this.form.controls.defaultSelection.value;
        this.customFieldAddView.customFields.sortOrder=this.form.controls.sortOrder.value;
        this.customFieldAddView.customFields.required=this.form.controls.required.value;
        this.customFieldAddView.customFields.hide=this.form.controls.hide.value;
        this.customFieldAddView.customFields.systemField=this.form.controls.systemField.value;
        this.customFieldAddView.customFields.type=this.form.controls.fieldType.value;
        this.customFieldService.updateCustomField(this.customFieldAddView).subscribe(
          (res:CustomFieldAddView)=>{
            if(typeof(res)=='undefined'){
              this.snackbar.open('School field failed. ' + sessionStorage.getItem("httpError"), '', {
                duration: 10000
              });
            }
            else{
              if (res._failure) {
                this.snackbar.open('School field failed. ' + res._message, 'LOL THANKS', {
                  duration: 10000
                });
              } 
              else {
                this.snackbar.open('School field Successful Edited.', '', {
                  duration: 10000
                }); 
                this.dialogRef.close('submited');
              }
            }
          }
        );
      }
    }
  }

}
