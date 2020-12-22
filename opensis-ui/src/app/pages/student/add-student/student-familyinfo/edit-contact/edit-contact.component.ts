import { Component, OnInit ,Inject,ViewChild,Input} from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, NgForm, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import icBack from '@iconify/icons-ic/baseline-arrow-back';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { ParentInfoService } from '../../../../../services/parent-info.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { AddParentInfoModel,ParentInfoList,AssociateStudent } from '../../../../../models/parentInfoModel';
import { salutation,suffix ,relationShip,userProfile,Custody} from '../../../../../enums/studentAdd.enum';
import { CountryModel } from '../../../../../models/countryModel';
import { CommonService } from '../../../../../services/common.service';
import { SharedFunction } from '../../../../shared/shared-function';

@Component({
  selector: 'vex-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]

})
export class EditContactComponent implements OnInit {
  @ViewChild('f') currentForm: NgForm;
  f: NgForm;
  search: NgForm;
  associateStudents: NgForm;
  associateMultipleStudents:NgForm;
  icClose = icClose;
  icBack = icBack; 
  addParentInfoModel: AddParentInfoModel = new AddParentInfoModel(); 
  parentInfoList:ParentInfoList=new ParentInfoList();
  associateStudent:AssociateStudent=new AssociateStudent();
  contactModalTitle="addContact";
  contactModalActionTitle="submit";
  isEdit=false;
  salutationEnum=Object.keys(salutation);
  suffixEnum = Object.keys(suffix);
  relationShipEnum = Object.keys(relationShip); 
  userProfileEnum = Object.keys(userProfile);
  custodyEnum = Custody;
  mode;
  viewData:any;
  countryModel: CountryModel = new CountryModel();
  countryListArr=[]; 
  countryName="-";
  mailingAddressCountry="-";
  val;
  isPortalUser=false;
  sameAsStudentAddress:boolean=false;
  disableAddressFlag;
  singleParentInfo;
  multipleParentInfo=[];
  editMode=false;
  isCustodyCheck=false;
  disablePassword=false;
  studentDetailsForViewAndEditDataDetails;
  constructor(
      private dialogRef: MatDialogRef<EditContactComponent>,
      private fb: FormBuilder, 
      public translateService:TranslateService,
      private parentInfoService:ParentInfoService,
      private snackbar: MatSnackBar,
      private router:Router,
      private commonService:CommonService,
      private commonFunction:SharedFunction,
      @Inject(MAT_DIALOG_DATA) public data
    ) 
    { }

  ngOnInit(): void {  
    this.studentDetailsForViewAndEditDataDetails=this.data.studentDetailsForViewAndEditData; 
    this.getAllCountry(); 
   
      if(this.data.mode === "view"){       
       this.mode = "view";
       this.viewData = this.data.parentInfo; 
       if(this.viewData.middlename === null){
        this.viewData.middlename = "";
       }  
      }else{
        if(this.data.mode === "add"){
        this.disableAddressFlag =true;
        this.val="No";     
        this.addParentInfoModel.parentInfo.contactType = this.data.contactType;    
        this.addParentInfoModel.parentInfo.studentAddressSame = true;    
        }else{
                    
          this.addParentInfoModel.parentInfo = this.data.parentInfo;     
          this.addParentInfoModel.parentInfo.country = +this.data.parentInfo.country;                 
          this.editMode=true;
          this.disablePassword = true;
          this.addParentInfoModel.parentInfo.relationship= this.commonFunction.trimData(this.data.parentInfo.relationship);
        
          if(this.data.parentInfo.studentAddressSame === true){
            this.val="Yes";
            this.sameAsStudentAddress=true;
            
          }else{
            this.val="No";
            this.sameAsStudentAddress=false;
          }
          if(this.addParentInfoModel.parentInfo.isPortalUser === true){
            this.isPortalUser = true;
            this.addParentInfoModel.parentInfo.isPortalUser = true; 
          }else{
            this.isPortalUser = false;
            this.addParentInfoModel.parentInfo.isPortalUser = false; 
          }
          if(this.addParentInfoModel.parentInfo.isCustodian === true){
            this.isCustodyCheck = true;
            this.addParentInfoModel.parentInfo.isCustodian = true; 
            this.disableAddressFlag = false;
           
          }else{
            this.isCustodyCheck = false;
            this.addParentInfoModel.parentInfo.isCustodian = false; 
            this.disableAddressFlag =true;
          }  
          this.addParentInfoModel.parentInfo.contactType = this.data.parentInfo.contactType;       
        }
        this.mode = "add";     
      }   
      
      this.addParentInfoModel.parentInfo.userProfile = "Parent";
  }
  disableAddress(event){
    if(event.value === true){
      this.disableAddressFlag = false;
    }else{
      this.disableAddressFlag = true;
      this.val="No";
      this.addParentInfoModel.parentInfo.studentAddressSame = false;
      this.sameAsStudentAddress = false;
    }
  }
  associateStudentToParent(){    
    let isCustodian=this.associateStudent.isCustodian;
    let contactRelationship=this.associateStudent.contactRelationship;
    if(contactRelationship === undefined){
      contactRelationship = "";
     }   
     if(isCustodian === undefined){
      isCustodian = false
     }
    this.singleParentInfo.isCustodian = isCustodian;
    this.singleParentInfo.relationShip = contactRelationship; 
    this.singleParentInfo.tenantId =  sessionStorage.getItem("tenantId");
    this.singleParentInfo.schoolId = +sessionStorage.getItem("selectedSchoolId");  
    this.singleParentInfo.studentId = this.data.studentDetailsForViewAndEditData.studentMaster.studentId;  
    delete this.singleParentInfo.getStudentForView;
    this.addParentInfoModel.parentInfo= this.singleParentInfo; 
    this.addParentInfoModel.parentInfo.contactType = this.data.contactType;   
    this.submit();
  }

