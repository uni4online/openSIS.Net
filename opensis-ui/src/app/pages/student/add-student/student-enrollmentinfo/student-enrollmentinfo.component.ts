import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm} from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import icAdd from '@iconify/icons-ic/baseline-add';
import icClear from '@iconify/icons-ic/baseline-clear';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { RollingOptionsEnum } from '../../../../enums/rolling-retention-option.enum';
import { CalendarListModel } from '../../../../models/calendarModel';
import { CalendarService } from '../../../../services/calendar.service';
import { StudentEnrollmentDetails, StudentEnrollmentModel, StudentEnrollmentSchoolListModel } from '../../../../models/studentModel';
import { StudentService } from '../../../../services/student.service';
import { EnrollmentCodesService } from '../../../../services/enrollment-codes.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import icEdit from '@iconify/icons-ic/edit';
import icCheckbox from '@iconify/icons-ic/baseline-check-box';
import icCheckboxOutline from '@iconify/icons-ic/baseline-check-box-outline-blank';
import icPromoted from '@iconify/icons-ic/baseline-north';
import icExternal from '@iconify/icons-ic/baseline-undo';
import icRetained from '@iconify/icons-ic/baseline-replay';
import icHomeSchool from '@iconify/icons-ic/baseline-home';
import icExpand from '@iconify/icons-ic/baseline-arrow-drop-up';
import icCollapse from '@iconify/icons-ic/baseline-arrow-drop-down';
import icTrasnferIn from '@iconify/icons-ic/baseline-call-received';
import icTrasnferOut from '@iconify/icons-ic/baseline-call-made';
import icDrop from '@iconify/icons-ic/vertical-align-bottom';
import { EnrollmentCodeListView } from '../../../../models/enrollmentCodeModel';
import { Router } from '@angular/router';

