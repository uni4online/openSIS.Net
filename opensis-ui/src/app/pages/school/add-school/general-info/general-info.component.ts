import { Component, OnInit, EventEmitter, Output, Input, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { FormBuilder, NgForm, Validators } from '@angular/forms';
import { CheckSchoolInternalIdViewModel, SchoolAddViewModel } from '../../../../models/schoolMasterModel';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { SchoolService } from '../../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as _moment from 'moment';
import { default as _rollupMoment } from 'moment';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { TranslateService } from '@ngx-translate/core';
import { schoolLevel } from '../../../../enums/school_level.enum';
import { schoolClassification } from '../../../../enums/school_classification.enum';
import { gender } from '../../../../enums/gender.enum';
import { WashInfoEnum } from '../../../../enums/wash-info.enum';
import { status } from '../../../../enums/wash-info.enum';
import { MY_FORMATS } from '../../../shared/format-datepicker';
import { ValidationService } from '../../../shared/validation.service';
import { LoaderService } from '../../../../services/loader.service';
import { __values } from 'tslib';
import { Subject } from 'rxjs';
import { CountryModel } from '../../../../models/countryModel';
import { StateModel } from '../../../../models/stateModel';
import { CityModel } from '../../../../models/cityModel';
import { CommonService } from '../../../../services/common.service';
import { SharedFunction } from '../../../shared/shared-function';
import { ImageCropperService } from '../../../../services/image-cropper.service';
import { CustomFieldAddView, CustomFieldListViewModel, CustomFieldModel } from '../../../../models/customFieldModel';
import { CustomFieldService } from '../../../../services/custom-field.service';
import icEdit from '@iconify/icons-ic/twotone-edit';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'vex-general-info',
  templateUrl: './general-info.component.html',
  styleUrls: ['./general-info.component.scss'],
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

export class GeneralInfoComponent implements OnInit, OnDestroy {
  SchoolCreate = SchoolCreate;
  @ViewChild('f') currentForm: NgForm;
  @Input() schoolCreateMode: SchoolCreate;
  @Input() schoolDetailsForViewAndEdit;
  @Input() categoryId;
  icEdit = icEdit;

  cityName;
  schoolLevelOptions = [];
  schoolClassificationOptions = [];
  genderOptions = [];

  destroySubject$: Subject<void> = new Subject();
  customFieldModel = new CustomFieldListViewModel();
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  checkSchoolInternalIdViewModel: CheckSchoolInternalIdViewModel = new CheckSchoolInternalIdViewModel();
  countryModel: CountryModel = new CountryModel();
  stateModel: StateModel = new StateModel();
  cityModel: CityModel = new CityModel();
  countryListArr = [];
  stateListArr = [];
  cityListArr = [];
  countryName = "";
  schoolInternalId = '';
  module = "School";
  stateName = "";
  status: string;
  generalInfo = WashInfoEnum;
  statusInfo = status;
  city: number;
  f: NgForm;
  stateCount: number;
  minDate;
  selectedLowGradeLevelIndex: number;
  selectedHighGradeLevelIndex: number;
  formActionButtonTitle = "submit";
  gradeLevel = [
    { id: 'PK', title: "PK" },
    { id: 'K', title: "K" },
    { id: '1', title: "1" },
    { id: '2', title: "2" },
    { id: '3', title: "3" },
    { id: '4', title: "4" },
    { id: '5', title: "5" },
    { id: '6', title: "6" },
    { id: '7', title: "7" },
    { id: '8', title: "8" },
    { id: '9', title: "9" },
    { id: '10', title: "10" },
    { id: '11', title: "11" },
    { id: '12', title: "12" },
    { id: '13', title: "13" },
    { id: '14', title: "14" },
    { id: '15', title: "15" },
    { id: '16', title: "16" },
    { id: '17', title: "17" },
    { id: '18', title: "18" },
    { id: '19', title: "19" },
    { id: '20', title: "20" },
  ];
  constructor(private fb: FormBuilder,
    private _schoolService: SchoolService,
    private customFieldService: CustomFieldService,
    private snackbar: MatSnackBar,
    public translateService: TranslateService,
    private loaderService: LoaderService,
    private commonService: CommonService,
    private commonFunction: SharedFunction,
    private _imageCropperService: ImageCropperService,
  ) {
    translateService.use('en');
    this._schoolService.getSchoolDetailsForGeneral.pipe(takeUntil(this.destroySubject$)).subscribe((res: SchoolAddViewModel) => {
      this.schoolAddViewModel = res;
      this.schoolInternalId = res.schoolMaster.schoolInternalId;
      if (this.schoolAddViewModel.schoolMaster.country) {
        this.getAllCountry();
      }
    })
  }

  ngOnInit(): void {
    if (this.schoolCreateMode == this.SchoolCreate.ADD) {
      this.getAllCountry();
    }
    else if (this.schoolCreateMode == this.SchoolCreate.VIEW) {
      this.schoolAddViewModel = this.schoolDetailsForViewAndEdit;
      this.status = this.schoolAddViewModel.schoolMaster.schoolDetail[0].status ? 'Active' : 'Inactive';
    }
    else if (this.schoolCreateMode == this.SchoolCreate.EDIT && (this.schoolDetailsForViewAndEdit != undefined || this.schoolDetailsForViewAndEdit != null)) {
      this.formActionButtonTitle = "update";
      this.schoolAddViewModel = this.schoolDetailsForViewAndEdit;
    }

    this.initializeDropdownsForSchool();

  }

  initializeDropdownsForSchool() {
    this.schoolLevelOptions = Object.keys(schoolLevel);
    this.schoolClassificationOptions = Object.keys(schoolClassification);
    this.genderOptions = Object.keys(gender);
  }

  editGeneralInfo() {
    this.schoolCreateMode = this.SchoolCreate.EDIT;
    this.formActionButtonTitle = "update";
    this.getAllCountry();
    this.checkGradeLevelsOnEdit();
    this._imageCropperService.nextMessage(false);
    this.schoolAddViewModel.schoolMaster.country = +this.schoolAddViewModel.schoolMaster.country
  }

  cancelEdit() {
    this._imageCropperService.nextMessage(true);
    this.schoolCreateMode = this.SchoolCreate.VIEW;
  }

  checkLowGradeLevel(event) {
    let index = this.gradeLevel.findIndex((val) => {
      return val.id == event.value;
    });
    this.selectedLowGradeLevelIndex = index;
    if (index == -1) {
      this.currentForm.form.controls.lowestGradeLevel.markAsTouched();
    } else {
      if (index > this.selectedHighGradeLevelIndex) {
        this.currentForm.form.controls.lowestGradeLevel.setErrors({ 'nomatch': true });
      } else {
        this.currentForm.controls.lowestGradeLevel.setErrors(null);
        if (this.currentForm.controls.highestGradeLevel.errors != null) {
          this.currentForm.controls.highestGradeLevel.errors.nomatch = false;
        }
      }
    }

  }
  checkHighGradeLevel(event) {
    let index = this.gradeLevel.findIndex((val) => {
      return val.id == event.value;
    });
    this.selectedHighGradeLevelIndex = index;
    if (index == -1) {
      this.currentForm.form.controls.highestGradeLevel.markAsTouched();
    } else {
      if (this.selectedLowGradeLevelIndex > index) {
        this.currentForm.form.controls.highestGradeLevel.setErrors({ 'nomatch': true });
      } else {
        this.currentForm.controls.highestGradeLevel.setErrors(null);
        if (this.currentForm.form.controls.lowestGradeLevel.errors != null) {
          this.currentForm.form.controls.lowestGradeLevel.errors.nomatch = false;
        }
      }
    }
  }

  checkSchoolInternalId(event) {
    let internalId = event.target.value;
    if (this.schoolInternalId === internalId) {
      this.currentForm.form.controls.schoolId.setErrors(null);
    }
    else {
      this.checkSchoolInternalIdViewModel.schoolInternalId = internalId;
      this._schoolService.checkSchoolInternalId(this.checkSchoolInternalIdViewModel).subscribe(data => {
        if (data.isValidInternalId) {
          this.currentForm.form.controls.schoolId.setErrors(null);
        }
        else {
          this.currentForm.form.controls.schoolId.setErrors({ 'nomatch': true });
        }
      });
    }


  }

  checkGradeLevelsOnEdit() {
    let lowGradeIndex = this.gradeLevel.findIndex((val) => {
      return val.id == this.schoolAddViewModel.schoolMaster.schoolDetail[0].lowestGradeLevel;
    });
    this.selectedLowGradeLevelIndex = lowGradeIndex;
    let highGradeIndex = this.gradeLevel.findIndex((val) => {
      return val.id == this.schoolAddViewModel.schoolMaster.schoolDetail[0].highestGradeLevel;
    });
    this.selectedHighGradeLevelIndex = highGradeIndex;
  }

  dateCompare() {
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed = null;
    this.minDate = this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened;
    let minDate = new Date(this.minDate)
    this.minDate = new Date(minDate.setDate(minDate.getDate() + 1));

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
          this.stateCount = data.stateCount;
          if (this.schoolCreateMode == SchoolCreate.VIEW) {
            this.findCountryNameByIdOnViewMode();
          }
        }
      }

    })

  }

  findCountryNameByIdOnViewMode() {
    let index = this.countryListArr.findIndex((x) => {
      return x.id === +this.schoolAddViewModel.schoolMaster.country
    });
    this.countryName = this.countryListArr[index]?.name;
  }

  getAllStateByCountry(data) {
    if (this.stateCount > 0) {
      if ((this.commonFunction.checkEmptyObject(this.schoolDetailsForViewAndEdit) === true)
        || (this.commonFunction.checkEmptyObject(this.schoolDetailsForViewAndEdit) === true)
        || (this.commonFunction.checkEmptyObject(this.schoolDetailsForViewAndEdit) === true)) {

        if (data.value === "") {
          this.stateModel.countryId = 0;
          this.countryName = '';
          this.cityListArr = [];
          this.stateListArr = [];
        } else {
          if (data.value === undefined && data) {
            this.stateModel.countryId = data;
            this.countryName = data.toString();
          } else {
            this.stateModel.countryId = data.value;
            this.countryName = data.value.toString();
          }
        }
      } else {
        if (data.value === "") {
          this.stateModel.countryId = 0;
          this.countryName = '';
          this.cityListArr = [];
          this.stateListArr = [];
        } else {
          this.stateModel.countryId = data.value;
          this.countryName = data.value.toString();
        }
      }

      if (this.stateModel.countryId !== 0) {

        this.commonService.GetAllState(this.stateModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.stateListArr = [];
          }
          else {
            if (data._failure) {
              this.stateListArr = [];

            } else {
              this.cityListArr = [];
              this.stateListArr = data.tableState;

            }
          }

        })
      }
    }
  }
  getAllCitiesByState(data) {
    if ((this.commonFunction.checkEmptyObject(this.schoolDetailsForViewAndEdit) === true)
      || (this.commonFunction.checkEmptyObject(this.schoolDetailsForViewAndEdit) === true)
      || (this.commonFunction.checkEmptyObject(this.schoolDetailsForViewAndEdit) === true)) {
      if (data.value === "") {
        this.cityModel.stateId = 0;
        this.stateName = '';
        this.cityListArr = [];
      } else {
        if (data.value === undefined && data) {
          this.cityModel.stateId = data;
          this.stateName = data.toString();
        } else {
          this.cityModel.stateId = data.value;
          this.stateName = data.value.toString();
        }
      }
    } else {
      if (data.value === "") {
        this.cityModel.stateId = 0;
        this.stateName = '';
        this.cityListArr = [];
      } else {
        this.cityModel.stateId = data.value;
        this.stateName = data.value.toString();
      }
    }


    if (this.cityModel.stateId !== 0) {
      this.commonService.GetAllCity(this.cityModel).subscribe(val => {
        if (typeof (val) == 'undefined') {
          this.cityListArr = [];
        }
        else {
          if (val._failure) {
            this.cityListArr = [];
          } else {
            this.cityListArr = val.tableCity;
          }
        }

      })
    }
  }
  onStatusChange(event: boolean) {
    let schoolClosedDate = this.currentForm.value.date_school_closed;
    if (event === false && (schoolClosedDate == null || schoolClosedDate == undefined)) {
      this.currentForm.controls.date_school_closed.setValidators(Validators.required);
      this.currentForm.controls.date_school_closed.setErrors({ required: true })
      this.currentForm.controls.date_school_closed.markAsTouched();
    } else {
      if (this.currentForm.controls.date_school_closed.errors?.required) {
        this.currentForm.controls.date_school_closed.setErrors(null);
      }
    }
  }

  checkClosedDate() {
    let startDate = new Date(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened).getTime();
    let closedDate = new Date(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed).getTime();
    if (closedDate <= startDate && startDate != null && closedDate != 0) {
      this.currentForm.controls.date_school_closed.setErrors({ 'nomatch': true });
    } else {
      if (this.currentForm.controls.date_school_closed.errors?.nomatch) {
        this.currentForm.controls.date_school_closed.setErrors(null);
      }
    }
    this.onStatusChange(this.schoolAddViewModel.schoolMaster.schoolDetail[0].status);
  }

  submit() {
    this.currentForm.form.markAllAsTouched();
    if (this.currentForm.form.valid) {
      if (this.schoolCreateMode == this.SchoolCreate.EDIT) {
        this.schoolAddViewModel.selectedCategoryId = this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].categoryId;
        this.updateSchool();
      } else {
        this.addSchool();
      }
    }
  }

  addSchool() {
    this.schoolAddViewModel.schoolMaster.country = this.schoolAddViewModel.schoolMaster.country.toString();
    this.schoolAddViewModel.schoolMaster.state = this.stateName;
    this.schoolAddViewModel.schoolMaster.state = this.schoolAddViewModel.schoolMaster.state.toString();
    this.schoolAddViewModel.schoolMaster.city = this.schoolAddViewModel.schoolMaster.city.toString();
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened);
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed);
    this._schoolService.AddSchool(this.schoolAddViewModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('General Info Submission failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('General Info Submission failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {

          this.snackbar.open('General Info Submission Successful.', '', {
            duration: 10000
          });
          let schoolIdToString = data.schoolMaster.schoolId.toString();
          sessionStorage.setItem("selectedSchoolId", schoolIdToString);
          this._schoolService.changeMessage(true);
          this._schoolService.changeCategory(2);
          this._schoolService.setSchoolDetails(data);
        }
      }

    });
  }

  updateSchool() {
    if (this.stateCount !== 0) {
      this.schoolAddViewModel.schoolMaster.country = this.countryName;
      this.schoolAddViewModel.schoolMaster.state = this.stateName;
    }
    else {
      this.schoolAddViewModel.schoolMaster.country = this.schoolAddViewModel.schoolMaster.country.toString();
      this.schoolAddViewModel.schoolMaster.state = this.schoolAddViewModel.schoolMaster.state.toString();
    }
    this.schoolAddViewModel.schoolMaster.city = this.schoolAddViewModel.schoolMaster.city.toString();
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened);
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed);
    this._schoolService.UpdateSchool(this.schoolAddViewModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('General Info Updation failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('General Info Updation failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {

          this.snackbar.open('General Info Updation Successful.', '', {
            duration: 10000
          });
          this._schoolService.changeMessage(true);
          this._schoolService.changeCategory(2);
        }
      }

    });
  }


  ngOnDestroy() {
    this.destroySubject$.next();
  }
}
