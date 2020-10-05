import { Component, OnInit,EventEmitter,Output,Input } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WashInfoEnum } from '../../../../enums/wash-info.enum';
import { SchoolAddViewModel } from '../../../../models/schoolMasterModel';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import { SchoolService } from 'src/app/services/school.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoaderService } from 'src/app/services/loader.service';
import { SharedFunction } from '../../../shared/shared-function';
import { ImageCropperService } from '../../../../services/image-cropper.service';
@Component({
  selector: 'vex-wash-info',
  templateUrl: './wash-info.component.html',
  styleUrls: ['./wash-info.component.scss'],
  animations: [
    
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ] 
})
export class WashInfoComponent implements OnInit {

  @Output("parentShowWash") parentShowWash :EventEmitter<object> = new EventEmitter<object>();  
  @Input() dataOfgeneralInfo;
  @Input() dataOfWashInfoFromView;
  
  
  form:FormGroup
  washinfo= WashInfoEnum;  
  //public tenant = "opensisv2";  
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();  
  loading;
  
  
   
  constructor( private fb: FormBuilder,
    private generalInfoService:SchoolService,
     private snackbar: MatSnackBar,
     private router: Router,    
     public translateService:TranslateService,
     private loaderService:LoaderService,
     private commonFunction: SharedFunction,
     private _ImageCropperService:ImageCropperService) {
       
      translateService.use('en');      
      this.loaderService.isLoading.subscribe((val) => {
        this.loading = val;
      });
      
    }
    ngOnInit(): void {
      this.form = this.fb.group(
        {

        runningWater:[''],
        mainSourceofDrinkingWater:['',Validators.maxLength(100)],
        currentlyAvailable:[""],
        handwashingAvailable:[""],
        soapWaterAvailable:[""],
        hygeneEducation:["",Validators.maxLength(50)],

        /* Female Toilet Information */
        femaleToiletType:["",Validators.maxLength(50)],
        totalFemaleToilets:[""],
        totalFemaleToiletsUsable:[""],
        femaleToiletAccessibility:["",Validators.maxLength(50)],

        /* maleToiletInformation */
        maleToiletType:["",Validators.maxLength(50)],
        totalMaleToilets:[""], 
        totalMaleToiletsUsable:[""],
        maleToiletAccessibility:["",Validators.maxLength(50)],

        /* Common Toilet Information */
        commonToiletType:["",Validators.maxLength(50)],
        totalCommonToilets:[""],
        totalCommonToiletsUsable:[""],
        commonToiletAccessibility:["",Validators.maxLength(50)]
      })
     
      if(this.commonFunction.checkEmptyObject(this.dataOfWashInfoFromView) === true){
        this._ImageCropperService.nextMessage(false);
        this.schoolAddViewModel=this.dataOfWashInfoFromView;        
      }else{        
        this.schoolAddViewModel=this.dataOfgeneralInfo;
      }

    this.schoolAddViewModel.schoolMaster.schoolDetail[0].comonToiletType = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].comonToiletType);   
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].commonToiletAccessibility = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].commonToiletAccessibility);
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].maleToiletAccessibility = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].maleToiletAccessibility);  
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].maleToiletType = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].maleToiletType) ;
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].femaleToiletAccessibility = this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].femaleToiletAccessibility) ;     
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].femaleToiletType=this.commonFunction.trimData(this.schoolAddViewModel.schoolMaster.schoolDetail[0].femaleToiletType) ;
    

      
     }


     
     submit() {    
        this.schoolAddViewModel._tenantName = sessionStorage.getItem("tenant"); 
        this.schoolAddViewModel._token = sessionStorage.getItem("token");               

        this.generalInfoService.UpdateGeneralInfo(this.schoolAddViewModel).subscribe(data => {
          if(typeof(data)=='undefined')
          {
            this.snackbar.open('Wash Info Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else
          {
            if (data._failure) {
              this.snackbar.open('Wash Info Submission failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {                
                this.snackbar.open('Wash Info Submission Successful.', '', {
                duration: 10000
              });             
            
              this.router.navigateByUrl("school/schoolinfo"); 
            }
          }
            
        })    
    }   
  
  }
