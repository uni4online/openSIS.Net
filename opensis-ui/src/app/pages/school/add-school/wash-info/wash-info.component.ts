import { Component, OnInit,EventEmitter,Output,Input } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WashInfoEnum } from '../../../../enums/wash-info.enum';
import { SchoolAddViewModel } from '../../../../models/schoolDetailsModel';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import { SchoolService } from 'src/app/services/school.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoaderService } from 'src/app/services/loader.service';
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

  @Output("parentShowWash") parentShowWash :EventEmitter<any> = new EventEmitter<any>();  
  @Input() dataOfgeneralInfo: any;
  @Input() dataOfWashInfoFromView: any;
  
  
  form:FormGroup
  washinfo= WashInfoEnum;
  selectoption = [];
  public tenant = "";  
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();  
  loading;
  
  
   
  constructor( private fb: FormBuilder,
    private generalInfoService:SchoolService,
     private snackbar: MatSnackBar,
     private router: Router,
     private Activeroute: ActivatedRoute,
     public translateService:TranslateService,
     private loaderService:LoaderService) {
       
      translateService.use('en');
      this.Activeroute.params.subscribe(params => { this.tenant = 'OpensisV2'; });
      this.loaderService.isLoading.subscribe((val) => {
        this.loading = val;
      });
      
    }
    ngOnInit(): void {
      this.form = this.fb.group(
        {

        runningWater:[''],
        mainSourceofDrinkingWater:[''],
        currentlyAvailable:[""],
        handwashingAvailable:[""],
        soapWaterAvailable:[""],
        hygeneEducation:[""],

        /* Female Toilet Information */
        femaleToiletType:[""],
        totalFemaleToilets:[""],
        totalFemaleToiletsUsable:[""],
        femaleToiletAccessibility:[""],

        /* maleToiletInformation */
        maleToiletType:[""],
        totalMaleToilets:[""], 
        totalMaleToiletsUsable:[""],
        maleToiletAccessibility:[""],

        /* Common Toilet Information */
        commonToiletType:[""],
        totalCommonToilets:[""],
        totalCommonToiletsUsable:[""],
        commonToiletAccessibility:[""]
      })
     
      if(Object.keys(this.dataOfWashInfoFromView).length !== 0){
        this.schoolAddViewModel=this.dataOfWashInfoFromView;        
      }else{        
        this.schoolAddViewModel=this.dataOfgeneralInfo;
      }
    
     }
 
     submit() {    
        this.schoolAddViewModel._tenantName = this.tenant; 
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
             // sessionStorage.removeItem("id");
              this.router.navigateByUrl("school/schoolinfo"); 
            }
          }
            
        })    
    }   
  
  }
