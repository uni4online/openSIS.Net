import { Component, OnInit, ChangeDetectorRef, ViewChild, Input, ComponentFactoryResolver } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icAdd from '@iconify/icons-ic/baseline-add';
import icClear from '@iconify/icons-ic/baseline-clear';
import icEdit from '@iconify/icons-ic/edit';
import icVisibility from '@iconify/icons-ic/twotone-visibility';
import icVisibilityOff from '@iconify/icons-ic/twotone-visibility-off';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { CheckStaffInternalIdViewModel, StaffAddModel } from '../../../../models/staffModel';
import { LanguageModel } from '../../../../models/languageModel';
import { ethnicity, gender, maritalStatus, race, salutation, suffix } from '../../../../enums/studentAdd.enum';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonService } from '../../../../services/common.service';
import { LoginService } from '../../../../services/login.service';
import { SharedFunction } from '../../../../pages/shared/shared-function';
import { CountryModel } from '../../../../models/countryModel';
import { StaffService } from '../../../../services/staff.service';
import { CheckUserEmailAddressViewModel } from '../../../../models/userModel';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { WashInfoEnum } from '../../../../enums/wash-info.enum';

@Component({
  selector: 'vex-staff-generalinfo',
  templateUrl: './staff-generalinfo.component.html',
  styleUrls: ['./staff-generalinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StaffGeneralinfoComponent implements OnInit {
  staffCreate = SchoolCreate;
  @Input() staffDetailsForViewAndEdit;
  @Input() categoryId;
  @ViewChild('f') currentForm: NgForm;
  @Input() staffCreateMode: SchoolCreate;
  countryModel: CountryModel = new CountryModel();
  staffAddModel: StaffAddModel = new StaffAddModel();
  languages: LanguageModel = new LanguageModel();
  checkStaffInternalIdViewModel: CheckStaffInternalIdViewModel = new CheckStaffInternalIdViewModel();
  checkUserEmailAddressViewModel: CheckUserEmailAddressViewModel = new CheckUserEmailAddressViewModel();
  salutationEnum = Object.keys(salutation);
  suffixEnum = Object.keys(suffix);
  genderEnum = Object.keys(gender);
  raceEnum = Object.keys(race);
  ethnicityEnum = Object.keys(ethnicity);
  maritalStatusEnum = Object.keys(maritalStatus);
  destroySubject$: Subject<void> = new Subject();
  languageList = [];
  disablity = WashInfoEnum;
  countryListArr = [];
  countryName = '-';
  nationality = '-';
  icAdd = icAdd;
  icEdit = icEdit;
  icClear = icClear;
  icVisibility = icVisibility;
  icVisibilityOff = icVisibilityOff;
  inputType = 'password';
  module = 'Staff';
  visible = false;
  staffInternalId = '';
  data: any;
  hidePasswordAccess: boolean = false;
  firstLanguageName : string;
  secondLanguageName : string;
  thirdLanguageName : string;
  hideAccess: boolean = false;
  fieldDisabled: boolean = false;
  saveAndNext = 'saveAndNext';
  pageStatus: string;
  staffPortalId: string;

  constructor(private fb: FormBuilder,
    public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private commonService: CommonService,
    private loginService: LoginService,
    private commonFunction: SharedFunction,
    private staffService: StaffService,
    private cd: ChangeDetectorRef) {
    translateService.use('en');

    this.staffService.getStaffDetailsForGeneral.pipe(takeUntil(this.destroySubject$)).subscribe((res: StaffAddModel) => {
      this.staffAddModel = res;
      this.data = this.staffAddModel?.staffMaster;
      this.staffInternalId = this.data.staffInternalId;
      this.staffPortalId = this.data.loginEmailAddress;
      this.accessPortal();
      this.GetAllLanguage();
      this.getAllCountry();
    })

  }

  ngOnInit(): void {
    if (this.staffCreateMode == this.staffCreate.ADD) {
      this.getAllCountry();
      this.GetAllLanguage();
    } else if (this.staffCreateMode == this.staffCreate.VIEW) {

      this.staffAddModel = this.staffDetailsForViewAndEdit;
      this.data = this.staffDetailsForViewAndEdit?.staffMaster;
    } else if (this.staffCreateMode == this.staffCreate.EDIT && (this.staffDetailsForViewAndEdit != undefined || this.staffDetailsForViewAndEdit != null)) {
      this.staffAddModel = this.staffDetailsForViewAndEdit;

      this.saveAndNext = 'update';
      if (this.staffAddModel.staffMaster.loginEmailAddress !== null) {

        this.hideAccess = true;
        this.fieldDisabled = true;

      }
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
          if (this.staffCreateMode == this.staffCreate.VIEW) {
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

  GetAllLanguage() {
    this.languages._tenantName = sessionStorage.getItem("tenant");
    this.loginService.getAllLanguage(this.languages).subscribe((res) => {
      if (typeof (res) == 'undefined') {
        this.languageList = [];
      }
      else {
        this.languageList = res.tableLanguage;

        if (this.staffCreateMode == this.staffCreate.VIEW) {
          this.languageList.map((val) => {
            var firstLanguageId = + this.data.firstLanguage;
            var secondLanguageId = + this.data.secondLanguage;
            var thirdLanguageId = + this.data.thirdLanguage;
            if (val.id === firstLanguageId) {
              this.firstLanguageName = val.name;
            }
            if (val.id === secondLanguageId) {
              this.secondLanguageName = val.name;
            }
            if (val.id === thirdLanguageId) {
              this.thirdLanguageName = val.name;
            }
          })
        }
      }

    })
  }

  checkInternalId(event) {
    let internalId = event.target.value;
    if (this.staffInternalId === internalId) {
      this.currentForm.form.controls.staffInternalId.setErrors(null);
    }
    else {
      this.checkStaffInternalIdViewModel.staffInternalId = internalId;
      this.staffService.checkStaffInternalId(this.checkStaffInternalIdViewModel).subscribe(data => {
        if (data.isValidInternalId) {
          this.currentForm.form.controls.staffInternalId.setErrors(null);
        }
        else {
          this.currentForm.form.controls.staffInternalId.setErrors({ 'nomatch': true });
        }
      });
    }
  }
  accessPortal() {
    if (this.staffPortalId !== null) {
      this.hideAccess = true;
      this.fieldDisabled = true;
      this.hidePasswordAccess = false;

    } else {
      this.hideAccess = false;
      this.fieldDisabled = false;
      this.hidePasswordAccess = false;

    }
  }
  generate() {
    this.currentForm.controls.passwordHash.setValue(this.commonFunction.autoGeneratePassword());
  }

  isPortalAccess(event) {
    if (event.checked) {
      if (this.staffCreateMode == this.staffCreate.ADD) {
        this.hidePasswordAccess = true;
      }
      else {
        if (this.staffPortalId !== null) {
          this.hidePasswordAccess = false;
        }
        else {
          this.hidePasswordAccess = true;
        }
      }
      this.hideAccess = true;
      this.staffAddModel.staffMaster.portalAccess = true;
    }
    else {
      this.hideAccess = false;
      this.hidePasswordAccess = false;
      this.staffAddModel.staffMaster.portalAccess = false;
    }
  }

  editGeneralInfo() {
    this.staffCreateMode = this.staffCreate.EDIT
    this.pageStatus = "Edit Staff";
  }

  getAge(birthDate) {
    return this.commonFunction.getAge(birthDate);
  }

  checkLoginEmail(event) {
    let emailId = event.target.value;
    if (this.staffPortalId === emailId) {
      this.currentForm.form.controls.loginEmail.setErrors(null);
    }
    else {
      this.checkUserEmailAddressViewModel.emailAddress = emailId;
      this.loginService.checkUserLoginEmail(this.checkUserEmailAddressViewModel).subscribe(data => {
        if (data.isValidEmailAddress) {
          this.currentForm.form.controls.loginEmailAddress.setErrors(null);
        }
        else {
          this.currentForm.form.controls.loginEmailAddress.setErrors({ 'nomatch': true });
        }
      });
    }
  }

  submitStaff() {
    this.currentForm.form.markAllAsTouched();
    if (this.currentForm.controls.passwordHash !== undefined) {
      this.staffAddModel.passwordHash = this.currentForm.controls.passwordHash.value;
    }

    if (this.currentForm.form.valid) {

      if (this.staffCreateMode == this.staffCreate.EDIT) {
        this.staffAddModel.selectedCategoryId = this.staffAddModel.fieldsCategoryList[this.categoryId].categoryId;

        this.updateStaff();
      } else {
        this.addStaff();
      }
    }
  }

  addStaff() {
    this.staffAddModel.staffMaster.dob = this.commonFunction.formatDateSaveWithoutTime(this.staffAddModel.staffMaster.dob);
    this.staffService.addStaff(this.staffAddModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Staff Save failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('Staff Save failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.snackbar.open('Staff Save Successful.', '', {
            duration: 10000
          });
          this.staffService.setStaffId(data.staffMaster.staffId);
          this.staffService.changeCategory(13);
          this.staffService.setStaffDetails(data);
        }
      }

    })
  }

  updateStaff() {
    this.staffAddModel.selectedCategoryId = this.staffAddModel.fieldsCategoryList[this.categoryId].categoryId;
    this.staffAddModel._token = sessionStorage.getItem("token");
    this.staffAddModel._tenantName = sessionStorage.getItem("tenant");
    this.staffAddModel.staffMaster.dob = this.commonFunction.formatDateSaveWithoutTime(this.staffAddModel.staffMaster.dob);
    this.staffService.updateStaff(this.staffAddModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Staff Save failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('Staff Update failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.snackbar.open('Staff Update Successful.', '', {
            duration: 10000
          });
          // this.staffService.setStudentId(data.studentMaster.studentId);
          // this._studentService.changeCategory(4);
          // this._studentService.setStudentDetails(data);
        }
      }

    })
  }

  cancelEdit() {
    this.staffCreateMode = this.staffCreate.VIEW
    this.data = this.staffAddModel.staffMaster;
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
