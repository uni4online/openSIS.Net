import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';
import { ImageCropperService } from 'src/app/services/image-cropper.service';
import { SchoolAddViewModel } from '../../../models/schoolMasterModel';
import { ActivatedRoute } from '@angular/router';
import { SchoolService } from '../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedFunction } from '../../../pages/shared/shared-function';
import { LayoutService } from 'src/@vex/services/layout.service';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import icSchool from '@iconify/icons-ic/outline-school';
import icCalendar from '@iconify/icons-ic/outline-calendar-today';
import icAlarm from '@iconify/icons-ic/outline-alarm';
import icPoll from '@iconify/icons-ic/outline-poll';
import icAccessibility from '@iconify/icons-ic/outline-accessibility';
import icHowToReg from '@iconify/icons-ic/outline-how-to-reg';
import icBilling from '@iconify/icons-ic/outline-monetization-on';
import { StudentService } from '../../../services/student.service';
import { StudentAddModel } from '../../../models/studentModel';
import { CustomFieldService } from '../../../services/custom-field.service';
import { FieldsCategoryListView } from '../../../models/fieldsCategoryModel';
import { SchoolCreate } from '../../../enums/school-create.enum';
import icHospital from '@iconify/icons-ic/round-local-hospital';
import { takeUntil } from 'rxjs/operators';
import { LoaderService } from '../../../services/loader.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'vex-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.scss'],
  animations: [
    fadeInRight400ms,
    stagger60ms,
    fadeInUp400ms
  ]
})
export class AddStudentComponent implements OnInit, OnDestroy {
  StudentCreate = SchoolCreate;
  studentCreateMode: SchoolCreate = SchoolCreate.ADD;
  fieldsCategoryListView = new FieldsCategoryListView();
  currentCategory: number = 3; // because 3 is the id of general info.
  indexOfCategory: number = 0;
  icSchool = icSchool;
  icCalendar = icCalendar;
  icAlarm = icAlarm;
  icPoll = icPoll;
  icAccessibility = icAccessibility;
  icHowToReg = icHowToReg;
  icBilling = icBilling;
  icHospital = icHospital;
  studentId: number;
  studentTitle = "Add Student Information";
  pageStatus = "Add Student"
  module = 'Student';
  responseImage: string;
  enableCropTool = true;
  studentAddModel: StudentAddModel = new StudentAddModel();
  fieldsCategory = [];
  criticalAlert = false;
  destroySubject$: Subject<void> = new Subject();
  loading: boolean;
  constructor(private layoutService: LayoutService,
    private studentService: StudentService,
    private snackbar: MatSnackBar,
    private customFieldservice: CustomFieldService,
    private imageCropperService: ImageCropperService,
    private loaderService: LoaderService,
    private cdr: ChangeDetectorRef) {
    this.layoutService.collapseSidenav();
    this.imageCropperService.getCroppedEvent().pipe(takeUntil(this.destroySubject$)).subscribe((res) => {
      this.studentService.setStudentImage(res[1]);
    });
    this.studentService.categoryToSend.pipe(takeUntil(this.destroySubject$)).subscribe((res: number) => {
      this.currentCategory = res;
    });
    this.loaderService.isLoading.pipe(takeUntil(this.destroySubject$)).subscribe((currentState) => {
      this.loading = currentState;
    });
  }

  ngOnInit(): void {
    this.studentCreateMode = this.StudentCreate.ADD
    this.studentId = this.studentService.getStudentId();
    if (this.studentId != null || this.studentId != undefined) {
      this.studentCreateMode = this.StudentCreate.VIEW;
      this.getStudentDetailsUsingId();
      this.onViewMode();
    } else if (this.studentCreateMode == this.StudentCreate.ADD) {
      this.getAllFieldsCategory();
    }

  }

  onViewMode() {
    this.studentService.setStudentImage(this.responseImage);
    this.pageStatus = "View Student"
  }

  checkCriticalAlertFromMedical(event) {
    if (event.studentMaster.criticalAlert?.length > 0) {
      this.criticalAlert = true;
    } else {
      this.criticalAlert = false;
    }
  }

  changeCategory(field, index) {
    let studentDetails = this.studentService.getStudentDetails();

    if (studentDetails != undefined || studentDetails != null) {
      this.studentCreateMode = this.StudentCreate.EDIT;
      this.currentCategory = field.categoryId;
      this.indexOfCategory = index;
      this.studentAddModel = studentDetails;
    }

    if (this.studentCreateMode == this.StudentCreate.VIEW) {
      this.currentCategory = field.categoryId;
      this.indexOfCategory = index;
      this.pageStatus = "View Student"
    }
  }

  getAllFieldsCategory() {
    this.fieldsCategoryListView.module = "Student";
    this.fieldsCategoryListView.schoolId = +sessionStorage.getItem('selectedSchoolId');
    this.customFieldservice.getAllFieldsCategory(this.fieldsCategoryListView).subscribe((res) => {
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Category list failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          this.snackbar.open('Cateogy list failed. ' + res._message, 'LOL THANKS', {
            duration: 10000
          });
        }
        else {
          this.fieldsCategory = res.fieldsCategoryList.filter(x => x.isSystemCategory == true);
        }
      }
    }
    );
  }

  getStudentDetailsUsingId() {
    this.studentAddModel.studentMaster.studentId = this.studentId;
    this.studentService.viewStudent(this.studentAddModel).subscribe(data => {
      this.studentAddModel = data;
      this.fieldsCategory = data.fieldsCategoryList;
      this.studentService.sendDetails(this.studentAddModel);
      this.responseImage = this.studentAddModel.studentMaster.studentPhoto;
      this.studentTitle = this.studentAddModel.studentMaster.firstGivenName + " " + this.studentAddModel.studentMaster.lastFamilyName;
      this.studentService.setStudentImage(this.responseImage);
      this.checkCriticalAlertFromMedical(this.studentAddModel);
    });
  }

  ngAfterViewChecked() {
    this.cdr.detectChanges();
  }

  ngOnDestroy() {
    this.studentService.setStudentDetails(null);
    this.studentService.setStudentImage(null);
    this.studentService.setStudentId(null);
    this.destroySubject$.next();
  }

}
