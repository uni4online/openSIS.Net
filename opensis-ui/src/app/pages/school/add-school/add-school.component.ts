import { Component, OnInit} from '@angular/core';
import { Subscription } from 'rxjs';
import {fadeInRight400ms} from '../../../../@vex/animations/fade-in-right.animation';
import { ImageCropperService } from '../../../services/image-cropper.service';

import { SchoolAddViewModel } from '../../../models/schoolMasterModel';
import { ActivatedRoute } from '@angular/router';
import { SchoolService } from '../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedFunction } from '../../../pages/shared/shared-function';
import { CommonService } from '../../../services/common.service';
@Component({
  
  selector: 'vex-add-school',
  templateUrl: './add-school.component.html',
  styleUrls: ['./add-school.component.scss'],
  animations: [  
    fadeInRight400ms    
  ]
})
export class AddSchoolComponent implements OnInit {
  clickEventSubscriptionForCrop:Subscription;
  clickEventSubscriptionForUnCrop:Subscription;  
  responseImage:string;
  image:string='';
  displayGeneral = true;
  displayWash = false;
  generalFlag: boolean = true;
  washFlag: boolean = false;
  isEditMode:boolean=false;
  dataOfgeneralInfo;
  id;
  schoolId:number=0;  
  displayViewWash:boolean=false;
  displayViewGeneral:boolean=false;
  isViewMode:boolean=false;  
  dataOfgeneralInfoFromView;
  dataOfWashInfoFromView;
  disabledWashInfo:boolean=false;  
  dataOfgeneralInfoFromWash;
  dataOfwashInfoFromGeneral;
  isAddMode:boolean=false;
  getImageResponse="";
  imageObj;
  enableCropTool=false;
  modeForImage=true;
  message:string;
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  generalAndWashInfoData: SchoolAddViewModel = new SchoolAddViewModel();;
  constructor(private imageCropperService:ImageCropperService,
    private Activeroute: ActivatedRoute,
     private snackbar: MatSnackBar,
     private schoolService:SchoolService,
     private commonFunction:SharedFunction) { 
   // this.Activeroute.params.subscribe(params => { this.tenant ='opensisv2'; });

    this.clickEventSubscriptionForCrop=this.imageCropperService.getCroppedEvent().subscribe((res)=>{
     
        this.image=res;
      });
    
    this.clickEventSubscriptionForUnCrop=this.imageCropperService.getUncroppedEvent().subscribe((res)=>{    
      //this.image='data:image/png;base64,'+btoa(res.target.result);
      this.image=btoa(res.target.result);    
    });
  }

  ngOnInit() { 
    this.modeForImage=true;
    this.schoolId=this.schoolService.getSchoolId();
    if(this.schoolId!=null){
    this.getSchoolGeneralandWashInfoDetails();
    }
    this.disabledWashInfo= true;   
   }

  getDataOfgeneralInfo(data){   
    this.dataOfgeneralInfo=data;    
  }

  getDataOfgeneralInfoFromView(data){  
   
    if(this.commonFunction.checkEmptyObject(data) === true){
      this.dataOfgeneralInfoFromView=data; 
      this.responseImage = this.generalAndWashInfoData.schoolMaster.schoolDetail[0]?.schoolLogo;
      this.image = this.responseImage;
    }   
    
  }

  getDataOfWashInfoFromView(data){   
    this.dataOfWashInfoFromView=data;   
  }
  
  
  showGeneralEdit(){ 
    
    if(this.isAddMode){
      this.isEditMode=false; 
    }else{
      this.isEditMode=true; 
    }  
    this.dataOfgeneralInfoFromWash=this.dataOfgeneralInfo;
    this.dataOfwashInfoFromGeneral=this.dataOfWashInfoFromView;
   
    this.displayGeneral = true;
    this.displayWash = false;
    this.generalFlag = true;
    this.washFlag = false;   
    this.displayViewWash=false;
    this.displayViewGeneral=false;    
    this.disabledWashInfo= true;
  }

  showWashEdit(data){ 
   
    if(data){     
      this.isViewMode=false;
    }  
    this.displayGeneral = false;
    this.displayWash = true;
    this.generalFlag = false;
    this.washFlag = true;
    this.isEditMode=true; 
    this.displayViewWash=false;
    this.displayViewGeneral=false;   
  }
  imageResponse(data){
    if(this.commonFunction.checkEmptyObject(data) === true){ 
      this.responseImage = this.generalAndWashInfoData.schoolMaster.schoolDetail[0]?.schoolLogo;
    }    
  }
  showViewGeneralInfo(){ 
     
    this.displayGeneral = false;
    this.displayWash = false;
    this.generalFlag = true;
    this.washFlag = false;
    this.isEditMode=false; 
    this.displayViewWash=false;
    this.displayViewGeneral=true;    
    this.disabledWashInfo= false;
    this.imageCropperService.nextMessage(true);
  }
  showViewWashInfo(){  
      
    this.displayGeneral = false;
    this.displayWash = false;
    this.generalFlag = false;
    this.washFlag = true;
    this.isEditMode=false; 
    this.displayViewWash=true;
    this.displayViewGeneral=false; 
    this.imageCropperService.nextMessage(true);
  }

  getSchoolGeneralandWashInfoDetails(){   
   
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].schoolId=this.schoolId;
    this.schoolAddViewModel.schoolMaster.schoolId=this.schoolId;     
    this.schoolService.ViewSchool(this.schoolAddViewModel).subscribe(data => {    
      this.generalAndWashInfoData = data;     
      if(this.id === null){
        this.isAddMode=true;
        this.showGeneralEdit();
      } else{
        this.isViewMode=true;
        this.responseImage = this.generalAndWashInfoData.schoolMaster.schoolDetail[0]?.schoolLogo;
        this.showViewGeneralInfo();        
      }      
      this.getDataOfgeneralInfo(this.dataOfgeneralInfo);
      this.getDataOfgeneralInfoFromView(this.dataOfgeneralInfoFromView);
      this.getDataOfWashInfoFromView(this.dataOfgeneralInfoFromView);
      this.imageResponse(this.imageObj);
    })
  }
}
