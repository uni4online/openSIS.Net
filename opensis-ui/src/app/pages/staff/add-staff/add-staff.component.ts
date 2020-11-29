import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LayoutService } from 'src/@vex/services/layout.service';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import icGeneralInfo from '@iconify/icons-ic/outline-account-circle';
import icSchoolInfo from '@iconify/icons-ic/outline-corporate-fare';
import icLoginInfo from '@iconify/icons-ic/outline-lock-open';
import icAddressInfo from '@iconify/icons-ic/outline-location-on';
import icCertificationInfo from '@iconify/icons-ic/outline-military-tech';
import icSchedule from '@iconify/icons-ic/outline-schedule';
import { ImageCropperService } from 'src/app/services/image-cropper.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-add-staff',
  templateUrl: './add-staff.component.html',
  styleUrls: ['./add-staff.component.scss'],
  animations: [  
    fadeInRight400ms,
    stagger60ms,
    fadeInUp400ms
  ]
})
export class AddStaffComponent implements OnInit {

  icGeneralInfo = icGeneralInfo;
  icSchoolInfo = icSchoolInfo;
  icLoginInfo = icLoginInfo;
  icAddressInfo = icAddressInfo;
  icCertificationInfo = icCertificationInfo;
  icSchedule = icSchedule;


  pageId = 'General Info';

  constructor(private layoutService: LayoutService, public translateService:TranslateService) {
    translateService.use('en');
    this.layoutService.collapseSidenav();
  }

  ngOnInit(): void {
  }

  // disableSection(){
  //   if(this.pageId == "GeneralInfo"){

  //   }else{

  //   }

  //   if(this.pageId == "SchoolEnrollmentInfo"){
  //   }else{
  //   }
  // }

  showPage(pageId) {    
    localStorage.setItem("pageId",pageId); 
    this.pageId=localStorage.getItem("pageId");    
    //this.disableSection();
  }

}
