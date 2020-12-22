import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
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
export class StudentMedicalinfoComponent implements OnInit {
  StudentCreate = SchoolCreate;
  @Input() studentCreateMode: SchoolCreate;
  @Input() categoryId;
  @Input() studentDetailsForViewAndEdit;
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
    private parentInfoService: ParentInfoService) {
    translateService.use('en');

  }

  ngOnInit(): void {
    if (this.studentCreateMode == this.StudentCreate.VIEW) {
      this.studentAddModel = this.studentDetailsForViewAndEdit;
    } else {
      this.getAllParents();
      this.studentAddModel = this.studentService.getStudentDetails();
    }
  }

  editMedicalInfo() {
    this.getAllParents();
    this.studentCreateMode = this.StudentCreate.EDIT
  }

  cancelEdit() {
    this.studentCreateMode = this.StudentCreate.VIEW
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
    this.studentAddModel.selectedCategoryId= this.studentAddModel.fieldsCategoryList[this.categoryId].categoryId;
    
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
          this.snackbar.open('Medical Information Update Successfully.', '', {
            duration: 10000
          });
          this.studentDetailsForParent.emit(data);

        }
      }

    })
  }

}
