import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/baseline-add';
import icComment from '@iconify/icons-ic/twotone-comment';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { StudentAddModel } from '../../../../models/studentModel';
import { StudentService } from '../../../../services/student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ViewParentInfoModel } from '../../../../models/parentInfoModel';
import { ParentInfoService } from '../../../../services/parent-info.service';
import { ImageCropperService } from '../../../../services/image-cropper.service';

@Component({
  selector: 'vex-student-medicalinfo',
  templateUrl: './student-medicalinfo.component.html',
  styleUrls: ['./student-medicalinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StudentMedicalinfoComponent implements OnInit, OnDestroy {
  studentCreate = SchoolCreate;
  @Input() studentCreateMode: SchoolCreate;
  @Input() categoryId;
  @Input() studentDetailsForViewAndEdit;
  @ViewChild('f') currentForm: NgForm;
  @Output() studentDetailsForParent = new EventEmitter<StudentAddModel>();
  studentAddModel: StudentAddModel = new StudentAddModel();
  parentInfoModel: ViewParentInfoModel = new ViewParentInfoModel();
  icEdit = icEdit;
  icDelete = icDelete;
  module = 'Student';
  icAdd = icAdd;
  icComment = icComment;
  parentsFullName = [];
  constructor(private fb: FormBuilder,
    public translateService: TranslateService,
    private studentService: StudentService,
    private snackbar: MatSnackBar,
    private parentInfoService: ParentInfoService,
    private imageCropperService: ImageCropperService) {
    translateService.use('en');

  }

  ngOnInit(): void {
    if (this.studentCreateMode == this.studentCreate.VIEW) {
      this.studentService.changePageMode(this.studentCreateMode);
      this.studentAddModel = this.studentDetailsForViewAndEdit;
      this.imageCropperService.enableUpload(false);
    } else {
      this.getAllParents();
      this.studentService.changePageMode(this.studentCreateMode);
      this.studentAddModel = this.studentService.getStudentDetails();
    }
  }

  editMedicalInfo() {
    this.getAllParents();
    this.studentCreateMode = this.studentCreate.EDIT;
    this.studentService.changePageMode(this.studentCreateMode);
    this.imageCropperService.enableUpload(true);
  }

  cancelEdit() {
    this.studentCreateMode = this.studentCreate.VIEW;
    this.studentService.changePageMode(this.studentCreateMode);
    this.imageCropperService.enableUpload(false);
  }

  getAllParents() {
    this.parentInfoModel.studentId = this.studentAddModel.studentMaster.studentId;
    this.parentInfoService.ViewParentListForStudent(this.parentInfoModel).subscribe((res) => {
      this.concatenateParentsName(res.parentInfoList);
    })
  }

  concatenateParentsName(parentDetails) {
    this.parentsFullName = parentDetails?.map((item) => {
      return item.firstname + ' ' + item.lastname
    });
  }

  submit() {
    this.currentForm.form.markAllAsTouched();
    if (this.currentForm.form.valid) {
      if(this.studentAddModel.fieldsCategoryList!==null){
        this.studentAddModel.selectedCategoryId = this.studentAddModel.fieldsCategoryList[this.categoryId].categoryId;
        
        for (var i = 0; i < this.studentAddModel.fieldsCategoryList[this.categoryId].customFields.length; i++) {
          if (this.studentAddModel.fieldsCategoryList[this.categoryId].customFields[i].type === "Multiple SelectBox") {
            this.studentAddModel.fieldsCategoryList[this.categoryId].customFields[i].customFieldsValue[0].customFieldValue = this.studentService.getStudentMultiselectValue().toString().replaceAll(",", "|");
          }
        }
      }
      this.studentAddModel._tenantName = sessionStorage.getItem("tenant");
      this.studentAddModel._token = sessionStorage.getItem("token");
      this.studentService.UpdateStudent(this.studentAddModel).subscribe(data => {
        if (typeof (data) == 'undefined') {
          this.snackbar.open('Medical Information Updation failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (data._failure) {
            this.snackbar.open('Medical Information Updation failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          } else {
            this.snackbar.open('Medical Information Update Successful.', '', {
              duration: 10000
            });
            this.studentCreateMode = this.studentCreate.VIEW;
            this.studentService.changePageMode(this.studentCreateMode);
            this.studentDetailsForParent.emit(data);

          }
        }

      })
    }
  }

  ngOnDestroy() {
    this.imageCropperService.enableUpload(false);
  }

}
