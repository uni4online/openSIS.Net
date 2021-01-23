import { Component, OnInit, Input, ViewChild, ChangeDetectorRef, OnDestroy, EventEmitter, Output, AfterViewInit, ElementRef } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
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
import { SchoolCreate } from '../../../../enums/school-create.enum';
import icEdit from '@iconify/icons-ic/edit';
import { Subject } from 'rxjs/internal/Subject';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import icVisibility from '@iconify/icons-ic/twotone-visibility';
import icVisibilityOff from '@iconify/icons-ic/twotone-visibility-off';
import { SectionService } from '../../../../services/section.service';
import { GetAllSectionModel, TableSectionList } from '../../../../models/sectionModel';
import { CheckUserEmailAddressViewModel } from '../../../../models/userModel';
import { ImageCropperService } from '../../../../services/image-cropper.service';
import { LovList } from '../../../../models/lovModel';
import {MiscModel} from '../../../../models/misc-data-student.model';

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
export class StudentGeneralinfoComponent implements OnInit, AfterViewInit, OnDestroy {
  icEdit = icEdit;
  icVisibility = icVisibility;
  icVisibilityOff = icVisibilityOff;

  studentCreate = SchoolCreate;
  @Input() studentCreateMode: SchoolCreate;
  @Input() studentDetailsForViewAndEdit;
  @Input() categoryId;
  @ViewChild('f') currentForm: NgForm;

  data;
  nameOfMiscValuesForView:MiscModel=new MiscModel; //This Object contains Section Name, Nationality, Country, languages for View Mode.
  countryListArr = [];
  ethnicityList = [];
  raceList = [];
  genderList = [];
  suffixList = [];
  salutationList = [];
  maritalStatusList = [];
  countryModel: CountryModel = new CountryModel();
  destroySubject$: Subject<void> = new Subject();
  form: FormGroup;
  studentAddModel: StudentAddModel = new StudentAddModel();
  checkStudentInternalIdViewModel: CheckStudentInternalIdViewModel = new CheckStudentInternalIdViewModel();
  checkUserEmailAddressViewModel: CheckUserEmailAddressViewModel = new CheckUserEmailAddressViewModel();
  sectionList: [TableSectionList];
  languages: LanguageModel = new LanguageModel();
  lovListViewModel: LovList = new LovList()
  module = 'Student';
  saveAndNext = 'saveAndNext';
  pageStatus: string;
  languageList;
  inputType = 'password';
  studentInternalId = '';
  studentPortalId = '';
  visible = false;
  pass: string = "";
  hidePasswordAccess: boolean = false;
  hideAccess: boolean = false;
  fieldDisabled: boolean = false;
  internalId: FormControl;
  loginEmail:FormControl;
  cloneStudentModel;
  isSubjectActivated=false;
  @Output() dataAfterSavingGeneralInfo = new EventEmitter<any>();
  constructor(
    private el: ElementRef,
    public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private studentService: StudentService,
    private commonService: CommonService,
    private loginService: LoginService,
    private commonFunction: SharedFunction,
    private sectionService: SectionService,
    private cd: ChangeDetectorRef,
    private imageCropperService: ImageCropperService) {
    translateService.use('en');
    this.studentService.getStudentDetailsForGeneral.pipe(takeUntil(this.destroySubject$)).subscribe((res: StudentAddModel) => {
      this.isSubjectActivated=true;
      this.studentAddModel = res;
      this.studentAddModel.loginEmail = this.studentAddModel.studentMaster.studentPortalId;
      this.data = this.studentAddModel?.studentMaster;
      this.cloneStudentModel=JSON.stringify(this.studentAddModel);
      this.studentInternalId = this.data.studentInternalId;
      this.studentPortalId = this.data.studentPortalId;
      if(this.studentAddModel.studentMaster?.studentId){
        this.accessPortal();
        this.GetAllLanguage();
        this.initializeDropdowns()
      }
    })
  }

