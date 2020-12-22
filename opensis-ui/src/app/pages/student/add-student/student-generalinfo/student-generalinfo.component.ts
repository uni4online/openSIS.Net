import { Component, OnInit, Input, ViewChild, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { StudentService } from '../../../../services/student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { CommonService } from '../../../../services/common.service';
import { CheckStudentInternalIdViewModel, StudentAddModel } from '../../../../models/studentModel';
import { CountryModel } from '../../../../models/countryModel';
import { LanguageModel } from '../../../../models/languageModel';
import { LoginService } from '../../../../services/login.service';
import * as _moment from 'moment';
import { default as _rollupMoment } from 'moment';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MY_FORMATS } from '../../../shared/format-datepicker';
import { SharedFunction } from '../../../shared/shared-function';
import { salutation, suffix, gender, race, ethnicity, maritalStatus } from '../../../../enums/studentAdd.enum';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import icEdit from '@iconify/icons-ic/edit';
import { Subject } from 'rxjs/internal/Subject';
import { takeUntil } from 'rxjs/operators';
import icVisibility from '@iconify/icons-ic/twotone-visibility';
import icVisibilityOff from '@iconify/icons-ic/twotone-visibility-off';
import icCheckbox from '@iconify/icons-ic/baseline-check-box';
import icCheckboxOutline from '@iconify/icons-ic/baseline-check-box-outline-blank';
import { SectionService } from '../../../../services/section.service';
import { GetAllSectionModel, TableSectionList } from '../../../../models/sectionModel';
import { CryptoService } from '../../../../services/Crypto.service';
import { CheckUserEmailAddressViewModel } from '../../../../models/userModel';

