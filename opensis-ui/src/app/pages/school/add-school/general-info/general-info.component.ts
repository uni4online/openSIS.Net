import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SchoolAddViewModel } from '../../../../models/schoolMasterModel';
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
import { highestGradeLevel } from '../../../../enums/highest_grade_level.enum';
import { MY_FORMATS } from '../../../shared/format-datepicker';
import { ValidationService } from '../../../shared/validation.service';
import { LoaderService } from 'src/app/services/loader.service';
import { __values } from 'tslib';
import { BehaviorSubject } from 'rxjs';
import { CountryModel } from '../../../../models/countryModel';
import { StateModel } from '../../../../models/stateModel';
import { CityModel } from '../../../../models/cityModel';
import { CommonService } from '../../../../services/common.service';
import { SharedFunction } from '../../../shared/shared-function';
import { ImageCropperService } from '../../../../services/image-cropper.service';

const moment = _rollupMoment || _moment;


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

export class GeneralInfoComponent implements OnInit {
  @Output("parentShowWash") parentShowWash: EventEmitter<object> = new EventEmitter<object>();
  @Output() dataOfgeneralInfo: EventEmitter<object> = new EventEmitter();
  @Input() isEditMode: Boolean;
  @Input() dataOfgeneralInfoFromView;
  @Input() dataOfgeneralInfoFromWash;
  @Input() dataOfwashInfoFromGeneral;
  @Input() image;