  associateMultipleStudentsToParent(){
   var isCustodian=this.associateStudent.isCustodian;
   var contactRelationship=this.associateStudent.contactRelationship;  
   if(contactRelationship === undefined){
    contactRelationship = "";
   }   
   if(isCustodian === undefined){
    isCustodian = false
   }
   let obj = {
    'isCustodian':isCustodian,
    'relationship':contactRelationship,
    "tenantId": sessionStorage.getItem("tenantId"),
    "schoolId": +sessionStorage.getItem("selectedSchoolId"),
    "studentId": this.data.studentDetailsForViewAndEditData.studentMaster.studentId,
    "contactType" : this.data.contactType
    
    }   
   return obj;
  }
  getIndexOfParentInfo(data){   
    let obj = this.associateMultipleStudentsToParent();   
    if(obj.relationship === ""){
      this.snackbar.open('Please provide Relationship','', {
        duration: 10000
      });
    }else{
      let concatObj = {...data,...obj};
      delete concatObj.getStudentForView;
      this.addParentInfoModel.parentInfo= concatObj;   
        
      this.submit();
    }
    
  }
  copyEmailAddress(emailType){
    if(emailType === "personal"){
      if(this.currentForm.form.controls.personalEmail.value !== ""){
        this.addParentInfoModel.parentInfo.loginEmail =this.currentForm.form.controls.personalEmail.value;
      }      
    }else{
      if(this.currentForm.form.controls.workEmail.value !== ""){
        this.addParentInfoModel.parentInfo.loginEmail =this.currentForm.form.controls.workEmail.value;
      }
    }
  }
  
