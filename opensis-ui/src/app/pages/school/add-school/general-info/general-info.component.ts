import { Component, OnInit ,EventEmitter,Output,Input} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {SchoolAddViewModel } from '../../../../models/schoolDetailsModel';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import {fadeInRight400ms} from '../../../../../@vex/animations/fade-in-right.animation';
import { SchoolService } from '../../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import {ActivatedRoute, Router} from '@angular/router';
import * as _moment from 'moment';
import {default as _rollupMoment} from 'moment';
import {MomentDateAdapter} from '@angular/material-moment-adapter';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import { TranslateService } from '@ngx-translate/core';
import { schoolLevel } from '../../../../enums/school_level.enum';
import { schoolClassification } from '../../../../enums/school_classification.enum';
import { gender } from '../../../../enums/gender.enum';
import { country } from '../../../../enums/country.enum';
import { status } from '../../../../enums/status.enum';
import { highestGradeLevel } from '../../../../enums/highest_grade_level.enum';
import {MY_FORMATS } from '../../../shared/format-datepicker';
import { ValidationService } from '../../../shared/validation.service';
import { LoaderService } from 'src/app/services/loader.service';
import { __values } from 'tslib';
import { BehaviorSubject } from 'rxjs'
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
    {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
    {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS},
  ],
})

export class GeneralInfoComponent implements OnInit {
  @Output("parentShowWash") parentShowWash :EventEmitter<any> = new EventEmitter<any>();
  @Output() dataOfgeneralInfo: EventEmitter<any> = new EventEmitter();
  @Input() isEditMode: Boolean;
  @Input() dataOfgeneralInfoFromView: any;
  @Input() dataOfgeneralInfoFromWash: any;
  @Input() dataOfwashInfoFromGeneral:any;
  @Input() image:any;

  private schoolLevels = schoolLevel;
  public schoolLevelOptions = [];
  private schoolClassifications = schoolClassification;
  public schoolClassificationOptions = [];
  private genders = gender;
  public genderOptions = [];
  private statusList = status;
  public statusOption = [];
  private countryList = country;
  public countryOption = [];
  private highestGradeLevels = highestGradeLevel;
  public highestGradeLevelsOption = [];
  public tenant = "";
  form: FormGroup;  
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();  
  schoolViewModel= new BehaviorSubject<SchoolAddViewModel>(this.schoolAddViewModel)
  schoolViewModelObserver= this.schoolViewModel.asObservable()
  loading;
  
  public internet = [
    {"id": true, "name": "Yes"},
    {"id": false, "name": "No"}
  ]
  constructor( private fb: FormBuilder,
    private generalInfoService:SchoolService,
     private snackbar: MatSnackBar,
     private router: Router,
     private Activeroute: ActivatedRoute,
     public translateService:TranslateService,
     private loaderService:LoaderService) {
       
      translateService.use('en');
      this.Activeroute.params.subscribe(params => { this.tenant = 'OpensisV2'; });
      this.loaderService.isLoading.subscribe((val) => {
        this.loading = val;
      });
      
    }

