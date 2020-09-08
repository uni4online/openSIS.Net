import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GeneralInfoModel } from '../../../../models/generalInfoModel';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import {fadeInRight400ms} from '../../../../../@vex/animations/fade-in-right.animation';
import { GeneralInfoService } from '../../../../services/general-info.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
import * as _moment from 'moment';
import {default as _rollupMoment} from 'moment';
import {MomentDateAdapter} from '@angular/material-moment-adapter';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import { TranslateService } from '@ngx-translate/core';
import { schoolLevel } from '../../../../enums/school_level.enum';
import { schoolClassification } from '../../../../enums/school_classification.enum';
import { gender } from '../../../../enums/gender.enum';
import { country } from '../../../../enums/country.enum';
import { highestGradeLevel } from '../../../../enums/highest_grade_level.enum';
import {MY_FORMATS } from '../../../shared/format-datepicker'
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
  form: FormGroup;  
  UserModel: GeneralInfoModel = new GeneralInfoModel();
  
  constructor( private fb: FormBuilder,
    private generalInfoService:GeneralInfoService,
     private snackbar: MatSnackBar,
     private router: Router,
     public translateService:TranslateService) {
      translateService.use('en');
      }

  ngOnInit(): void {
    this.form = this.fb.group({
      school_name: ['', Validators.required],    
      alternate_name: ['',],
      school_id: ['', Validators.required],
      alternate_id: [''],
      school_level: ['', Validators.required],
      school_classification: ['', Validators.required],
      affiliation: [''],
      associations: [''],
      lowest_grade_level: ['', Validators.required],
      highest_grade_level: ['', Validators.required],
      date_school_opened: [moment()],
      date_school_closed: [moment()],
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
      telephone: ['', Validators.required],
      fax: [''],
      website: [''],
      email: [''],
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
  }

  get f() 
  {
     return this.form.controls;
  }

  submit() {    
    if (this.form.dirty && this.form.valid) {  
     
      this.generalInfoService.SaveGeneralInfo(this.UserModel).subscribe(data => {
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
          
            
          }
        }
        
      })

    } 
  }   

}
