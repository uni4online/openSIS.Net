import { Component, OnInit, EventEmitter, Output, Input ,ViewChild} from '@angular/core';
import { FormBuilder, NgForm, Validators } from '@angular/forms';
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
import icCheckBox from '@iconify/icons-ic/check-box';
import icCheckBoxOutlineBlank from '@iconify/icons-ic/check-box-outline-blank';
import icEdit from '@iconify/icons-ic/edit';
import { ImageCropperService } from '../../../../services/image-cropper.service';

@Component({
  selector: 'vex-student-addressandcontacts',
  templateUrl: './student-addressandcontacts.component.html',
  styleUrls: ['./student-addressandcontacts.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ],
})
export class StudentAddressandcontactsComponent implements OnInit {
  @Input() studentDetailsForViewAndEdit;
  @ViewChild('f') currentForm: NgForm;
  f: NgForm;
  icEdit = icEdit;
  icCheckBox = icCheckBox;
  icCheckBoxOutlineBlank = icCheckBoxOutlineBlank;
  countryListArr=[]; 
  countryName="-";
  mailingAddressCountry="-";
  countryModel: CountryModel = new CountryModel();
  data;
  studentCreate=SchoolCreate;
  @Input() studentCreateMode:SchoolCreate;
  studentAddModel: StudentAddModel = new StudentAddModel();
  languageList;
  checkBoxChecked = false; 
  actionButtonTitle="submit" 

  constructor(public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private studentService:StudentService,
    private commonService:CommonService,
    private loginService: LoginService,
    private commonFunction:SharedFunction,
    private imageCropperService:ImageCropperService) { 
      translateService.use('en');
    }

  ngOnInit(): void {  
    if(this.studentCreateMode==this.studentCreate.VIEW){
      this.studentService.changePageMode(this.studentCreateMode);
      this.data=this.studentDetailsForViewAndEdit?.studentMaster;
      this.studentAddModel = this.studentDetailsForViewAndEdit;
      this.imageCropperService.enableUpload(false);
      this.getAllCountry();   
    }else{
      this.studentService.changePageMode(this.studentCreateMode);
      this.studentAddModel = this.studentService.getStudentDetails();
    }

    this.getAllCountry();
  }
  
  editAddressContactInfo(){
    this.studentCreateMode=this.studentCreate.EDIT;
    this.studentService.changePageMode(this.studentCreateMode);
    this.actionButtonTitle="update";
    this.imageCropperService.enableUpload(true);
  }

  cancelEdit(){
    this.studentCreateMode = this.studentCreate.VIEW;
    this.studentService.changePageMode(this.studentCreateMode);
    this.data=this.studentAddModel.studentMaster; 
    this.imageCropperService.enableUpload(false);
  }

  copyHomeAddress(check){
 
    if(this.studentAddModel.studentMaster.mailingAddressSameToHome === false || this.studentAddModel.studentMaster.mailingAddressSameToHome === null){
     
      if(this.studentAddModel.studentMaster.homeAddressLineOne !== undefined && this.studentAddModel.studentMaster.homeAddressCity !== undefined &&
        this.studentAddModel.studentMaster.homeAddressState !== undefined && this.studentAddModel.studentMaster.homeAddressZip !== undefined ){
         
      this.studentAddModel.studentMaster.mailingAddressLineOne=this.studentAddModel.studentMaster.homeAddressLineOne;
      this.studentAddModel.studentMaster.mailingAddressLineTwo=this.studentAddModel.studentMaster.homeAddressLineTwo;
      this.studentAddModel.studentMaster.mailingAddressCity=this.studentAddModel.studentMaster.homeAddressCity;
      this.studentAddModel.studentMaster.mailingAddressState=this.studentAddModel.studentMaster.homeAddressState;
      this.studentAddModel.studentMaster.mailingAddressZip=this.studentAddModel.studentMaster.homeAddressZip;
      this.studentAddModel.studentMaster.mailingAddressCountry=+this.studentAddModel.studentMaster.homeAddressCountry;

    }else{
     
      this.checkBoxChecked = check ? true : false;
      this.snackbar.open('Please Provide All Mandatory Fields First', '', {
        duration: 10000
      });
    }
     
    }else{
      this.studentAddModel.studentMaster.mailingAddressLineOne="";
      this.studentAddModel.studentMaster.mailingAddressLineTwo="";
      this.studentAddModel.studentMaster.mailingAddressCity="";
      this.studentAddModel.studentMaster.mailingAddressState="";
      this.studentAddModel.studentMaster.mailingAddressZip="";
      this.studentAddModel.studentMaster.mailingAddressCountry=null;
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
         if(this.studentCreateMode==this.studentCreate.VIEW){
          this.countryListArr.map((val) => {
            var countryInNumber = +this.data.homeAddressCountry;  
            var mailingAddressCountry=+this.data.mailingAddressCountry; 
              if(val.id === countryInNumber){
                this.countryName= val.name;
              }
              if(val.id === mailingAddressCountry){
                this.mailingAddressCountry= val.name;
              }
            })  
         }
        }        
      }
    }) 
  }
  submit(){
    this.studentService.UpdateStudent(this.studentAddModel).subscribe(data => {                        
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Student Updation failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('Student Updation failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {    
          this.snackbar.open('Student Update Successful.', '', {
            duration: 10000
          });
        this.studentCreateMode = this.studentCreate.VIEW
        this.data=data.studentMaster;
        this.studentService.changePageMode(this.studentCreateMode);
        }
      }
  
    })
  }

}
