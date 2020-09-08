import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { schoolDetailsModel } from '../../../../models/schoolDetailsModel';
import { WashInfoEnum } from '../../../../enums/wash-info.enum';


import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';

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
  form:FormGroup
  washinfo= WashInfoEnum;
  runningWater = [];
  
  schoolDetail:schoolDetailsModel
  selectoption =[{id:true,value:"Yes"},{id:false,value:"No"}]
  constructor(
    private translate: TranslateService,
    private router: Router,
    private fb:FormBuilder) {
      this.runningWater=Object.keys(this.washinfo);
      translate.use('en');
      this.form=fb.group({

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
     }

  ngOnInit(): void {
  }

  openGeneralInfo() {
    this.router.navigate(["school/schoolinfo/add-school"]);
  }
  send(){
    
    console.log(this.form.value)
  }

}