  private schoolLevels = schoolLevel;
  public schoolLevelOptions = [];
  private schoolClassifications = schoolClassification;
  public schoolClassificationOptions = [];
  private genders = gender;
  public genderOptions = [];  
  private highestGradeLevels = highestGradeLevel;
  public highestGradeLevelsOption = [];
  public tenant = "opensisv2";
  form: FormGroup;
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  countryModel: CountryModel = new CountryModel();
  stateModel: StateModel = new StateModel();
  cityModel: CityModel = new CityModel();
  schoolViewModel = new BehaviorSubject<SchoolAddViewModel>(this.schoolAddViewModel)
  schoolViewModelObserver = this.schoolViewModel.asObservable()
  loading;
  countryListArr=[];
  stateListArr=[];
  cityListArr=[];
  countryName="";
  stateName="";
  dateCreated=Date();
  generalInfo=WashInfoEnum;
  statusInfo=status;
city:number;
  constructor(private fb: FormBuilder,
    private generalInfoService: SchoolService,
    private snackbar: MatSnackBar,    
    public translateService: TranslateService,
    private loaderService: LoaderService,
    private commonService: CommonService,
    private commonFunction:SharedFunction,
    private _ImageCropperService:ImageCropperService) {

    translateService.use('en');    
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });

  }

  ngOnInit(): void {
    const today = moment();
    this.dateCreated = today.format('YYYY-MM-DD');

    this.form = this.fb.group(
      {
        school_Name: ['', [Validators.required,Validators.maxLength(100)]],
        alternate_name: ['',Validators.maxLength(100)],
        school_id: ['',Validators.maxLength(50)],
        state_id: ['',Validators.maxLength(10)],
        district_id: ['',Validators.maxLength(10)],
        alternate_id: ['',[Validators.maxLength(10)]],
        school_level: ['', Validators.required],
        school_classification: ['', [Validators.required,Validators.maxLength(50)]],
        affiliation: [''],
        associations: [''],
        lowest_grade_level: ['', Validators.required],
        highest_grade_level: ['', Validators.required],
        date_school_opened: [''],
        date_school_closed: [''],
        code: ['',Validators.maxLength(100)],
        gender: ['',Validators.maxLength(6)],
        internet: [''],
        electricity: [''],
        status: [''],
        street_address_1: ['', [Validators.required,Validators.maxLength(150)]],
        street_address_2: ['',Validators.maxLength(150)],
        city: ['', [Validators.required,Validators.maxLength(50)]],
        country: ['', Validators.required],
        division: ['',Validators.maxLength(50)],
        district: ['',Validators.maxLength(50)],
        zip: ['', [Validators.required,Validators.maxLength(10)]],
        state: ['',Validators.maxLength(50)],
        latitude: [''],
        longitude: [''],
        principal: ['', [Validators.required,Validators.maxLength(100)]],
        ass_principal: ['',Validators.maxLength(100)],
        telephone: ['', [Validators.required,Validators.maxLength(20)]],
        fax: ['',Validators.maxLength(20)],
        website: [''],
        email: ['', [ValidationService.emailValidator,Validators.maxLength(100)]],
        twitter: ['',Validators.maxLength(100)],
        facebook: ['',Validators.maxLength(100)],
        instagram: ['',Validators.maxLength(100)],
        youtube: ['',Validators.maxLength(100)],
        linkedin: ['',Validators.maxLength(100)],
        county: ['',Validators.maxLength(50)],
      });
      this.getAllCountry();
     
    this.schoolLevelOptions = Object.keys(this.schoolLevels);
    this.schoolClassificationOptions = Object.keys(this.schoolClassifications);
    this.genderOptions = Object.keys(this.genders);   
    this.highestGradeLevelsOption = Object.keys(this.highestGradeLevels);
    
    if (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromView) === true) {
      this.schoolAddViewModel = this.dataOfgeneralInfoFromView;
    


    } else if (this.commonFunction.checkEmptyObject(this.dataOfwashInfoFromGeneral) === true) {
      this.schoolAddViewModel = this.dataOfwashInfoFromGeneral;

    }
    else if (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromWash) === true) {
      this.schoolAddViewModel = this.dataOfgeneralInfoFromWash;

    }

    
    if ((this.commonFunction.checkEmptyObject(this.dataOfwashInfoFromGeneral) === true)
     || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromView) === true) 
     || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromWash) === true))
     {
      this._ImageCropperService.nextMessage(false);      
      this.schoolAddViewModel.tblSchoolMaster.city = +this.schoolAddViewModel.tblSchoolMaster.city;
      this.schoolAddViewModel.tblSchoolMaster.state = +this.schoolAddViewModel.tblSchoolMaster.state;
      this.schoolAddViewModel.tblSchoolMaster.country = +this.schoolAddViewModel.tblSchoolMaster.country;
   
      this.getAllStateByCountry(this.schoolAddViewModel.tblSchoolMaster.country);
      this.getAllCitiesByState(this.schoolAddViewModel.tblSchoolMaster.state);
      this.schoolAddViewModel.tblSchoolMaster.modifiedBy = sessionStorage.getItem('email');
      this.schoolAddViewModel.tblSchoolMaster.dateModifed = this.dateCreated;
      this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].dateSchoolOpened=this.commonFunction.formatDateInEditMode(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].dateSchoolOpened);      
      this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].dateSchoolClosed=this.commonFunction.formatDateInEditMode(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].dateSchoolClosed);
      
    }else{

      
      this.schoolAddViewModel.tblSchoolMaster.createdBy = sessionStorage.getItem('email');
      this.schoolAddViewModel.tblSchoolMaster.dateCreated = this.dateCreated;     
    }

    this.schoolAddViewModel.tblSchoolMaster.schoolLevel = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.schoolLevel);
    this.schoolAddViewModel.tblSchoolMaster.schoolClassification = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.schoolClassification) ;
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].lowestGradeLevel = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].lowestGradeLevel);
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].highestGradeLevel = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].highestGradeLevel);
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].gender = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].gender);
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].telephone = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].telephone) ;
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].website = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].website) ;
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].twitter = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].twitter) ;
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].instagram = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].instagram) ;



    this.schoolAddViewModel.tblSchoolMaster.schoolName = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.schoolName);
    this.schoolAddViewModel.tblSchoolMaster.alternateName = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.alternateName) ;
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].locale = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].locale);
    this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].gender = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].gender);
    this.schoolAddViewModel.tblSchoolMaster.county = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.county);
    this.schoolAddViewModel.tblSchoolMaster.division = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.division) ;
  
    this.schoolAddViewModel.tblSchoolMaster.district = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.district) ;
    this.schoolAddViewModel.tblSchoolMaster.zip = this.commonFunction.trimData(this.schoolAddViewModel.tblSchoolMaster.zip) ;
    
  }


  get f() {
    return this.form.controls;
  }
  dateCompare() {
   
    let openingDate = this.form.controls.date_school_opened.value
    let closingDate = this.form.controls.date_school_closed.value
   
    if (ValidationService.compareValidation(openingDate, closingDate) === false) {
      this.form.controls.date_school_closed.setErrors({ compareError: false })
      
    }

  }
 getAllCountry(){
  this.countryModel._tenantName = this.tenant;
  this.countryModel._token = sessionStorage.getItem("token");
  this.commonService.GetAllCountry(this.countryModel).subscribe(data => {
    if (typeof (data) == 'undefined') {
      this.countryListArr=[];
    }
    else {
      if (data._failure) {
        this.countryListArr=[];
      } else {        
        this.countryListArr=data.tableCountry;         
      }
    }

  }) 

 }

 getAllStateByCountry(data){
   
   if (data.value === undefined)
   {
    this.stateModel.countryId= data;
    this.countryName = data.toString();

   }else{
    this.stateModel.countryId= data.value;
    this.countryName = data.value.toString();
   }
  this.stateModel._tenantName = this.tenant;
  this.stateModel._token = sessionStorage.getItem("token");
 
  this.commonService.GetAllState(this.stateModel).subscribe(data => {
    if (typeof (data) == 'undefined') {
      this.stateListArr=[];
    }
    else {
      if (data._failure) {
        this.stateListArr=[];
       
      } else {
        this.cityListArr=[];
        this.stateListArr=data.tableState;      
       
      }
    }

  })
 }
 getAllCitiesByState(data){

  if (data.value === undefined)
   {
    this.cityModel.stateId= data;
    this.stateName = data.toString();

   }else{
    this.cityModel.stateId= data.value;
    this.stateName = data.value.toString();
  }
  
  
  this.cityModel._tenantName = this.tenant;
  this.cityModel._token = sessionStorage.getItem("token");

  this.commonService.GetAllCity(this.cityModel).subscribe(val => {
    if (typeof (val) == 'undefined') {
      this.cityListArr=[];
    }
    else {
      if (val._failure) {
        this.cityListArr=[];
      } else {
        this.cityListArr=val.tableCity;    
      }
    }

  }) 

 }
  submit() {
    
    if (this.form.valid) {
      this.schoolAddViewModel._tenantName = this.tenant;
      this.schoolAddViewModel._token = sessionStorage.getItem("token");
      
      this.schoolAddViewModel.tblSchoolMaster.tableSchoolDetail[0].schoolLogo = this.image;
     
      if ((this.commonFunction.checkEmptyObject(this.dataOfwashInfoFromGeneral) === true)
     || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromView) === true) 
     || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromWash) === true)){

      this.schoolAddViewModel.tblSchoolMaster.country = this.countryName;
      this.schoolAddViewModel.tblSchoolMaster.state = this.stateName;
      this.schoolAddViewModel.tblSchoolMaster.city = this.schoolAddViewModel.tblSchoolMaster.city.toString();

        this.generalInfoService.UpdateGeneralInfo(this.schoolAddViewModel).subscribe(data => {
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
              this.parentShowWash.emit(this.isEditMode);
              this.dataOfgeneralInfo.emit(data);
            }
          }

        })
      }else{

        this.schoolAddViewModel.tblSchoolMaster.country = this.countryName;
      this.schoolAddViewModel.tblSchoolMaster.state = this.stateName;
      this.schoolAddViewModel.tblSchoolMaster.city = this.schoolAddViewModel.tblSchoolMaster.city.toString();
        
        this.generalInfoService.SaveGeneralInfo(this.schoolAddViewModel).subscribe(data => {
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
              this.parentShowWash.emit(this.isEditMode);
              this.dataOfgeneralInfo.emit(data);
            }
          }

        })

      }
    }
  }

}
