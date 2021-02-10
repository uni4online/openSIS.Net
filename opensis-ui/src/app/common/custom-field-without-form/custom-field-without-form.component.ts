import { Component, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm, ControlContainer } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SchoolAddViewModel } from '../../../../src/app/models/schoolMasterModel';
import { SchoolCreate } from '../../../../src/app/enums/school-create.enum';
import { CustomFieldListViewModel, CustomFieldModel } from '../../../../src/app/models/customFieldModel';
import { FieldsCategoryListView } from '../../../../src/app/models/fieldsCategoryModel';
import { SharedFunction } from '../../../../src/app/pages/shared/shared-function';
import { CustomFieldService } from '../../../../src/app/services/custom-field.service';
import { SchoolService } from '../../../../src/app/services/school.service';
import { CustomFieldsValueModel } from '../../../../src/app/models/customFieldsValueModel';
import { stagger60ms } from '../../../../src/@vex/animations/stagger.animation';
import { StudentAddModel } from '../../../../src/app/models/studentModel';
import { StudentService } from '../../../../src/app/services/student.service';
import { StaffAddModel } from '../../models/staffModel';
import { StaffService } from '../../services/staff.service';

@Component({
  selector: 'vex-custom-field-without-form',
  templateUrl: './custom-field-without-form.component.html',
  styleUrls: ['./custom-field-without-form.component.scss'],
  animations: [stagger60ms],
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }]
})
export class CustomFieldWithoutFormComponent implements OnInit {
  schoolCreate = SchoolCreate;
  @Input() schoolDetailsForViewAndEdit;
  @Input() categoryId;
  editInfo: boolean = false;
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  studentAddModel: StudentAddModel = new StudentAddModel();
  staffAddModel: StaffAddModel = new StaffAddModel();
  @Input() schoolCreateMode;
  @Input() studentCreateMode;
  @Input() staffCreateMode;
  @Input() module;
  staffMultiSelectValue;
  studentMultiSelectValue;
  schoolMultiSelectValue
  customFieldListViewModel = new CustomFieldListViewModel();
  headerTitle: string = "Other Information";
  @ViewChild('f') currentForm: NgForm;
  f: NgForm;
  fieldsCategoryListView = new FieldsCategoryListView();
  constructor(
    private snackbar: MatSnackBar,
    private commonFunction: SharedFunction,
    private customFieldservice: CustomFieldService,
    private schoolService: SchoolService,
    private studentService: StudentService,
    private staffService : StaffService
  ) {
    this.schoolService.getSchoolDetailsForGeneral.subscribe((res: SchoolAddViewModel) => {
      this.schoolAddViewModel = res;
      this.checkCustomValue();
    });
    this.studentService.getStudentDetailsForGeneral.subscribe((res: StudentAddModel) => {
      this.studentAddModel = res;
      this.checkStudentCustomValue();
    })
    this.staffService.getStaffDetailsForGeneral.subscribe((res: StaffAddModel) => {
      this.staffAddModel = res;
      this.checkStaffCustomValue();
    })
  }

  ngOnInit(): void {

    if (this.module === 'Student') {
      this.studentAddModel = this.schoolDetailsForViewAndEdit;
      this.checkStudentCustomValue();

    }
    else if (this.module === 'School') {
      this.checkNgOnInitCustomValue();
    }
    else if(this.module === 'Staff') {
      this.staffAddModel = this.schoolDetailsForViewAndEdit;
      this.checkStaffCustomValue();

    }
  }

  modelChanged(selectValue) {
    if (this.module === 'Staff') {
      this.staffService.setStaffMultiselectValue(selectValue);
    }
    else if (this.module === 'Student') {
      this.studentService.setStudentMultiselectValue(selectValue);
    }
    else if (this.module === 'School') {
      this.schoolService.setSchoolMultiselectValue(selectValue);
    }

}

  checkStudentCustomValue() {
    if (this.studentAddModel !== undefined) {
      if (this.studentAddModel?.fieldsCategoryList !== undefined && this.studentAddModel?.fieldsCategoryList?.length >0) {

        for (let studentCustomField of this.studentAddModel.fieldsCategoryList[this.categoryId]?.customFields) {
          if (studentCustomField?.customFieldsValue.length == 0) {

            studentCustomField?.customFieldsValue.push(new CustomFieldsValueModel());
          }
          else {
            if (studentCustomField?.type === 'Multiple SelectBox') {
              this.studentMultiSelectValue = studentCustomField?.customFieldsValue[0].customFieldValue.split('|');

            }
          }
        }

      }
    }
  }

  checkStaffCustomValue(){
    if (this.staffAddModel !== undefined) {
      if (this.staffAddModel?.fieldsCategoryList !== undefined && this.staffAddModel?.fieldsCategoryList !== null) {

        for (let staffCustomField of this.staffAddModel?.fieldsCategoryList[this.categoryId]?.customFields) {
          if (staffCustomField?.customFieldsValue.length == 0) {

            staffCustomField?.customFieldsValue.push(new CustomFieldsValueModel());
          }
          else {
            if (staffCustomField?.type === 'Multiple SelectBox') {
              this.staffMultiSelectValue = staffCustomField?.customFieldsValue[0].customFieldValue.split('|');
              
            }
          }
        }

      }
    }
  }

  checkNgOnInitCustomValue() {

    if (this.schoolDetailsForViewAndEdit !== undefined) {
      if (this.schoolDetailsForViewAndEdit.schoolMaster.fieldsCategory !== undefined) {
        this.schoolAddViewModel = this.schoolDetailsForViewAndEdit;
        if (this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId]?.customFields !== undefined) {


          for (let schoolCustomField of this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].customFields) {
            if (schoolCustomField.customFieldsValue.length == 0) {

              schoolCustomField.customFieldsValue.push(new CustomFieldsValueModel());

            }
            else {
              if (schoolCustomField?.type === 'Multiple SelectBox') {
                this.schoolMultiSelectValue = schoolCustomField?.customFieldsValue[0].customFieldValue.split('|');
  
              }
            }
          }
        }
      }
    }
  }

  checkCustomValue() {
    if (this.schoolAddViewModel !== undefined) {
      if (this.schoolAddViewModel.schoolMaster.fieldsCategory !== undefined && this.schoolAddViewModel.schoolMaster.fieldsCategory !== null) {

        if (this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId]?.customFields !== undefined) {


          for (let schoolCustomField of this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].customFields) {
            if (schoolCustomField.customFieldsValue.length == 0) {

              schoolCustomField.customFieldsValue.push(new CustomFieldsValueModel());
            }
            else {
              if (schoolCustomField?.type === 'Multiple SelectBox') {
                this.schoolMultiSelectValue = schoolCustomField?.customFieldsValue[0].customFieldValue.split('|');
  
              }
            }
          }
        }
      }
    }
  }

}