  ngOnInit(): void {
    this.internalId = new FormControl('',Validators.required);
    this.loginEmail=new FormControl('',Validators.required);
    if (this.studentCreateMode == this.studentCreate.ADD) {
      this.initializeDropdownsInAddMode();
    } else if (this.studentCreateMode == this.studentCreate.VIEW) {
      this.studentService.changePageMode(this.studentCreateMode);
      this.imageCropperService.enableUpload(false);
      this.studentAddModel = this.studentDetailsForViewAndEdit;
      this.data = this.studentDetailsForViewAndEdit?.studentMaster;
      this.cloneStudentModel=JSON.stringify(this.studentAddModel);
      if(!this.isSubjectActivated){
        this.GetAllLanguage();
        this.getAllCountry();
        this.getAllSection();
        this.getAllEthnicity();
        this.getAllRace();
        this.getAllGender();
        this.getAllSalutation();
        this.getAllSuffix();
        this.getAllMaritalStatus();
      }

    } else if (this.studentCreateMode == this.studentCreate.EDIT && (this.studentDetailsForViewAndEdit != undefined || this.studentDetailsForViewAndEdit != null)) {
      this.studentAddModel = this.studentDetailsForViewAndEdit;
      this.cloneStudentModel=JSON.stringify(this.studentAddModel);
      this.data=this.studentAddModel.studentMaster;
      this.studentPortalId = this.studentAddModel.studentMaster.studentPortalId;
      this.studentService.changePageMode(this.studentCreateMode);
      this.accessPortal();
      this.initializeDropdownsInAddMode();
      this.saveAndNext = 'update';
      if (this.studentAddModel.studentMaster.studentPortalId !== null) {
        this.hideAccess = true;
        this.fieldDisabled = true;
      }
    }
  }

  initializeDropdowns(){
    this.getAllCountry();
    this.getAllSection();
  }

  initializeDropdownsInAddMode(){
    this.getAllSalutation();
    this.getAllSuffix();
    this.getAllGender();
    this.getAllRace();
    this.getAllEthnicity();
    this.getAllMaritalStatus();
    this.getAllCountry();
    this.GetAllLanguage();
    this.getAllSection();
  }

