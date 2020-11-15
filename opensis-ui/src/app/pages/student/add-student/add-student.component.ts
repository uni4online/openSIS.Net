import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import {fadeInRight400ms} from '../../../../@vex/animations/fade-in-right.animation';
import { ImageCropperService } from 'src/app/services/image-cropper.service';
import { SchoolAddViewModel } from '../../../models/schoolMasterModel';
import { ActivatedRoute } from '@angular/router';
import { SchoolService } from '../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedFunction } from '../../../pages/shared/shared-function';
import { LayoutService } from 'src/@vex/services/layout.service';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import icSchool from '@iconify/icons-ic/outline-school';
import icCalendar from '@iconify/icons-ic/outline-calendar-today';
import icAlarm from '@iconify/icons-ic/outline-alarm';
import icPoll from '@iconify/icons-ic/outline-poll';
import icAccessibility from '@iconify/icons-ic/outline-accessibility';
import icHowToReg from '@iconify/icons-ic/outline-how-to-reg';
import icBilling from '@iconify/icons-ic/outline-monetization-on';
import { StudentService } from '../../../services/student.service';
import { StudentAddModel} from '../../../models/studentModel';
@Component({
  selector: 'vex-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.scss'],
  animations: [  
    fadeInRight400ms,
    stagger60ms,
    fadeInUp400ms
  ]
})
export class AddStudentComponent implements OnInit {
  clickEventSubscriptionForCrop:Subscription;
  clickEventSubscriptionForUnCrop:Subscription; 
  icSchool = icSchool;
  icCalendar = icCalendar;
  icAlarm = icAlarm;
  icPoll = icPoll;
  icAccessibility = icAccessibility;
  icHowToReg = icHowToReg;
  icBilling = icBilling; 
  pageId: string;
  disabledAddress;
  disabledEnrollment;
  disabledFamily;
  disabledLogin;
  viewGeneralInfo=false;
  data;
  image:string='';
  passImageDataToChildren;
  isViewMode=false;
  isEditMode=false;
  editDataOfGeneralInfo;
  saveDataFromGeneralInfo;
  responseImage;
  modeForImage=true;
  enableCropTool=true;
  studentAddModel: StudentAddModel = new StudentAddModel();
  constructor(private layoutService: LayoutService,
    private studentService:StudentService,
    private commonFunction:SharedFunction,
    private imageCropperService:ImageCropperService) {

    this.layoutService.collapseSidenav();

    this.clickEventSubscriptionForCrop=this.imageCropperService.getCroppedEvent().subscribe((res)=>{
      this.image=res[1];
    
    });  
    this.clickEventSubscriptionForUnCrop=this.imageCropperService.getUncroppedEvent().subscribe((res)=>{    
      this.image=btoa(res.target.result);    
    });
  
  }

  ngOnInit(): void {  
    this.modeForImage=true;
    this.studentService.currentData.subscribe(data => this.data = data);   
    if(this.commonFunction.checkEmptyObject(this.data) === true){   
      localStorage.setItem("pageId","GeneralInfo"); 
      this.pageId=localStorage.getItem("pageId");   
      this.isViewMode=true; 
      this.isEditMode=false;   
      this.imageResponse(this.data); 
    }else{  
      localStorage.setItem("pageId","GeneralInfo"); 
      this.pageId=localStorage.getItem("pageId");    
      this.disableSection();   
      this.isViewMode=false;  
      this.isEditMode=false;       
    }     
   
  }

  imageResponse(data){
    if(this.commonFunction.checkEmptyObject(data) === true){ 
      this.responseImage = this.data.studentPhoto;
    }    
  }
  disableSection(){
    
    if(this.pageId == "GeneralInfo"){     
      this.disabledAddress=true;
      this.disabledEnrollment=true;
      this.disabledFamily=true;
      this.disabledLogin=true;
    }else if(this.pageId == "AddressContacts"){     
      this.disabledEnrollment=true;
      this.disabledFamily=true;
      this.disabledLogin=true;
    }else if(this.pageId == "SchoolEnrollmentInfo"){      
      this.disabledFamily=true;
      this.disabledLogin=true;
    }else if(this.pageId == "FamilyInfo"){
      this.disabledLogin=true;
    }
  }
  showPage(pageId) { 
    localStorage.setItem("pageId",pageId); 
    this.pageId=localStorage.getItem("pageId");    
    this.disableSection();
  }
    
  getEditData(data){
   if(Object.keys(data).length === 0){
    this.isEditMode=false;
    this.isViewMode=false;
    this.editDataOfGeneralInfo="";
   }else{
    this.isEditMode=true;
    this.isViewMode=false;
    this.editDataOfGeneralInfo=data;
    this.responseImage= data.studentPhoto;
    this.image = this.responseImage;
   }
   

  }
  editSuccessfull(data){
    if(data){
      this.isEditMode=false;
      this.isViewMode=true;
    }else{
      this.isEditMode=true;
      this.isViewMode=false;
    }
  }

  saveSuccessfullData(data){
    this.saveDataFromGeneralInfo=data;
  }
 
}
