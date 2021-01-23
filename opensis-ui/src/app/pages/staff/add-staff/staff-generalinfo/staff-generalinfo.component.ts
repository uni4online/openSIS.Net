import { Component, OnInit, ChangeDetectorRef, ViewChild, Input, ComponentFactoryResolver, ElementRef, Output, EventEmitter } from '@angular/core';
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
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { WashInfoEnum } from '../../../../enums/wash-info.enum';
import { ImageCropperService } from '../../../../services/image-cropper.service';
import { LovList } from '../../../../models/lovModel';
import * as cloneDeep from 'lodash/cloneDeep';
import { MiscModel } from '../../../../models/misc-data-student.model';
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
  @Output() dataAfterSavingGeneralInfo = new EventEmitter<any>();
  @ViewChild('f') currentForm: NgForm;
  @Input() staffCreateMode: SchoolCreate;
  nameOfMiscValuesForView:MiscModel=new MiscModel();
  countryModel: CountryModel = new CountryModel();
  staffAddModel: StaffAddModel = new StaffAddModel();
  languages: LanguageModel = new LanguageModel();
  lovListViewModel: LovList = new LovList()
  checkStaffInternalIdViewModel: CheckStaffInternalIdViewModel = new CheckStaffInternalIdViewModel();
  checkUserEmailAddressViewModel: CheckUserEmailAddressViewModel = new CheckUserEmailAddressViewModel();
  destroySubject$: Subject<void> = new Subject();
  languageList = [];
  ethnicityList = [];
  raceList = [];
  genderList = [];
  suffixList = [];
  salutationList = [];
  maritalStatusList = [];
  disablity = WashInfoEnum;
  staffPortalAccess: string;
  countryListArr = [];
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
  hideAccess: boolean = false;
  fieldDisabled: boolean = false;
  saveAndNext = 'saveAndNext';
  pageStatus: string;
  staffPortalId: string;
  showDisabilityDescription: boolean = false;
  internalId:FormControl;
  loginEmail:FormControl;
  cloneStaffAddModel;
  constructor(private fb: FormBuilder,
    private el: ElementRef,
    public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private commonService: CommonService,
    private loginService: LoginService,
    private commonFunction: SharedFunction,
    private staffService: StaffService,
    private cd: ChangeDetectorRef,
    private imageCropperService: ImageCropperService) {
    translateService.use('en');

    this.staffService.getStaffDetailsForGeneral.pipe(takeUntil(this.destroySubject$)).subscribe((res: StaffAddModel) => {
      this.staffAddModel = res;
      this.data = this.staffAddModel?.staffMaster;
      this.cloneStaffAddModel=JSON.stringify(this.staffAddModel);
      this.staffInternalId = this.data.staffInternalId;
      this.staffPortalId = this.data.loginEmailAddress;
      if(this.staffAddModel.staffMaster?.staffId){
        this.accessPortal();
        this.GetAllLanguage();
        this.getAllCountry();
        this.getAllGender();
        this.getAllSalutation();
        this.getAllSuffix();
        this.getAllMaritalStatus();
      }

    })

  }

  ngOnInit(): void {
    this.internalId = new FormControl('',Validators.required);
    this.loginEmail = new FormControl('',Validators.required);
    if (this.staffCreateMode == this.staffCreate.ADD) {
      this.initializeDropdowns();
    } else if (this.staffCreateMode == this.staffCreate.VIEW) {
      this.staffService.changePageMode(this.staffCreateMode);
      this.getAllCountry();
      this.GetAllLanguage();
      this.imageCropperService.enableUpload(false);
      this.staffAddModel = this.staffDetailsForViewAndEdit;
      this.data = this.staffDetailsForViewAndEdit?.staffMaster;
      this.cloneStaffAddModel=JSON.stringify(this.staffAddModel);
    } else if (this.staffCreateMode == this.staffCreate.EDIT && (this.staffDetailsForViewAndEdit != undefined || this.staffDetailsForViewAndEdit != null)) {
      this.staffAddModel = this.staffDetailsForViewAndEdit;
      this.cloneStaffAddModel=JSON.stringify(this.staffAddModel);
      this.initializeDropdowns();
      this.staffService.changePageMode(this.staffCreateMode);
      this.imageCropperService.enableUpload(true);
      this.saveAndNext = 'update';
      if (this.staffAddModel.staffMaster.loginEmailAddress !== null) {
        this.hideAccess = true;
        this.fieldDisabled = true;

      }
    }
  }

  initializeDropdowns(){
     this.getAllCountry();
      this.GetAllLanguage();
      this.getAllEthnicity();
      this.getAllRace();
      this.getAllGender();
      this.getAllSalutation();
      this.getAllSuffix();
      this.getAllMaritalStatus();
  }

  ngAfterViewInit(){
    // For Checking Internal Id
    this.internalId.valueChanges.pipe(debounceTime(500),distinctUntilChanged()).subscribe((term)=>{
      if(term!=''){
        if (this.staffInternalId === term) {
          this.internalId.setErrors(null);
        }
        else {
          this.checkStaffInternalIdViewModel.staffInternalId = term;
          this.staffService.checkStaffInternalId(this.checkStaffInternalIdViewModel).subscribe(data => {
            if (data.isValidInternalId) {
              this.internalId.setErrors(null);
            }
            else {
              this.internalId.markAsTouched();
              this.internalId.setErrors({ 'nomatch': true });
            }
          });
        }
      }else{
        this.internalId.markAsTouched();
      }
    });

    // For Checking Login Email Id
    this.loginEmail.valueChanges
    .pipe(debounceTime(600), distinctUntilChanged())
    .subscribe(term => {
      if (term != '') {
        if (this.staffPortalId === term) {
          this.loginEmail.setErrors(null);
        }
        else {
          this.checkUserEmailAddressViewModel.emailAddress = term;
          this.loginService.checkUserLoginEmail(this.checkUserEmailAddressViewModel).subscribe(data => {
            if (data.isValidEmailAddress) {
              this.loginEmail.setErrors(null);
            }
            else {
              this.loginEmail.markAsTouched();
              this.loginEmail.setErrors({ 'nomatch': true });
            }
          });
        }
      } else {
        this.loginEmail.markAsTouched();
      }
    });
  }

  getAllEthnicity() {
    this.lovListViewModel.lovName = "Ethnicity";
    this.commonService.getAllDropdownValues(this.lovListViewModel).subscribe(
      (res: LovList) => {
        if (typeof (res) == 'undefined') {
        }
        else {
          if (res._failure) {
          }
          else {
            this.ethnicityList = res.dropdownList;
          }
        }
      })
  }

  getAllRace() {
    this.lovListViewModel.lovName = "Race";
    this.commonService.getAllDropdownValues(this.lovListViewModel).subscribe(
      (res: LovList) => {
        if (typeof (res) == 'undefined') {
        }
        else {
          if (res._failure) {
          }
          else {
            this.raceList = res.dropdownList;
          }
        }
      })
  }

  getAllGender() {
    this.lovListViewModel.lovName = "Gender";
    this.commonService.getAllDropdownValues(this.lovListViewModel).subscribe(
      (res: LovList) => {
        if (typeof (res) == 'undefined') {
        }
        else {
          if (res._failure) {
          }
          else {
            this.genderList = res.dropdownList;
          }
        }
      })
  }

  getAllSuffix() {
    this.lovListViewModel.lovName = "Suffix";
    this.commonService.getAllDropdownValues(this.lovListViewModel).subscribe(
      (res: LovList) => {
        if (typeof (res) == 'undefined') {
        }
        else {
          if (res._failure) {
          }
          else {
            this.suffixList = res.dropdownList;
          }
        }
      })
  }

  getAllSalutation() {
    this.lovListViewModel.lovName = "Salutation";
    this.commonService.getAllDropdownValues(this.lovListViewModel).subscribe(
      (res: LovList) => {
        if (typeof (res) == 'undefined') {
        }
        else {
          if (res._failure) {
          }
          else {
            this.salutationList = res.dropdownList;
          }
        }
      })
  }

  getAllMaritalStatus() {
    this.lovListViewModel.lovName = "Marital Status";
    this.commonService.getAllDropdownValues(this.lovListViewModel).subscribe(
      (res: LovList) => {
        if (typeof (res) == 'undefined') {
        }
        else {
          if (res._failure) {
          }
          else {
            this.maritalStatusList = res.dropdownList;
          }
        }
      })
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
              this.findNationalityNameById();
          }
        }
      }
    })
  }

  findNationalityNameById(){
    this.countryListArr.map((val) => {
      let countryInNumber = +this.data.countryOfBirth;
      let nationality = +this.data.nationality;
      if (val.id === countryInNumber) {
        this.nameOfMiscValuesForView.countryName = val.name;
      }
      if (val.id === nationality) {
        this.nameOfMiscValuesForView.nationality = val.name;
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
          this.findNameById();
        }
      }

    })
  }

  findNameById(){
    this.languageList.map((val) => {
      let firstLanguageId = + this.data.firstLanguage;
      let secondLanguageId = + this.data.secondLanguage;
      let thirdLanguageId = + this.data.thirdLanguage;
      if (val.langId === firstLanguageId) {
        this.nameOfMiscValuesForView.firstLanguage = val.locale;
      }
      if (val.langId === secondLanguageId) {
        this.nameOfMiscValuesForView.secondLanguage = val.locale;
      }
      if (val.langId === thirdLanguageId) {
        this.nameOfMiscValuesForView.thirdLanguage = val.locale;
      }
    })
  }

  
  accessPortal() {
    if (this.staffPortalId !== null && this.staffPortalId !== undefined) {
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
    this.imageCropperService.enableUpload(true);
    this.staffService.changePageMode(this.staffCreateMode);
    this.saveAndNext = 'update';
    if (this.staffAddModel.staffMaster.physicalDisability) {
      this.showDisabilityDescription = true;
    }
    this.getAllSalutation();
    this.getAllSuffix();
    this.getAllGender();
    this.getAllMaritalStatus();
    this.getAllEthnicity();
    this.getAllRace()
  }

  cancelEdit() {
    if(this.staffAddModel!==JSON.parse(this.cloneStaffAddModel)){
      this.staffAddModel=JSON.parse(this.cloneStaffAddModel);
      this.staffDetailsForViewAndEdit=JSON.parse(this.cloneStaffAddModel);
      this.staffService.sendDetails(JSON.parse(this.cloneStaffAddModel));
    }
    this.staffCreateMode = this.staffCreate.VIEW;
    this.staffService.changePageMode(this.staffCreateMode);
    this.imageCropperService.enableUpload(false);
    this.imageCropperService.cancelImage("staff");
    this.data = this.staffAddModel.staffMaster;
    this.findNationalityNameById()
    this.GetAllLanguage();
  }


  checkDisability(event) {
    if (event.value == true) {
      this.showDisabilityDescription = true;
    }
    else {
      this.showDisabilityDescription = false;
    }
  }

  submitStaff() {
    this.currentForm.form.markAllAsTouched();
    if (this.currentForm.controls.passwordHash !== undefined) {
      this.staffAddModel.passwordHash = this.currentForm.controls.passwordHash.value;
    }

    if (this.currentForm.form.valid) {
      if (this.staffAddModel.fieldsCategoryList !== null) {
        this.staffAddModel.selectedCategoryId = this.staffAddModel.fieldsCategoryList[this.categoryId].categoryId;
        
        for (let staffCustomField of this.staffAddModel.fieldsCategoryList[this.categoryId].customFields) {
          if (staffCustomField.type === "Multiple SelectBox" && this.staffService.getStaffMultiselectValue() !== undefined) {
            staffCustomField.customFieldsValue[0].customFieldValue = this.staffService.getStaffMultiselectValue().toString().replaceAll(",", "|");
          }
        }
      }
      if (this.staffCreateMode == this.staffCreate.EDIT) {
        this.updateStaff();
      } else {
        this.addStaff();
      }
    }
  }

  addStaff() {
    if(this.internalId.invalid){
      this.invalidScroll();
      return
    }
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
          this.staffService.setStaffCloneImage(data.staffMaster.staffPhoto);
          this.staffService.changeCategory(13);
          this.staffService.setStaffDetails(data);
          this.dataAfterSavingGeneralInfo.emit(data);
        }
      }
    })
  }

  updateStaff() {
    if(this.internalId.invalid){
      this.invalidScroll();
      return
    }
    if (this.staffAddModel.fieldsCategoryList !== null) {
      this.staffAddModel.selectedCategoryId = this.staffAddModel.fieldsCategoryList[this.categoryId].categoryId;
    }
    this.staffAddModel._token = sessionStorage.getItem("token");
    this.staffAddModel._tenantName = sessionStorage.getItem("tenant");
    this.staffAddModel.staffMaster.dob = this.commonFunction.formatDateSaveWithoutTime(this.staffAddModel.staffMaster.dob);
    this.staffService.updateStaff(this.staffAddModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Staff Update failed. ' + sessionStorage.getItem("httpError"), '', {
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
          this.staffService.setStaffCloneImage(data.staffMaster.staffPhoto);
          data.staffMaster.staffPhoto=null;
          this.dataAfterSavingGeneralInfo.emit(data);
          this.staffAddModel.staffMaster = data.staffMaster;
          this.staffDetailsForViewAndEdit=data;
          this.cloneStaffAddModel=JSON.stringify(this.staffAddModel);
          this.findNationalityNameById();
          this.findNameById();
          this.staffCreateMode = this.staffCreate.VIEW
          if (this.staffAddModel.staffMaster.loginEmailAddress !== null) {
            this.hidePasswordAccess = false;
          }
          this.imageCropperService.enableUpload(false);
          this.staffService.changePageMode(this.staffCreateMode);
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
  
  invalidScroll(){
    const firstInvalidControl: HTMLElement = this.el.nativeElement.querySelector(
      'input.ng-invalid'
    );
      firstInvalidControl.scrollIntoView({ behavior: 'smooth',block: 'center' });
  }

  ngOnDestroy() {
    this.imageCropperService.enableUpload(false);
    this.destroySubject$.next();
    this.destroySubject$.complete();
  }

}