  ngAfterViewInit() {
    // For Checking Internal Id
    this.internalId.valueChanges.pipe(debounceTime(500), distinctUntilChanged()).subscribe((term) => {
      if (term != '') {
        if (this.studentInternalId === term) {
          this.internalId.setErrors(null);
        }
        else {
          this.checkStudentInternalIdViewModel.studentInternalId = term;
          this.studentService.checkStudentInternalId(this.checkStudentInternalIdViewModel).subscribe(data => {
            if (data.isValidInternalId) {
              this.internalId.setErrors(null);
            }
            else {
              this.internalId.markAsTouched();
              this.internalId.setErrors({ 'nomatch': true });
            }
          });
        }
      } else {
        this.internalId.markAsTouched();
      }
    });

    this.loginEmail.valueChanges
    .pipe(debounceTime(600), distinctUntilChanged())
    .subscribe(term => {
      if (term != '') {
        if (this.studentPortalId === term) {
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

  accessPortal() {
    if (this.data?.studentPortalId !== null && this.data?.studentPortalId !== undefined) {
      this.hideAccess = true;
      this.fieldDisabled = true;
      this.hidePasswordAccess = false;
    } else {
      this.hideAccess = false;
      this.fieldDisabled = false;
      this.hidePasswordAccess = false;
    }
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

  getAllSection() {
    let section: GetAllSectionModel = new GetAllSectionModel();
    this.sectionService.GetAllSection(section).subscribe(data => {
      if (data._failure) {
      }
      else {
        this.sectionList = data.tableSectionsList;
        if (this.studentCreateMode == this.studentCreate.VIEW) {
         this.findSectionNameById();
        }
      }
      
    });
  }

  findSectionNameById(){
    this.sectionList.map((val) => {
      var sectionNumber = +this.data.sectionId;
      if (val.sectionId === sectionNumber) {
        this.nameOfMiscValuesForView.sectionName = val.name;
      }
    })
  }

  isPortalAccess(event) {
    if (event.checked) {
      if (this.studentCreateMode == this.studentCreate.ADD) {
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
          if (this.studentCreateMode == this.studentCreate.VIEW) {
           this.findCountryNationalityById();
          }
        }
      }
    })
  }

  findCountryNationalityById(){
    this.countryListArr.map((val) => {
      var countryInNumber = +this.data.countryOfBirth;
      var nationality = +this.data.nationality;
      if (val.id === countryInNumber) {
        this.nameOfMiscValuesForView.countryName = val.name;
      }
      if (val.id === nationality) {
        this.nameOfMiscValuesForView.nationality = val.name;
      }
    });
  }
  editGeneralInfo() {
    this.studentCreateMode = this.studentCreate.EDIT
    this.getAllEthnicity();
    this.getAllRace();
    this.getAllGender();
    this.getAllSalutation();
    this.getAllSuffix();
    this.getAllMaritalStatus();
    this.studentService.changePageMode(this.studentCreateMode);
    this.imageCropperService.enableUpload(true);
    this.saveAndNext = 'update';
  }
  
  cancelEdit() {
    if(JSON.stringify(this.studentAddModel)!==this.cloneStudentModel){
      this.studentAddModel=JSON.parse(this.cloneStudentModel);
      this.studentDetailsForViewAndEdit=JSON.parse(this.cloneStudentModel);
      this.studentService.sendDetails(JSON.parse(this.cloneStudentModel));
    }
    this.findCountryNationalityById();
    this.findLanguagesById();
    this.findSectionNameById();
    this.studentCreateMode = this.studentCreate.VIEW
    this.imageCropperService.enableUpload(false);
    this.imageCropperService.cancelImage("student");
    this.studentService.changePageMode(this.studentCreateMode);
    this.data = this.studentAddModel.studentMaster;       
  }

  GetAllLanguage() {
    this.languages._tenantName = sessionStorage.getItem("tenant");
    this.loginService.getAllLanguage(this.languages).subscribe((res) => {
      if (typeof (res) == 'undefined') {
        this.languageList = [];
      }
      else {
        this.languageList = res.tableLanguage;
        if (this.studentCreateMode == this.studentCreate.VIEW) {
         this.findLanguagesById()
        }
      }
    })
  }

  findLanguagesById(){
    this.languageList.map((val) => {
      let firstLanguageId = + this.data.firstLanguageId;
      let secondLanguageId = + this.data.secondLanguageId;
      let thirdLanguageId = + this.data.thirdLanguageId;
      
      if (val.langId === firstLanguageId) {
        this.nameOfMiscValuesForView.firstLanguage = val.locale;
      }
      if (val.langId === secondLanguageId) {
        this.nameOfMiscValuesForView.secondLanguage = val.locale;
      }
      if (val.langId === thirdLanguageId) {
        this.nameOfMiscValuesForView.thirdLanguage = val.locale;
      }
    });
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

      if (this.studentAddModel.fieldsCategoryList !== null) {
        this.studentAddModel.selectedCategoryId = this.studentAddModel.fieldsCategoryList[this.categoryId].categoryId;
        for (let studentCustomField of this.studentAddModel.fieldsCategoryList[this.categoryId].customFields) {
          if (studentCustomField.type === "Multiple SelectBox" && this.studentService.getStudentMultiselectValue() !== undefined) {
            studentCustomField.customFieldsValue[0].customFieldValue = this.studentService.getStudentMultiselectValue().toString().replaceAll(",", "|");
          }
        }
      }
      if (this.studentCreateMode == this.studentCreate.EDIT) {
        this.updateStudent();
      } else {
        this.addStudent();
      }
    }
  }

  updateStudent() {
    if (this.internalId.invalid) {
      this.invalidScroll();
      return
    }
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
          this.snackbar.open('Student Information has been updated successfully', '', {
            duration: 10000
          });
          this.studentService.setStudentCloneImage(data.studentMaster.studentPhoto);
          data.studentMaster.studentPhoto=null;
          this.data = data.studentMaster;
          this.cloneStudentModel=JSON.stringify(data);
          this.findCountryNationalityById();
          this.findLanguagesById();
          this.studentDetailsForViewAndEdit=data;
          this.dataAfterSavingGeneralInfo.emit(data);
          this.studentCreateMode = this.studentCreate.VIEW
          this.studentService.changePageMode(this.studentCreateMode);
          this.imageCropperService.enableUpload(false);        
        }
      }
    });
  }

  addStudent() {
    if (this.internalId.invalid) {
      this.invalidScroll();
      return
    }
    this.studentAddModel.studentMaster.dob = this.commonFunction.formatDateSaveWithoutTime(this.studentAddModel.studentMaster.dob);
    this.studentService.AddStudent(this.studentAddModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Student Save failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('Student Save failed. ' + data._message, '', {
            duration: 10000
          });
        } else {
          this.snackbar.open('Student has been saved successfully.', '', {
            duration: 10000
          });
          this.studentService.setStudentId(data.studentMaster.studentId);
          this.studentService.setStudentCloneImage(data.studentMaster.studentPhoto);
          this.studentService.changeCategory(4);
          this.studentService.setStudentDetails(data);
          this.dataAfterSavingGeneralInfo.emit(data);
        }
      }

    })
  }

  invalidScroll() {
    const firstInvalidControl: HTMLElement = this.el.nativeElement.querySelector(
      'input.ng-invalid'
    );
    firstInvalidControl.scrollIntoView({ behavior: 'smooth', block: 'center' });
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
    this.destroySubject$.complete();
  }


}
