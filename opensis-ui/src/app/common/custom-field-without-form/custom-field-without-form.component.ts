import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
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

@Component({
  selector: 'vex-custom-field-without-form',
  templateUrl: './custom-field-without-form.component.html',
  styleUrls: ['./custom-field-without-form.component.scss'],
  animations: [
    stagger60ms]
})
export class CustomFieldWithoutFormComponent implements OnInit {
  schoolCreate = SchoolCreate;
  @Input() schoolDetailsForViewAndEdit;
  @Input() categoryId;
  editInfo: boolean = false;
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  @Input() schoolCreateMode;
  customFieldListViewModel = new CustomFieldListViewModel();
  headerTitle: string = "Other Information";
  @ViewChild('f') currentForm: NgForm;
  f: NgForm;
  fieldsCategoryListView = new FieldsCategoryListView();
  constructor(
    private snackbar: MatSnackBar,
    private commonFunction: SharedFunction,
    private customFieldservice: CustomFieldService,
    private _schoolService: SchoolService,
  ) {
    this._schoolService.getSchoolDetailsForGeneral.subscribe((res: SchoolAddViewModel) => {
      this.schoolAddViewModel = res;
      this.checkCustomValue();
    });

  }

  ngOnInit(): void {
    this.checkNgOnInitCustomValue();
  }

  checkNgOnInitCustomValue() {

    if (this.schoolDetailsForViewAndEdit !== undefined) {
      let catId = this.categoryId;
      if (this.schoolDetailsForViewAndEdit.schoolMaster.fieldsCategory !== undefined) {
        this.schoolAddViewModel = this.schoolDetailsForViewAndEdit;
        if (this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId]?.customFields !== undefined) {

         
          for (let i = 0; i < this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].customFields.length; i++) {
            if (this.schoolAddViewModel.schoolMaster.fieldsCategory[catId].customFields[i].customFieldsValue.length == 0) {

              this.schoolAddViewModel.schoolMaster.fieldsCategory[catId].customFields[i].customFieldsValue.push(new CustomFieldsValueModel());

            }
          }
        }
      }
    }
  }

  checkCustomValue() {
    if (this.schoolAddViewModel !== undefined) {
      let catId = this.categoryId;
      if (this.schoolAddViewModel.schoolMaster.fieldsCategory !== undefined) {

        if (this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId]?.customFields !== undefined) {


          for (let i = 0; i < this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].customFields.length; i++) {
            if (this.schoolAddViewModel.schoolMaster.fieldsCategory[catId].customFields[i].customFieldsValue.length == 0) {

              this.schoolAddViewModel.schoolMaster.fieldsCategory[catId].customFields[i].customFieldsValue.push(new CustomFieldsValueModel());
            }
          }
        }
      }
    }
  }

}
