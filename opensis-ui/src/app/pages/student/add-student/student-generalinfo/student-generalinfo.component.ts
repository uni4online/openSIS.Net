import { Component, OnInit, Input, ViewChild, ChangeDetectorRef,OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { StudentService } from '../../../../services/student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { CommonService } from '../../../../services/common.service';
import { StudentAddModel} from '../../../../models/studentModel';
import { CountryModel } from '../../../../models/countryModel';
import { LanguageModel } from '../../../../models/languageModel';
import { LoginService } from '../../../../services/login.service';
import * as _moment from 'moment';
import { default as _rollupMoment } from 'moment';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MY_FORMATS } from '../../../shared/format-datepicker';
import { SharedFunction } from '../../../shared/shared-function';
import { salutation,suffix,gender,race,ethnicity,maritalStatus } from '../../../../enums/studentAdd.enum';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import icEdit from '@iconify/icons-ic/edit';
import { Subject } from 'rxjs/internal/Subject';
import {takeUntil } from 'rxjs/operators';
import icVisibility from '@iconify/icons-ic/twotone-visibility';
import icVisibilityOff from '@iconify/icons-ic/twotone-visibility-off';

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
export class StudentGeneralinfoComponent implements OnInit,OnDestroy{
  StudentCreate=SchoolCreate;
  @Input() studentCreateMode:SchoolCreate;
  @Input() studentDetailsForViewAndEdit;
  @ViewChild('f') currentForm: NgForm;
  data;
  icEdit = icEdit; 
  countryListArr=[]; 
  countryName="-";
  nationality="-";
  countryModel: CountryModel = new CountryModel();
  destroySubject$: Subject<void> = new Subject();

  form: FormGroup;
  studentAddModel: StudentAddModel = new StudentAddModel();
  languages: LanguageModel = new LanguageModel();
  salutationEnum=Object.keys(salutation);
  suffixEnum=Object.keys(suffix);
  genderEnum=Object.keys(gender);
  raceEnum=Object.keys(race);
  ethnicityEnum=Object.keys(ethnicity);
  maritalStatusEnum=Object.keys(maritalStatus);
  languageList;
  icVisibility = icVisibility;
  icVisibilityOff = icVisibilityOff;
  inputType = 'password';
  visible = false;


  constructor(
    private fb: FormBuilder,
    public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private _studentService:StudentService,
    private commonService:CommonService,
    private loginService: LoginService,
    private commonFunction:SharedFunction,
    private cd: ChangeDetectorRef) { 
      translateService.use('en');
      this._studentService.getStudentDetailsForGeneral.pipe(takeUntil(this.destroySubject$)).subscribe((res:StudentAddModel)=>{
        this.studentAddModel=res;
        this.data=this.studentAddModel?.studentMaster;
            this.getAllCountry();
      })
    }

  ngOnInit(): void {   
    if(this.studentCreateMode==this.StudentCreate.ADD){
      this.getAllCountry();  
      this.GetAllLanguage();
    }else if(this.studentCreateMode==this.StudentCreate.VIEW){
      this.studentAddModel=this.studentDetailsForViewAndEdit;
      this.data=this.studentDetailsForViewAndEdit?.studentMaster;
    }else if(this.studentCreateMode==this.StudentCreate.EDIT  && (this.studentDetailsForViewAndEdit!=undefined || this.studentDetailsForViewAndEdit!=null)){
      this.studentAddModel=this.studentDetailsForViewAndEdit;
    } 
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
           if(this.studentCreateMode==this.StudentCreate.VIEW){
            this.countryListArr.map((val) => {
              var countryInNumber = +this.data.countryOfBirth;  
              var nationality=+this.data.nationality; 
                if(val.id === countryInNumber){
                  this.countryName= val.name;
                }
                if(val.id === nationality){
                  this.nationality= val.name;
                }
              })  
           }
          }        
        }
      }) 
    }
    editGeneralInfo(){
      this.studentCreateMode=this.StudentCreate.EDIT
    }
    
    GetAllLanguage() {
      this.languages._tenantName = sessionStorage.getItem("tenant");
      this.loginService.getAllLanguage(this.languages).subscribe((res) => {
        this.languageList = res.tableLanguage;          
      })
    }

  submit(){
    this.currentForm.form.markAllAsTouched();
    if (this.currentForm.form.valid) {  
      if( this.studentCreateMode == this.StudentCreate.EDIT) {
        this.updateSchool();
      }else{
        this.addSchool();
      }
    }
  }

  cancelEdit(){
    this.studentCreateMode = this.StudentCreate.VIEW
    this.data=this.studentAddModel.studentMaster;
  }

  updateSchool(){
        this.studentAddModel._token=sessionStorage.getItem("token");
        this.studentAddModel._tenantName=sessionStorage.getItem("tenant");
        this._studentService.UpdateStudent(this.studentAddModel).subscribe(data => {     
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
              this.data=data.studentMaster;
            }
          }
          
      
        });
  }

  addSchool(){
    this.studentAddModel.studentMaster.dob= this.commonFunction.formatDateSaveWithoutTime(this.studentAddModel.studentMaster.dob);  
    this._studentService.AddStudent(this.studentAddModel).subscribe(data => {          
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
          this._studentService.changeCategory(4);
          this._studentService.setStudentDetails(data);     
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
