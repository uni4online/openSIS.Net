import { Component, OnInit,ViewChildren, AfterViewInit, QueryList } from '@angular/core';
import {fadeInRight400ms} from '../../../../@vex/animations/fade-in-right.animation';
import { Subscription } from 'rxjs';
import { ImageCropperService } from '../../../services/image-cropper.service';
@Component({
  selector: 'vex-add-school',
  templateUrl: './add-school.component.html',
  styleUrls: ['./add-school.component.scss'],
  animations: [
  
    fadeInRight400ms

  ]
})
export class AddSchoolComponent implements AfterViewInit {
  clickEventSubscriptionForCrop:Subscription;
  clickEventSubscriptionForUnCrop:Subscription;
  displayGeneral = true;
  displayWash = false;
  generalFlag: boolean = true;
  washFlag: boolean = false;
  enableCropTool:Boolean=false;
  image:String='';
  constructor(private _imageCropperService:ImageCropperService) { 
    
    this.clickEventSubscriptionForCrop=this._imageCropperService.getCroppedEvent().subscribe((res)=>{
    // console.log("Im from Cropped"+res);
      this.image=res;
    });
  
  this.clickEventSubscriptionForUnCrop=this._imageCropperService.getUncroppedEvent().subscribe((res)=>{
    // console.log("Im from Uncropped "+'data:image/png;base64,'+btoa(res.target.result));
    this.image='data:image/png;base64,'+btoa(res.target.result);
      });
  }

  ngAfterViewInit() { }
  
  showGeneral(){
    this.displayGeneral = true;
    this.displayWash = false;
    this.generalFlag=true;
    this.washFlag=false;
  }
  showWash(){
    this.displayWash = true;
    this.displayGeneral = false;
    this.generalFlag=false;
    this.washFlag=true;
   

  }

}