  ngOnInit(): void {
    this.form = this.fb.group(
      {
      school_Name: ['', Validators.required],    
      alternate_name: ['',],
      school_id: ['', Validators.required],
      alternate_id: [''],
      school_level: ['', Validators.required],
      school_classification: ['', Validators.required],
      affiliation: [''],
      associations: [''],
      lowest_grade_level: ['', Validators.required],
      highest_grade_level: ['', Validators.required],
      date_school_opened: [''],
      date_school_closed: [''],
      code: [''],
      gender: [''],
      internet: [''],
      electricity: [''],
      status: [''],
      street_address_1: ['', Validators.required],
      street_address_2: [''],
      city: ['', Validators.required],
      country: ['', Validators.required],
      division: [''],
      district: [''],
      zip: ['', Validators.required],
      state: [''],
      latitude: [''],
      longitude: [''],
      principal: ['',Validators.required],
      ass_principal: [''],
      telephone: ['',[Validators.required] ],
      fax: [''],
      website: ['',ValidationService.websiteValidator],
      email: ['',ValidationService.emailValidator],
      twitter: [''],
      facebook: [''],
      instagram: [''],
      youtube: [''],
      linkedin: [''],
      county: [''],
    });

    this.schoolLevelOptions = Object.keys(this.schoolLevels);   
    this.schoolClassificationOptions = Object.keys(this.schoolClassifications);
    this.genderOptions = Object.keys(this.genders);
    this.statusOption = Object.keys(this.statusList);
    this.countryOption = Object.keys(this.countryList);
    this.highestGradeLevelsOption = Object.keys(this.highestGradeLevels);
    
   
    if(Object.keys(this.dataOfgeneralInfoFromView).length !== 0)
    {
      this.schoolAddViewModel=this.dataOfgeneralInfoFromView;
      
      
    }else if(Object.keys(this.dataOfwashInfoFromGeneral).length !== 0){ 
      this.schoolAddViewModel=this.dataOfwashInfoFromGeneral;
     
    }   
    else if(Object.keys(this.dataOfgeneralInfoFromWash).length !== 0){
        this.schoolAddViewModel=this.dataOfgeneralInfoFromWash;
      
      }
    
      this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Level= this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Level !== "" ? this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Level.trim():this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Level; 
      this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Classification= this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Classification !== null ? this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Classification.trim():this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Classification; 
      this.schoolAddViewModel.tblSchoolDetail.lowest_Grade_Level= this.schoolAddViewModel.tblSchoolDetail.lowest_Grade_Level !== null ? this.schoolAddViewModel.tblSchoolDetail.lowest_Grade_Level.trim():this.schoolAddViewModel.tblSchoolDetail.lowest_Grade_Level; 
      this.schoolAddViewModel.tblSchoolDetail.highest_Grade_Level= this.schoolAddViewModel.tblSchoolDetail.highest_Grade_Level !== null ? this.schoolAddViewModel.tblSchoolDetail.highest_Grade_Level.trim():this.schoolAddViewModel.tblSchoolDetail.highest_Grade_Level; 
      this.schoolAddViewModel.tblSchoolDetail.gender= this.schoolAddViewModel.tblSchoolDetail.gender !== null ? this.schoolAddViewModel.tblSchoolDetail.gender.trim():this.schoolAddViewModel.tblSchoolDetail.gender; 
      this.schoolAddViewModel.tblSchoolDetail.telephone= this.schoolAddViewModel.tblSchoolDetail.telephone !== null ? this.schoolAddViewModel.tblSchoolDetail.telephone.trim():this.schoolAddViewModel.tblSchoolDetail.telephone; 
  }

  get f() 
  {
     return this.form.controls;
  }
  dateCompare(){
    let openingDate=this.form.controls.date_school_opened.value
    let closingDate=this.form.controls.date_school_closed.value
   
    if(ValidationService.compareValidation(openingDate,closingDate)===false){
      this.form.controls.date_school_closed.setErrors({ compareError: false })
      console.log(this.form.controls.date_school_closed)
    }
    
  }
 
 
  submit() {    
    if (this.form.valid) { 
      this.schoolAddViewModel._tenantName = this.tenant; 
      this.schoolAddViewModel._token = sessionStorage.getItem("token"); 
      this.schoolAddViewModel.tblSchoolDetail.school_Logo=this.image;
       (Object.keys(this.dataOfgeneralInfoFromWash).length > 0) || (Object.keys(this.dataOfgeneralInfoFromView).length > 0) || (Object.keys(this.dataOfwashInfoFromGeneral).length > 0) ?
        this.generalInfoService.UpdateGeneralInfo(this.schoolAddViewModel).subscribe(data => {
          if(typeof(data)=='undefined')
          {
            this.snackbar.open('General Info Updation failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else
          {
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
        :
       
        this.generalInfoService.SaveGeneralInfo(this.schoolAddViewModel).subscribe(data => {
          if(typeof(data)=='undefined')
          {
            this.snackbar.open('General Info Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else
          {
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