@Component({
  selector: 'vex-student-generalinfo',
  templateUrl: './student-generalinfo.component.html',
  styleUrls: ['./student-generalinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms

  ],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class StudentGeneralinfoComponent implements OnInit, OnDestroy {
  StudentCreate = SchoolCreate;
  @Input() studentCreateMode: SchoolCreate;
  @Input() studentDetailsForViewAndEdit;
  @Input() categoryId;
  @ViewChild('f') currentForm: NgForm;
  data;
  icEdit = icEdit;
  icCheckbox = icCheckbox;
  icCheckboxOutline = icCheckboxOutline;
  countryListArr = [];
  countryName = "-";
  sectionName = "-";
  nationality = "-";
  countryModel: CountryModel = new CountryModel();
  destroySubject$: Subject<void> = new Subject();

  form: FormGroup;
  studentAddModel: StudentAddModel = new StudentAddModel();
  checkStudentInternalIdViewModel: CheckStudentInternalIdViewModel = new CheckStudentInternalIdViewModel();
  checkUserEmailAddressViewModel: CheckUserEmailAddressViewModel = new CheckUserEmailAddressViewModel();
  section: GetAllSectionModel = new GetAllSectionModel();
  sectionList: [TableSectionList];
  languages: LanguageModel = new LanguageModel();
  salutationEnum = Object.keys(salutation);
  suffixEnum = Object.keys(suffix);
  eligibility504: boolean = false;
  economicDisadvantage: boolean = false;
  freeLunchEligibility: boolean = false;
  specialEducationIndicator: boolean = false;
  lepIndicator: boolean = false;
  genderEnum = Object.keys(gender);
  module = 'Student';
  saveAndNext = 'saveAndNext';
  pageStatus: string;
  raceEnum = Object.keys(race);
  ethnicityEnum = Object.keys(ethnicity);
  maritalStatusEnum = Object.keys(maritalStatus);
  languageList;
  icVisibility = icVisibility;
  icVisibilityOff = icVisibilityOff;
  inputType = 'password';
  studentInternalId = '';
  studentPortalId = '';
  visible = false;
  pass: string = "";
  hidePasswordAccess: boolean = false;
  hideAccess: boolean = false;
  fieldDisabled: boolean = false;
  studentAge;

  constructor(
    private fb: FormBuilder,
    public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private studentService: StudentService,
    private commonService: CommonService,
    private loginService: LoginService,
    private commonFunction: SharedFunction,
    private sectionService: SectionService,
    private cd: ChangeDetectorRef) {
    translateService.use('en');
    this.studentService.getStudentDetailsForGeneral.pipe(takeUntil(this.destroySubject$)).subscribe((res: StudentAddModel) => {
      this.studentAddModel = res;
      this.studentAddModel.loginEmail = this.studentAddModel.studentMaster.studentPortalId;
      this.data = this.studentAddModel?.studentMaster;
      this.studentInternalId = this.data.studentInternalId;
      this.studentPortalId = this.data.studentPortalId;
      this.accessPortal();

      if (this.studentCreateMode == this.StudentCreate.VIEW) {
        this.renderCheckBox();
        this.studentAge = this.commonFunction.getAge(this.studentAddModel?.studentMaster.dob);
        this.getAllSection();
      }
      this.getAllCountry();
    })
  }
 
  ngOnInit(): void {
    this.getAllSection();
        
    if (this.studentCreateMode == this.StudentCreate.ADD) {
      this.getAllCountry();
      this.GetAllLanguage();
    } else if (this.studentCreateMode == this.StudentCreate.VIEW) {
      this.studentAddModel = this.studentDetailsForViewAndEdit;
      this.data = this.studentDetailsForViewAndEdit?.studentMaster;      
      this.renderCheckBox();
      
    
    } else if (this.studentCreateMode == this.StudentCreate.EDIT && (this.studentDetailsForViewAndEdit != undefined || this.studentDetailsForViewAndEdit != null)) {
      this.studentAddModel = this.studentDetailsForViewAndEdit;

      this.saveAndNext = 'update';
      if (this.studentAddModel.studentMaster.studentPortalId !== null) {

        this.hideAccess = true;
        this.fieldDisabled = true;

      }
    }
  }

  renderCheckBox() {
    if (this.data.eligibility504) {
      this.eligibility504 = true;
    } else {
      this.eligibility504 = false;
    }
    if (this.data.freeLunchEligibility) {
      this.freeLunchEligibility = true;
    } else {
      this.freeLunchEligibility = false;
    }
    if (this.data.economicDisadvantage) {
      this.economicDisadvantage = true;
    } else {
      this.economicDisadvantage = false;
    }
    if (this.data.specialEducationIndicator) {
      this.specialEducationIndicator = true;
    } else {
      this.specialEducationIndicator = false;
    }
    if (this.data.lepIndicator) {
      this.lepIndicator = true;
    } else {
      this.lepIndicator = false;
    }
  }

  accessPortal() {
    if (this.data.studentPortalId !== null) {
      this.hideAccess = true;
      this.fieldDisabled = true;
      this.hidePasswordAccess = false;

    } else {
      this.hideAccess = false;
      this.fieldDisabled = false;
      this.hidePasswordAccess = false;

    }
  }

  getAllSection() {
    this.sectionService.GetAllSection(this.section).subscribe(data => {
      if (data._failure) {
        this.snackbar.open('Section information failed. ' + data._message, 'LOL THANKS', {
          duration: 10000
        });
      } else {
        this.sectionList = data.tableSectionsList;
        if (this.studentCreateMode == this.StudentCreate.VIEW) {
          this.sectionList.map((val) => {
            var sectionNumber = +this.data.sectionId;
            if (val.sectionId === sectionNumber) {
              this.sectionName = val.name;
            }

          })
        }

      }
    });
  }

  isPortalAccess(event) {
    if (event.checked) {
      if (this.studentCreateMode == this.StudentCreate.ADD) {
        this.hidePasswordAccess = true;
      }
      else {
        if (this.data.studentPortalId !== null) {
          this.hidePasswordAccess = false;
        }
        else {
          this.hidePasswordAccess = true;
        }
      }
      this.hideAccess = true;
      this.studentAddModel.portalAccess = true;
    }
    else {
      this.hideAccess = false;
      this.hidePasswordAccess = false;
      this.studentAddModel.portalAccess = false;
    }
  }

  checkLoginEmail(event) {
    let emailId = event.target.value;
    if (this.studentPortalId === emailId) {
      this.currentForm.form.controls.loginEmail.setErrors(null);
    }
    else {
      this.checkUserEmailAddressViewModel.emailAddress = emailId;
      this.loginService.checkUserLoginEmail(this.checkUserEmailAddressViewModel).subscribe(data => {
        if (data.isValidEmailAddress) {
          this.currentForm.form.controls.loginEmail.setErrors(null);
        }
        else {
          this.currentForm.form.controls.loginEmail.setErrors({ 'nomatch': true });
        }
      });
    }
  }

  checkInternalId(event) {
    let internalId = event.target.value;
    if (this.studentInternalId === internalId) {
      this.currentForm.form.controls.studentInternalId.setErrors(null);
    }
    else {
      this.checkStudentInternalIdViewModel.studentInternalId = internalId;
      this.studentService.checkStudentInternalId(this.checkStudentInternalIdViewModel).subscribe(data => {
        if (data.isValidInternalId) {
          this.currentForm.form.controls.studentInternalId.setErrors(null);
        }
        else {
          this.currentForm.form.controls.studentInternalId.setErrors({ 'nomatch': true });
        }
      });
    }
  }

  getAllCountry() {
    this.commonService.GetAllCountry(this.countryModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.countryListArr = [];
      }
      else {
        if (data._failure) {
          this.countryListArr = [];
        } else {
          this.countryListArr = data.tableCountry;
          if (this.studentCreateMode == this.StudentCreate.VIEW) {
            this.countryListArr.map((val) => {
              var countryInNumber = +this.data.countryOfBirth;
              var nationality = +this.data.nationality;
              if (val.id === countryInNumber) {
                this.countryName = val.name;
              }
              if (val.id === nationality) {
                this.nationality = val.name;
              }
            })
          }
        }
      }
    })
  }
  editGeneralInfo() {
    this.studentCreateMode = this.StudentCreate.EDIT
    this.pageStatus = "Edit Student";
  }

  GetAllLanguage() {
    this.languages._tenantName = sessionStorage.getItem("tenant");
    this.loginService.getAllLanguage(this.languages).subscribe((res) => {
      this.languageList = res.tableLanguage;
    })
  }


  generate() {
    this.currentForm.controls.passwordHash.setValue(this.commonFunction.autoGeneratePassword());
  }

  submit() {
    this.currentForm.form.markAllAsTouched();
    if (this.currentForm.controls.passwordHash !== undefined) {
      this.studentAddModel.passwordHash = this.currentForm.controls.passwordHash.value;
    }

    if (this.currentForm.form.valid) {

      if (this.studentCreateMode == this.StudentCreate.EDIT) {
        this.studentAddModel.selectedCategoryId = this.studentAddModel.fieldsCategoryList[this.categoryId].categoryId;

        this.updateStudent();
      } else {
        this.addStudent();
      }
    }
  }

  cancelEdit() {
    this.studentCreateMode = this.StudentCreate.VIEW
    this.data = this.studentAddModel.studentMaster;
  }

  updateStudent() {
    this.studentAddModel._token = sessionStorage.getItem("token");
    this.studentAddModel._tenantName = sessionStorage.getItem("tenant");
    this.studentService.UpdateStudent(this.studentAddModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Student Update failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('Student Update failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.snackbar.open('Student Update Successful.', '', {
            duration: 10000
          });
          this.data = data.studentMaster;
          this.studentCreateMode = this.StudentCreate.VIEW
        }
      }


    });
  }

  addStudent() {
    this.studentAddModel.studentMaster.dob = this.commonFunction.formatDateSaveWithoutTime(this.studentAddModel.studentMaster.dob);
    this.studentService.AddStudent(this.studentAddModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Student Save failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('Student Save failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.snackbar.open('Student Save Successful.', '', {
            duration: 10000
          });
          this.studentService.setStudentId(data.studentMaster.studentId);
          this.studentService.changeCategory(4);
          this.studentService.setStudentDetails(data);
        }
      }

    })
  }

  toggleVisibility() {
    if (this.visible) {
      this.inputType = 'password';
      this.visible = false;
      this.cd.markForCheck();
    } else {
      this.inputType = 'text';
      this.visible = true;
      this.cd.markForCheck();
    }
  }

  ngOnDestroy() {
    this.destroySubject$.next();
  }


}
