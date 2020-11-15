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
import { MY_FORMATS } from '../../../shared/format-datepicker';
import { ValidationService } from '../../../shared/validation.service';
import { LoaderService } from '../../../../services/loader.service';
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
  @Output() onViewMode = new EventEmitter();
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
  stateCount:number;
  minDate;
  selectedLowGradeLevelIndex:number;
  selectedHighGradeLevelIndex:number;
  formActionButtonTitle="submit";
  gradeLevel= [
    {id : 'PK', title : "PK"},
    {id : 'K', title : "K"},
    {id : '1', title : "1"},
    {id : '2', title : "2"},
    {id : '3', title : "3"},
    {id : '4', title : "4"},
    {id : '5', title : "5"},
    {id : '6', title : "6"},
    {id : '7', title : "7"},
    {id : '8', title : "8"},
    {id : '9', title : "9"},
    {id : '10', title : "10"},
    {id : '11', title : "11"},
    {id : '12', title : "12"},
    {id : '13', title : "13"},
    {id : '14', title : "14"},
    {id : '15', title : "15"},
    {id : '16', title : "16"},
    {id : '17', title : "17"},
    {id : '18', title : "18"},
    {id : '19', title : "19"},
    {id : '20', title : "20"},
  ];
  constructor(private fb: FormBuilder,
    private generalInfoService: SchoolService,
    private snackbar: MatSnackBar,    
    public translateService: TranslateService,
    private loaderService: LoaderService,
    private commonService: CommonService,
    private commonFunction:SharedFunction,
    private _ImageCropperService:ImageCropperService,
    ) {

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
        date_school_opened: ['',Validators.required],
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
      if(this.dataOfgeneralInfoFromView!=undefined){
        this.formActionButtonTitle="update";
      } 
  }

  checkGradeLevelsOnEdit(){
    let lowGradeIndex=this.gradeLevel.findIndex((val)=>{
      return val.id==this.schoolAddViewModel.schoolMaster.schoolDetail[0].lowestGradeLevel;
    });
    this.selectedLowGradeLevelIndex=lowGradeIndex;
    let highGradeIndex=this.gradeLevel.findIndex((val)=>{
      return val.id==this.schoolAddViewModel.schoolMaster.schoolDetail[0].highestGradeLevel;
    });
    this.selectedHighGradeLevelIndex=highGradeIndex;
  }

  checkLowGradeLevel(event){
    let index=this.gradeLevel.findIndex((val)=>{
      return val.id==event.value;
    });
    this.selectedLowGradeLevelIndex=index;
    if(index==-1){
      this.form.controls.lowest_grade_level.markAsTouched();
    }else{
      if(index>this.selectedHighGradeLevelIndex){
        this.form.controls.lowest_grade_level.setErrors({'nomatch': true});
        }else{
          this.form.controls.lowest_grade_level.setErrors(null);
          if(this.form.controls.highest_grade_level.errors?.nomatch){
            this.form.controls.highest_grade_level.setErrors(null);
          }
        }
    }
 }
  checkHighGradeLevel(event){
    let index=this.gradeLevel.findIndex((val)=>{
      return val.id==event.value;
    });
    this.selectedHighGradeLevelIndex=index;
    if(index==-1){
      this.form.controls.highest_grade_level.markAsTouched();
    }else{
      if(this.selectedLowGradeLevelIndex>index){
        this.form.controls.highest_grade_level.setErrors({'nomatch': true});
        }else{
        this.form.controls.highest_grade_level.setErrors(null);
        if(this.form.controls.lowest_grade_level.errors?.nomatch){
          this.form.controls.lowest_grade_level.setErrors(null);
        }
        }
    }
  }

  checkClosedDate(){
    let startDate=new Date(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened).getTime();
    let closedDate=new Date(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed).getTime();
    if(closedDate<=startDate && startDate!=null){
      this.form.controls.date_school_closed.setErrors({'nomatch':true});
    }else{
      if(this.form.controls.date_school_closed.errors?.nomatch){
        this.form.controls.date_school_closed.setErrors(null);
      }
    }
  }


  get f() {
    return this.form.controls;
  }
  dateCompare() {
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed=null;
    this.minDate = this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened;
    let minDate = new Date(this.minDate)
    this.minDate=new Date(minDate.setDate(minDate.getDate() + 1));
    
  }

 getAllCountry(){  
  this.commonService.GetAllCountry(this.countryModel).subscribe(data => {
    if (typeof (data) == 'undefined') {
      this.countryListArr=[];
    }
    else {
      if (data._failure) {
        this.countryListArr=[];
      } else {        
        this.countryListArr=data.tableCountry;         
        this.stateCount=data.stateCount;
        this.onEditCall()
        
      }
    }

  }) 

 }

 getAllStateByCountry(data){   
   if(this.stateCount>0){
    if ((this.commonFunction.checkEmptyObject(this.dataOfwashInfoFromGeneral) === true)
    || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromView) === true) 
    || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromWash) === true)){
    
     if(data.value === ""){     
      this.stateModel.countryId= 0;
      this.countryName = '';
      this.cityListArr=[];
      this.stateListArr=[];
     }else{ 
       if(data.value === undefined && data){       
        this.stateModel.countryId= data;
        this.countryName = data.toString();
       }else{
        this.stateModel.countryId= data.value;
        this.countryName = data.value.toString();
       }     
    }          
  }else{
    if(data.value === ""){     
      this.stateModel.countryId= 0;
      this.countryName = '';
      this.cityListArr=[];
      this.stateListArr=[];
     }else{     
        this.stateModel.countryId= data.value;
        this.countryName = data.value.toString();           
    }    
  }
  
    if(this.stateModel.countryId !== 0){
     
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
  } 
 }
 getAllCitiesByState(data){
  if ((this.commonFunction.checkEmptyObject(this.dataOfwashInfoFromGeneral) === true)
    || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromView) === true) 
    || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromWash) === true)){
      if(data.value === ""){     
        this.cityModel.stateId= 0;
        this.stateName = '';
        this.cityListArr=[];       
       }else{ 
         if(data.value === undefined && data){       
          this.cityModel.stateId= data;
          this.stateName = data.toString();
         }else{
          this.cityModel.stateId= data.value;
          this.stateName = data.value.toString();
         }     
      }            
    }else{
      if(data.value === ""){     
        this.cityModel.stateId= 0;
        this.stateName = '';
        this.cityListArr=[];       
       }else{        
        this.cityModel.stateId= data.value;
        this.stateName = data.value.toString();           
      }    
    }
    

  if (this.cityModel.stateId !== 0)
  {  
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
 }
  submit() {
    
    if (this.form.valid) { 
      
      this.schoolAddViewModel.schoolMaster.schoolDetail[0].schoolLogo = this.image;     
      if ((this.commonFunction.checkEmptyObject(this.dataOfwashInfoFromGeneral) === true)
     || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromView) === true) 
     || (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromWash) === true)){

      if(this.stateCount!==0){
        this.schoolAddViewModel.schoolMaster.country = this.countryName;
        this.schoolAddViewModel.schoolMaster.state = this.stateName;
      }
      else{
        this.schoolAddViewModel.schoolMaster.country = this.schoolAddViewModel.schoolMaster.country.toString();
        this.schoolAddViewModel.schoolMaster.state = this.schoolAddViewModel.schoolMaster.state.toString();
      }
      

      this.schoolAddViewModel.schoolMaster.city = this.schoolAddViewModel.schoolMaster.city.toString();
      this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened);  
      this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed);  
        this.generalInfoService.UpdateSchool(this.schoolAddViewModel).subscribe(data => {
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
              this.generalInfoService.changeMessage(true);
              // this.parentShowWash.emit(this.isEditMode);
              this.onViewMode.emit();
              this.dataOfgeneralInfo.emit(data);
            }
          }

        })
      }else{
        this.schoolAddViewModel.schoolMaster.country = this.schoolAddViewModel.schoolMaster.country.toString();
       this.schoolAddViewModel.schoolMaster.state = this.stateName;
      this.schoolAddViewModel.schoolMaster.state = this.schoolAddViewModel.schoolMaster.state.toString();

      this.schoolAddViewModel.schoolMaster.city = this.schoolAddViewModel.schoolMaster.city.toString();
      this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened);  
      this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed = this.commonFunction.formatDateSaveWithoutTime(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed);  
        this.generalInfoService.AddSchool(this.schoolAddViewModel).subscribe(data => {
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
              let schoolIdToString=data.schoolMaster.schoolId.toString();
              sessionStorage.setItem("selectedSchoolId",schoolIdToString)
              this.generalInfoService.changeMessage(true);
              this.parentShowWash.emit(this.isEditMode);
              this.dataOfgeneralInfo.emit(data);
            }
          }

        })

      }
    }
  }
  onEditCall(){
    this.schoolLevelOptions = Object.keys(this.schoolLevels);
    this.schoolClassificationOptions = Object.keys(this.schoolClassifications);
    this.genderOptions = Object.keys(this.genders);   
   
    
    if (this.commonFunction.checkEmptyObject(this.dataOfgeneralInfoFromView) === true) {
      this.schoolAddViewModel = this.dataOfgeneralInfoFromView;
      this.checkGradeLevelsOnEdit()


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
      if(this.stateCount!==0)    {
       this.schoolAddViewModel.schoolMaster.city = +this.schoolAddViewModel.schoolMaster.city;
       this.schoolAddViewModel.schoolMaster.state = +this.schoolAddViewModel.schoolMaster.state;
      }
      else{
      this.schoolAddViewModel.schoolMaster.city = this.schoolAddViewModel.schoolMaster.city;
      this.schoolAddViewModel.schoolMaster.state = this.schoolAddViewModel.schoolMaster.state;
      }
      this.schoolAddViewModel.schoolMaster.country = +this.schoolAddViewModel.schoolMaster.country;
      if(this.stateCount!==0){
       this.getAllStateByCountry(this.schoolAddViewModel.schoolMaster.country);
       this.getAllCitiesByState(this.schoolAddViewModel.schoolMaster.state);
      }

      this.schoolAddViewModel.schoolMaster.modifiedBy = sessionStorage.getItem('email');
      this.schoolAddViewModel.schoolMaster.dateModifed = this.dateCreated;
      this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened=this.commonFunction.formatDateInEditMode(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolOpened);      
      this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed=this.commonFunction.formatDateInEditMode(this.schoolAddViewModel.schoolMaster.schoolDetail[0].dateSchoolClosed);
      
    }else{     
      this.schoolAddViewModel.schoolMaster.createdBy = sessionStorage.getItem('email');
      this.schoolAddViewModel.schoolMaster.dateCreated = this.dateCreated;     
    }

    this.schoolAddViewModel.schoolMaster.schoolLevel = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolLevel);
    this.schoolAddViewModel.schoolMaster.schoolClassification = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolClassification) ;
    
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].lowestGradeLevel = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].lowestGradeLevel);
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].highestGradeLevel = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].highestGradeLevel);
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].gender = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].gender);
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].telephone = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].telephone) ;
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].website = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].website) ;
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].twitter = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].twitter) ;
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].instagram = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].instagram) ;



    this.schoolAddViewModel.schoolMaster.schoolName = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolName);
    this.schoolAddViewModel.schoolMaster.alternateName = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.alternateName) ;
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].locale = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].locale);
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].gender = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].gender);
    this.schoolAddViewModel.schoolMaster.county = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.county);
    this.schoolAddViewModel.schoolMaster.division = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.division) ;
  
    this.schoolAddViewModel.schoolMaster.district = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.district) ;
    this.schoolAddViewModel.schoolMaster.zip = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.zip) ;
    
  }

}
