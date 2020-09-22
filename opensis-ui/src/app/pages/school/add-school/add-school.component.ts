import { Component, OnInit,ViewChildren, AfterViewInit, QueryList } from '@angular/core';
import { Subscription } from 'rxjs';
import {fadeInRight400ms} from '../../../../@vex/animations/fade-in-right.animation';
import { ImageCropperService } from 'src/app/services/image-cropper.service';
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
  enableCropTool:boolean=false;
  responseImage:string;
  image:string='';
  displayGeneral = true;
  displayWash = false;
  generalFlag: boolean = true;
  washFlag: boolean = false;
  isEditMode:boolean=false;
  dataOfgeneralInfo:any={};
  id:any=0;
  schoolId:number=0;  
  displayViewWash:boolean=false;
  displayViewGeneral:boolean=false;
  isViewMode:boolean=false;  
  dataOfgeneralInfoFromView:any={};
  dataOfWashInfoFromView:any={};
  disabledWashInfo:boolean=false;  
  dataOfgeneralInfoFromWash:any={};
  dataOfwashInfoFromGeneral:any={};
  isAddMode:boolean=false;
  getImageResponse="";
  imageObj:any={};
  constructor(private imageCropperService:ImageCropperService) { 
    this.clickEventSubscriptionForCrop=this.imageCropperService.getCroppedEvent().subscribe((res)=>{
     
        this.image=res;
      });
    
    this.clickEventSubscriptionForUnCrop=this.imageCropperService.getUncroppedEvent().subscribe((res)=>{
     
      //this.image='data:image/png;base64,'+btoa(res.target.result);
      this.image=btoa(res.target.result);
     
        });
  }

  ngOnInit() { 
    
    this.id = sessionStorage.getItem("id")
    this.schoolId=Number(this.id);   
    sessionStorage.removeItem("id") 
  
    if(this.id === null){
      this.isAddMode=true;
      this.showGeneralEdit();
    } else{
      this.isViewMode=true;
      this.showViewGeneralInfo();
    }
    
    this.getDataOfgeneralInfo(this.dataOfgeneralInfo);
    this.getDataOfgeneralInfoFromView(this.dataOfgeneralInfoFromView);
    this.getDataOfWashInfoFromView(this.dataOfgeneralInfoFromView);
    this.imageResponse(this.imageObj);
    
   
   }

  getDataOfgeneralInfo(data:any){   
    this.dataOfgeneralInfo=data;    
  }

  getDataOfgeneralInfoFromView(data:any){  
    if(Object.keys(data).length>0){
      this.dataOfgeneralInfoFromView=data; 
      this.responseImage = this.dataOfgeneralInfoFromView.tblSchoolDetail.schoolLogo;
    }
    
   
  }

  getDataOfWashInfoFromView(data:any){    
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

  showWashEdit(data:any){ 
    
    if(data){     
      this.isViewMode=false;
    }else{       
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
  imageResponse(data:any){
    if(Object.keys(data).length>0){
    this.responseImage = data.tblSchoolDetail.schoolLogo;
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
  }
  showViewWashInfo(){   
    this.displayGeneral = false;
    this.displayWash = false;
    this.generalFlag = false;
    this.washFlag = true;
    this.isEditMode=false; 
    this.displayViewWash=true;
    this.displayViewGeneral=false; 

  }
}