@Component({
  selector: 'vex-student-enrollmentinfo',
  templateUrl: './student-enrollmentinfo.component.html',
  styleUrls: ['./student-enrollmentinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ],
})
export class StudentEnrollmentinfoComponent implements OnInit, OnDestroy {
  icAdd = icAdd;
  icClear = icClear;
  icEdit = icEdit;
  icCheckbox = icCheckbox;
  icCheckboxOutline = icCheckboxOutline;
  icPromoted = icPromoted;
  icExternal = icExternal;
  icHomeSchool = icHomeSchool;
  icExpand = icExpand;
  icCollapse = icCollapse;
  icRetained = icRetained;
  icTrasnferIn = icTrasnferIn;
  icTrasnferOut = icTrasnferOut;
  icDrop = icDrop;
  studentCreate = SchoolCreate;
  @Input() studentCreateMode: SchoolCreate;
  @Input() studentDetailsForViewAndEdit;
  rollingOptions = Object.keys(RollingOptionsEnum);
  exitCodes=[];
  calendarListModel: CalendarListModel = new CalendarListModel();
  studentEnrollmentModel: StudentEnrollmentModel = new StudentEnrollmentModel();
  schoolListWithGrades: StudentEnrollmentSchoolListModel = new StudentEnrollmentSchoolListModel();
  enrollmentCodeListView: EnrollmentCodeListView = new EnrollmentCodeListView();
  @ViewChild('form') currentForm: NgForm;
  
  divCount = [1];
  schoolListWithGradeLevelsAndEnrollCodes = [];
  selectedSchoolIndex = [];
  selectedTransferredSchoolIndex = [];
  calendarNameInViewMode: string="-";
  filteredEnrollmentInfoInViewMode;
  expandEnrollmentHistory=true;
  selectedExitCodes=[];
  selectedEnrollmentCodes=[];
  disableEditDueToActiveExitCode=false;
  cloneStudentEnrollment;
  constructor(private calendarService: CalendarService,
    private studentService: StudentService,
    private enrollmentCodesService: EnrollmentCodesService,
    private snackbar: MatSnackBar,
    private router:Router) {
     }

  ngOnInit(): void {
    if (this.studentCreateMode == this.studentCreate.EDIT) {
      this.studentCreateMode = this.studentCreate.ADD;
    }
    if (this.studentCreateMode == this.studentCreate.ADD) {
      this.getAllCalendar();
      this.getAllSchoolListWithGradeLevelsAndEnrollCodes();
      this.getAllStudentEnrollments();
      this.studentService.changePageMode(this.studentCreateMode);
    } else if (this.studentCreateMode == this.studentCreate.VIEW) {
      this.getAllCalendar();
      // this.getAllSchoolListWithGradeLevels();
      this.getAllStudentEnrollments();
      this.studentService.changePageMode(this.studentCreateMode);
    }
  }

  cmpare(index) {
    return index;
  }

  onSchoolChange(schoolId, indexOfDynamicRow) {
    let index = this.schoolListWithGradeLevelsAndEnrollCodes.findIndex((x) => {
      return x.schoolId === +schoolId;
    });
    this.selectedSchoolIndex[indexOfDynamicRow] = index;
    this.cloneStudentEnrollment[indexOfDynamicRow].schoolName = this.schoolListWithGradeLevelsAndEnrollCodes[index].schoolName;
  }

  onTransferredSchoolChange(transferredSchoolId, indexOfDynamicRow) {
    let index = this.schoolListWithGradeLevelsAndEnrollCodes.findIndex((x) => {
      return x.schoolId === +transferredSchoolId;
    });
    this.selectedTransferredSchoolIndex[indexOfDynamicRow] = index;
    this.cloneStudentEnrollment[indexOfDynamicRow].schoolTransferred = this.schoolListWithGradeLevelsAndEnrollCodes[index].schoolName;

  }

  onEnrollmentCodeChange(enrollmentCode,indexOfDynamicRow){

  }

  onExitCodeChange(value,indexOfDynamicRow){
  this.schoolListWithGradeLevelsAndEnrollCodes[this.selectedSchoolIndex[indexOfDynamicRow]].studentEnrollmentCode?.map((item)=>{
    if(item.enrollmentCode==+value){
      this.selectedExitCodes[indexOfDynamicRow]=item.type;
    }
  })
  
  }

  addMoreEnrollments() {
    this.cloneStudentEnrollment.push(new StudentEnrollmentDetails)
    this.divCount.push(2); // Why 2? We have to fill up the divCount, It could be anything.
  }

  deleteDynamicRow(indexOfDynamicRow) {
    this.divCount.splice(indexOfDynamicRow, 1);
    this.cloneStudentEnrollment.splice(indexOfDynamicRow, 1);
    this.selectedSchoolIndex.splice(indexOfDynamicRow, 1);
    this.selectedTransferredSchoolIndex.splice(indexOfDynamicRow, 1);
  }

  getAllCalendar() {
    this.calendarService.getAllCalendar(this.calendarListModel).subscribe((res) => {
      let allCalendarsInCurrentSchool=res.calendarList;
      this.calendarListModel.calendarList= allCalendarsInCurrentSchool.filter((x)=>{
          return (x.academicYear==+sessionStorage.getItem("academicyear") && x.defaultCalender);
      })
    });
  }


 

  getAllSchoolListWithGradeLevelsAndEnrollCodes() {
    this.studentService.studentEnrollmentSchoolList(this.schoolListWithGrades).subscribe(res => {
      this.schoolListWithGradeLevelsAndEnrollCodes = res.schoolMaster;
      for (let i = 0; i < this.cloneStudentEnrollment.length; i++) {
        this.selectedSchoolIndex[i] = this.schoolListWithGradeLevelsAndEnrollCodes.findIndex((x) => {
          return x.schoolId==+this.cloneStudentEnrollment[i].schoolId;
        });
        this.selectedTransferredSchoolIndex[i] = this.schoolListWithGradeLevelsAndEnrollCodes.findIndex((x) => {
          return x.schoolId==+this.cloneStudentEnrollment[i].transferredSchoolId;
        })
      }
      this.findEnrollmentCodeIdByName();
      // this.findExitCodeIdByName();
    });

  }

  editEnrollmentInfo(){
    this.getAllSchoolListWithGradeLevelsAndEnrollCodes();
    this.studentCreateMode = this.studentCreate.EDIT
    this.studentService.changePageMode(this.studentCreateMode);
  }
  cancelEdit(){
    this.studentCreateMode = this.studentCreate.VIEW
    this.replaceEnrollmentCodeWithName();
    this.studentService.changePageMode(this.studentCreateMode);
  }
  replaceEnrollmentCodeWithName(){
    for(let i=0;i<this.studentEnrollmentModel.studentEnrollmentListForView?.length;i++){
      let index = this.schoolListWithGradeLevelsAndEnrollCodes.findIndex((x)=>{
        return x.schoolId == +this.studentEnrollmentModel.studentEnrollmentListForView[i].schoolId;
      });
      for(let j=0;j<this.schoolListWithGradeLevelsAndEnrollCodes[index].studentEnrollmentCode?.length;j++){
        if(this.studentEnrollmentModel.studentEnrollmentListForView[i].enrollmentCode==this.schoolListWithGradeLevelsAndEnrollCodes[index].studentEnrollmentCode[j].enrollmentCode.toString()){
          this.studentEnrollmentModel.studentEnrollmentListForView[i].enrollmentCode=this.schoolListWithGradeLevelsAndEnrollCodes[index].studentEnrollmentCode[j].title;
          break;
        }
      } 
    }
  }


  toggleEnrollmentHistory(){
    this.expandEnrollmentHistory=!this.expandEnrollmentHistory;
  }


  getAllStudentEnrollments() {
    if (this.studentCreateMode == this.studentCreate.ADD) {
      let studentDetails=this.studentService.getStudentDetails()
      this.studentEnrollmentModel.studentGuid=studentDetails.studentMaster.studentGuid;
    }
    this.studentEnrollmentModel.studentId = this.studentService.getStudentId();
    this.studentEnrollmentModel.tenantId = sessionStorage.getItem("tenantId");
    this.studentEnrollmentModel.schoolId=+sessionStorage.getItem("selectedSchoolId");
    if (this.studentCreateMode == this.studentCreate.VIEW) {
      this.studentEnrollmentModel.studentGuid=this.studentDetailsForViewAndEdit.studentMaster.studentGuid;
    }

    this.studentService.getAllStudentEnrollment(this.studentEnrollmentModel).subscribe(res => {
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Student Enrollments failed to fetch. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          this.snackbar.open('Student Enrollments failed to fetch. ' + res._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.studentEnrollmentModel = res;
          this.cloneStudentEnrollment= res.studentEnrollmentListForView.filter((item)=>{
            return item.exitCode==null;
          });
          for(let i=0;i<this.cloneStudentEnrollment?.length;i++){
            this.divCount[i]=i;
          }
          this.manipulateModelInEditMode();
        }
      }
    })
  }


  findEnrollmentCodeIdByName(){
      for(let i=0;i<this.studentEnrollmentModel.studentEnrollmentListForView?.length;i++){
        let index = this.schoolListWithGradeLevelsAndEnrollCodes.findIndex((x)=>{
          return x.schoolId == +this.studentEnrollmentModel.studentEnrollmentListForView[i].schoolId;
        });
        for(let j=0;j<this.schoolListWithGradeLevelsAndEnrollCodes[index].studentEnrollmentCode?.length;j++){
          if(this.studentEnrollmentModel.studentEnrollmentListForView[i].enrollmentCode==this.schoolListWithGradeLevelsAndEnrollCodes[index].studentEnrollmentCode[j].title){
            this.studentEnrollmentModel.studentEnrollmentListForView[i].enrollmentCode=this.schoolListWithGradeLevelsAndEnrollCodes[index].studentEnrollmentCode[j].enrollmentCode.toString();
            break;
          }
        }
        
      }
  }

  findCalendarNameById(){
    if (this.calendarListModel.calendarList != null) {
      for (let i = 0; i < this.calendarListModel.calendarList?.length; i++) {
        if (this.studentEnrollmentModel.calenderId != null && this.calendarListModel.calendarList[i].calenderId.toString() == this.studentEnrollmentModel.calenderId.toString()) {
          this.calendarNameInViewMode = this.calendarListModel.calendarList[i].title;
        } else {
          this.calendarNameInViewMode = '-';
        }
      }
    }
  }

  manipulateModelInEditMode() {
    this.filteredEnrollmentInfoInViewMode=this.studentEnrollmentModel.studentEnrollmentListForView.filter((x)=>{
      return x.exitCode==null;
    });
    if(this.studentEnrollmentModel.calenderId!=null){
      for (let i = 0; i < this.studentEnrollmentModel.studentEnrollmentListForView?.length; i++) {
        this.studentEnrollmentModel.calenderId = this.studentEnrollmentModel.calenderId.toString();
      }
    }
    for (let i = 0; i < this.cloneStudentEnrollment.length; i++) {
      this.selectedExitCodes[i]=null;
      this.cloneStudentEnrollment[i].schoolId=this.studentEnrollmentModel.studentEnrollmentListForView[i].schoolId.toString();
      }
      this.findCalendarNameById();
  }

  updateStudentEnrollment(){
    this.currentForm.form.markAllAsTouched();
    if(this.currentForm.form.invalid){
      return
    }
    delete this.studentEnrollmentModel.studentEnrollmentListForView;
      this.studentEnrollmentModel.studentEnrollments=this.cloneStudentEnrollment;
    let details=this.studentService.getStudentDetails();
    for(let i=0;i<this.studentEnrollmentModel.studentEnrollments?.length;i++){
      this.studentEnrollmentModel.studentEnrollments[i].studentId=this.studentService.getStudentId();
        this.studentEnrollmentModel.studentEnrollments[i].academicYear=+sessionStorage.getItem("academicyear");
        if(details!=null){
          this.studentEnrollmentModel.studentEnrollments[i].studentGuid=details.studentMaster.studentGuid;

        }else{
        this.studentEnrollmentModel.studentEnrollments[i].studentGuid=this.studentDetailsForViewAndEdit.studentMaster.studentGuid;

        }
    }
    this.studentEnrollmentModel.studentGuid=this.studentDetailsForViewAndEdit.studentMaster.studentGuid;
    this.studentEnrollmentModel.academicYear=sessionStorage.getItem("academicyear");
    this.studentEnrollmentModel.schoolId=+sessionStorage.getItem("selectedSchoolId");
    this.studentService.updateStudentEnrollment(this.studentEnrollmentModel).subscribe((res)=>{
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Enrollment Update failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          this.snackbar.open('Enrollment Update failed. ' + res._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.snackbar.open('Enrollment Update Successful.', '', {
            duration: 10000
          });
          for(let i=0;i<res.studentEnrollments?.length;i++){
            if(res.studentEnrollments[i].enrollmentCode=="Transferred In"){
             this.router.navigate(["school/students"]);
            }
          }
          this.getAllStudentEnrollments();
          // this.studentEnrollmentModel=res;
          // this.replaceEnrollmentCodeWithName();
          this.studentCreateMode = this.studentCreate.VIEW;
          this.studentService.changePageMode(this.studentCreateMode);
          

        }
      }
    })
  }

  ngOnDestroy() {

  }
}
