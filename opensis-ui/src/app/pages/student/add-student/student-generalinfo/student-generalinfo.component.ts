import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
export class StudentGeneralinfoComponent implements OnInit {
  @Output() pageIdFromChild: EventEmitter<string> = new EventEmitter();
  @Output() editSuccessfull: EventEmitter<boolean> = new EventEmitter();
  @Output() saveSuccessfullData: EventEmitter<object> = new EventEmitter();
  @Input() image;
  @Input() editDataOfGeneralInfo;
 
  form: FormGroup;
  studentAddModel: StudentAddModel = new StudentAddModel();
  countryModel:CountryModel = new CountryModel();
  languages: LanguageModel = new LanguageModel();
  salutationEnum=Object.keys(salutation);
  suffixEnum=Object.keys(suffix);
  genderEnum=Object.keys(gender);
  raceEnum=Object.keys(race);
  ethnicityEnum=Object.keys(ethnicity);
  maritalStatusEnum=Object.keys(maritalStatus);
  countryListArr=[];
  languageList;

  constructor(
    private fb: FormBuilder,
    public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private studentService:StudentService,
    private commonService:CommonService,
    private loginService: LoginService,
    private commonFunction:SharedFunction) { 
      translateService.use('en');
    }

  ngOnInit(): void {    
      this.getAllCountry();  
      this.GetAllLanguage();
      if(this.editDataOfGeneralInfo !== undefined && Object.keys(this.editDataOfGeneralInfo).length !== 0){
        this.studentAddModel.studentMaster = this.editDataOfGeneralInfo; 
        this.studentAddModel.studentMaster.dob= this.commonFunction.formatDateInEditMode(this.studentAddModel.studentMaster.dob)    
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
           
          }        
        }
      }) 
    }
    GetAllLanguage() {
      this.languages._tenantName = sessionStorage.getItem("tenant");
      this.loginService.getAllLanguage(this.languages).subscribe((res) => {
        this.languageList = res.tableLanguage;          
      })
    }
  submit(){
    
     if(this.editDataOfGeneralInfo === undefined){      
        this.studentAddModel.studentMaster.dob= this.commonFunction.formatDateSaveWithoutTime(this.studentAddModel.studentMaster.dob);  
        this.studentAddModel.studentMaster.studentPhoto= this.image;        
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
              this.pageIdFromChild.emit("AddressContacts");    
              this.saveSuccessfullData.emit(data);       
            }
          }
      
        })
      }else{
        this.studentAddModel.studentMaster.studentPhoto= this.image;  
        this.studentService.UpdateStudent(this.studentAddModel).subscribe(data => {     
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
              this.snackbar.open('Student Update Successful.', '', {
                duration: 10000
              });  
              this.pageIdFromChild.emit("GeneralInfo");         
              this.editSuccessfull.emit(true);  
            }
          }
      
        })

      }
  
  }

}
