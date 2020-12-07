import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedFunction } from '../../../../src/app/pages/shared/shared-function';
import { FieldsCategoryListView, FieldsCategoryModel } from '../../../../src/app/models/fieldsCategoryModel';
import { CustomFieldService } from '../../../../src/app/services/custom-field.service';
import { CustomFieldListViewModel, CustomFieldModel } from '../../../../src/app/models/customFieldModel';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import { NgForm } from '@angular/forms';
import { stagger60ms } from '../../../../src/@vex/animations/stagger.animation';
import { fadeInUp400ms } from '../../../../src/@vex/animations/fade-in-up.animation';
import { fadeInRight400ms } from '../../../../src/@vex/animations/fade-in-right.animation';
import { SchoolCreate } from '../../../../src/app/enums/school-create.enum';
import { SchoolAddViewModel } from '../../../../src/app/models/schoolMasterModel';
import { SchoolService } from '../../../../src/app/services/school.service';
import { CustomFieldsValueModel } from '../../../../src/app/models/customFieldsValueModel';
import { Router } from '@angular/router';
import { StudentAddModel } from '../../../../src/app/models/studentModel';
import { StudentService } from '../../../../src/app/services/student.service';

@Component({
  selector: 'vex-custom-field',
  templateUrl: './custom-field.component.html',
  styleUrls: ['./custom-field.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class CustomFieldComponent implements OnInit {
  SchoolCreate = SchoolCreate;
  @Input() schoolCreateMode;
  @Input() studentCreateMode;
  @Input() categoryTitle;
  @Input() categoryId;
  @Input() schoolDetailsForViewAndEdit;
  @Input() module;
  icEdit = icEdit;
  icAdd = icAdd;
  viewInfo: boolean = true;
  editInfo: boolean = false;
  studentAddViewModel: StudentAddModel = new StudentAddModel();
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  @ViewChild('f') currentForm: NgForm;
  f: NgForm;
  formActionButtonTitle: string;
  constructor(
    private snackbar: MatSnackBar,
    private commonFunction: SharedFunction,
    private _studentService:StudentService,
    private _schoolService: SchoolService,
    private router: Router,
  ) {
    if (this.module === 'School') {
      this._schoolService.getSchoolDetailsForGeneral.subscribe((res: SchoolAddViewModel) => {
        this.schoolAddViewModel = res;
        this.checkCustomValue();
      });
    }
  }

  ngOnInit(): void {
    if (this.module === 'Student') {
      this.studentAddViewModel = this.schoolDetailsForViewAndEdit;
      this.checkStudentCustomValue();

    }
    else if(this.module === 'School'){
      this.checkNgOnInitCustomValue();
    }
  }

  submit() {
    this.currentForm.form.markAllAsTouched();
    if (this.currentForm.form.valid) {
      if (this.module === 'School') {
        this.updateSchool();
      }
      else if (this.module === 'Student') {
        this.updateStudent();
      }
    }
  }

  checkNgOnInitCustomValue() {

    if (this.schoolDetailsForViewAndEdit !== undefined) {
      let catId = this.categoryId;
      if (this.schoolDetailsForViewAndEdit.schoolMaster.fieldsCategory !== undefined) {
        this.schoolAddViewModel = this.schoolDetailsForViewAndEdit;
        for (let i = 0; i < this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].customFields.length; i++) {
          if (this.schoolAddViewModel.schoolMaster.fieldsCategory[catId].customFields[i].customFieldsValue.length == 0) {
            this.schoolAddViewModel.schoolMaster.fieldsCategory[catId].customFields[i].customFieldsValue.push(new CustomFieldsValueModel());
          }
        }
      }

    }
  }


  checkStudentCustomValue() {
    if (this.studentAddViewModel !== undefined) {
      let catId = this.categoryId;
      if (this.studentAddViewModel.fieldsCategoryList !== undefined) {

        for (let i = 0; i < this.studentAddViewModel.fieldsCategoryList[this.categoryId]?.customFields.length; i++) {
          if (this.studentAddViewModel.fieldsCategoryList[catId].customFields[i]?.customFieldsValue.length == 0) {

            this.studentAddViewModel.fieldsCategoryList[catId]?.customFields[i]?.customFieldsValue.push(new CustomFieldsValueModel());
          }
        }

      }
    }
  }

  checkCustomValue() {
    
    if (this.schoolAddViewModel !== undefined) {
      let catId = this.categoryId;
      if (this.schoolAddViewModel.schoolMaster.fieldsCategory !== undefined) {

        for (let i = 0; i < this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].customFields.length; i++) {
          if (this.schoolAddViewModel.schoolMaster.fieldsCategory[catId].customFields[i].customFieldsValue.length == 0) {

            this.schoolAddViewModel.schoolMaster.fieldsCategory[catId].customFields[i].customFieldsValue.push(new CustomFieldsValueModel());
          }
        }

      }
    }
  }
  updateStudent(){
    this.studentAddViewModel.selectedCategoryId= this.studentAddViewModel.fieldsCategoryList[this.categoryId].categoryId;
    this.studentAddViewModel._tenantName=sessionStorage.getItem("tenant");
    this.studentAddViewModel._token=sessionStorage.getItem("token");
    this._studentService.UpdateStudent(this.studentAddViewModel).subscribe(data => {                        
      if (typeof (data) == 'undefined') {
        this.snackbar.open(this.categoryTitle + ' Updation failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open(this.categoryTitle + ' Updation failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {    
          this.snackbar.open(this.categoryTitle + ' Updated Successfully.', '', {
            duration: 10000
          });
          
          
        }
      }
  
    })
  }
  

  updateSchool() {
    this.schoolAddViewModel.selectedCategoryId = this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].categoryId;
    this.schoolAddViewModel.schoolMaster.city = this.schoolAddViewModel.schoolMaster.city.toString();
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened);
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed);
    this._schoolService.UpdateSchool(this.schoolAddViewModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open(this.categoryTitle + ' ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open(this.categoryTitle + ' ' + + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {

          this.snackbar.open(this.categoryTitle + " " + 'Updation Successful', '', {
            duration: 10000
          });
          this._schoolService.changeMessage(true);
        }
      }

    });
  }


  editOtherInfo() {
    this.schoolCreateMode = this.SchoolCreate.EDIT;
    this.studentCreateMode = this.SchoolCreate.EDIT;
    this.formActionButtonTitle = "update";
  }
}