  getAllCountry(){  
    this.commonService.GetAllCountry(this.countryModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.countryListArr = [];
      }
      else {
        if (data._failure) {
          this.countryListArr = [];
        } else {
          this.countryListArr = data.tableCountry; 
          if(this.mode === "view"){
            this.countryListArr.map((val) => {
              var countryInNumber = +this.viewData.country;            
                if(val.id === countryInNumber){
                  this.viewData.country= val.name;
                }               
              })     
          }  
             
        }
      }

    })
  }
  closeDialog(){
    this.dialogRef.close(false);
  }
  searchExistingContact(){
    this.mode="search";
  }  
  backToAdd(){
    this.mode="add";
  }
  backToSearch(){
    this.mode="search";
  }
  radioChange(event){
    if(event.value === "Yes"){
      this.sameAsStudentAddress = true;
      this.addParentInfoModel.parentInfo.studentAddressSame = true;
    
    }else{
      this.sameAsStudentAddress = false;
      this.addParentInfoModel.parentInfo.studentAddressSame = false;
     
    }    
  }
  custodyCheck(event){
    if(event.checked === true){
      this.isCustodyCheck = true;
      this.addParentInfoModel.parentInfo.isCustodian = true; 
    }else{
      this.isCustodyCheck = true;
      this.addParentInfoModel.parentInfo.isCustodian = false; 
    }
  }
  portalUserCheck(event){
    if(event.checked === true){
      this.isPortalUser = true;
      this.addParentInfoModel.parentInfo.isPortalUser = true; 
    }else{
      this.isPortalUser = false;
      this.addParentInfoModel.parentInfo.isPortalUser = false; 
    }
  }
  submit() {
    if(this.editMode === true){
      this.addParentInfoModel.parentInfo.country = this.addParentInfoModel.parentInfo.country.toString();
      this.addParentInfoModel.parentInfo.studentId = this.data.studentDetailsForViewAndEditData.studentMaster.studentId;
      this.parentInfoService.updateParentInfo(this.addParentInfoModel).subscribe(data => {
        if (typeof (data) == 'undefined') 
        {
          this.snackbar.open('Parent Information Updation failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else 
        {
          if (data._failure) {
            this.snackbar.open('Parent Information Updation failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          }
          else 
          {
            this.snackbar.open('Parent Information Updation Successful.', '', {
            duration: 10000
            });
            this.dialogRef.close(true);            
          }
        }
      })     
    }else{

      this.addParentInfoModel.parentInfo.studentId = this.data.studentDetailsForViewAndEditData.studentMaster.studentId;
      
      if(this.sameAsStudentAddress === true){
        this.addParentInfoModel.parentInfo.city = this.data.studentDetailsForViewAndEditData.studentMaster.homeAddressCity;
        this.addParentInfoModel.parentInfo.country = this.data.studentDetailsForViewAndEditData.studentMaster.homeAddressCountry;
        this.addParentInfoModel.parentInfo.addressLineOne = this.data.studentDetailsForViewAndEditData.studentMaster.homeAddressLineOne;
        this.addParentInfoModel.parentInfo.addressLineTwo = this.data.studentDetailsForViewAndEditData.studentMaster.homeAddressLineTwo;
        this.addParentInfoModel.parentInfo.state = this.data.studentDetailsForViewAndEditData.studentMaster.homeAddressState;
        this.addParentInfoModel.parentInfo.zip = this.data.studentDetailsForViewAndEditData.studentMaster.homeAddressZip;
      }
      if(this.addParentInfoModel.parentInfo.country !== null){
        this.addParentInfoModel.parentInfo.country = this.addParentInfoModel.parentInfo.country.toString();
      }    
     
      this.parentInfoService.addParentForStudent(this.addParentInfoModel).subscribe(data => {
        if (typeof (data) == 'undefined') 
        {
          this.snackbar.open('Parent Information Submission failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else 
        {
          if (data._failure) {
            this.snackbar.open('Parent Information Submission failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          }
          else 
          {
            this.snackbar.open('Parent Information Submission Successful.', '', {
            duration: 10000
            });
            
            this.dialogRef.close(true); 
            
                       
          }
        }
      })     
    }      
  }
    
    searchParent(){      
      if(this.parentInfoList.firstname === null && this.parentInfoList.lastname === null && this.parentInfoList.mobile === null &&
        this.parentInfoList.email === null && this.parentInfoList.streetAddress === null && this.parentInfoList.city === null &&
        this.parentInfoList.state === null  && this.parentInfoList.zip === null     
        ){
          this.snackbar.open('Please provide atleast one search field', '', {
            duration: 10000
            });
        }else{
          this.parentInfoList.studentId = this.data.studentDetailsForViewAndEditData.studentMaster.studentId;
          this.parentInfoService.searchParentInfoForStudent(this.parentInfoList).subscribe(data => {
            if (typeof (data) == 'undefined') 
            {
              this.snackbar.open('Search Parent Information failed. ' + sessionStorage.getItem("httpError"), '', {
                duration: 10000
              });
            }
            else 
            {
              if (data._failure) {
                this.snackbar.open('Search Parent Information failed. ' + data._message, 'LOL THANKS', {
                  duration: 10000
                });
              }
              else 
              {       
                if(data.parentInfoForView.length > 1){
                  this.mode="multipleResult";
                  this.multipleParentInfo =data.parentInfoForView ;
                 
                }else if(data.parentInfoForView.length == 0){
                  this.snackbar.open('No Record Found', '', {
                    duration: 10000
                    });
                  this.mode="search";
                  this.singleParentInfo={};              
                } else{
                  this.mode="singleResult";                  
                  data.parentInfoForView.map((val,index) => {
                     this.singleParentInfo = val;     
                  }) 
                }         
              }
            }
          })     
        }
    }
}
