import { Component, OnInit,Input,Output,EventEmitter } from '@angular/core';
import { fadeInUp400ms } from 'src/@vex/animations/fade-in-up.animation';
import {fadeInRight400ms} from 'src/@vex/animations/fade-in-right.animation';
import { stagger60ms } from 'src/@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';
import { SchoolService } from 'src/app/services/school.service';
import { SchoolAddViewModel } from 'src/app/models/schoolDetailsModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router ,ActivatedRoute} from '@angular/router';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'vex-view-wash-info',
  templateUrl: './view-wash-info.component.html',
  styleUrls: ['./view-wash-info.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class ViewWashInfoComponent implements OnInit {
  @Input() schoolId: number;
  @Output() parentShowWash :EventEmitter<any> = new EventEmitter<any>();
  @Output("dataOfWashInfoFromView") dataOfWashInfoFromView: EventEmitter<any> =   new EventEmitter();
  schoolAddViewModel:SchoolAddViewModel= new SchoolAddViewModel();
  icEdit = icEdit;
  hygene_Education: string;
  running_Water: string;
  currently_Available: string;
  handwashing_Available: string;
  soap_and_Water_Available: string;
  female_Toilet_Type: string;
  total_Female_Toilets: Number;
  total_Female_Toilets_Usable: Number;
  female_Toilet_Accessibility: string;
  male_Toilet_Type: string;
  total_Male_Toilets: Number;
  total_Male_Toilets_Usable: Number;
  male_Toilet_Accessibility: string;
  comon_Toilet_Type: string;
  total_Common_Toilets: Number;
  total_Common_Toilets_Usable: Number;
  common_Toilet_Accessibility: any;
  main_Source_of_Drinking_Water: string;
  public tenant = "";
  constructor(
    private generalInfoService:SchoolService,
    private snackbar: MatSnackBar,
    private router:Router,
    private Activeroute: ActivatedRoute,
    private translateService : TranslateService

    ) {
      this.Activeroute.params.subscribe(params => { this.tenant ='OpensisV2'; });
      translateService.use('en');
   }
   ngOnInit(): void {
    this.getSchoolGeneralInfoDetails();
  }
  getSchoolGeneralInfoDetails()
  {    
    this.schoolAddViewModel._tenantName=this.tenant; 
    this.schoolAddViewModel._token=sessionStorage.getItem("token");
    this.schoolAddViewModel.tblSchoolDetail.tenantId=sessionStorage.getItem("tenantId");
    this.schoolAddViewModel.tblSchoolDetail.schoolId=this.schoolId;
    this.schoolAddViewModel.tblSchoolDetail.tableSchoolMaster.schoolId=this.schoolId;
    this.schoolAddViewModel.tblSchoolDetail.tableSchoolMaster.tenantId=sessionStorage.getItem("tenantId");
    this.generalInfoService.GetGeneralInfoById(this.schoolAddViewModel).subscribe(data => {
      if(data._failure){
        this.snackbar.open('School information failed. '+ data._message, 'LOL THANKS', {
        duration: 10000
        });      
      }else{  
        debugger            
        this.schoolAddViewModel=data;
        
        if(data.tblSchoolDetail.runningWater){
          this.running_Water="Yes"
        }else if(data.tblSchoolDetail.runningWater===false){
          this.running_Water="No"
        }else{
          this.running_Water="-";
        }

        if(data.tblSchoolDetail.currentlyAvailable){
          this.currently_Available="Yes"
        }else if(data.tblSchoolDetail.currentlyAvailable===false){
          this.currently_Available="No";
        }else{
          this.currently_Available="-";
        }

        if(data.tblSchoolDetail.handwashingAvailable){
          this.handwashing_Available="Yes"
        }else if(data.tblSchoolDetail.handwashingAvailable===false){
          this.handwashing_Available="No"
        }else{
          this.handwashing_Available="-"
        }

        if(data.tblSchoolDetail.soapAndWaterAvailable){
          this.soap_and_Water_Available="Yes"
        }else if(data.tblSchoolDetail.soapAndWaterAvailable===false){
          this.soap_and_Water_Available="No"
        }else{
          this.soap_and_Water_Available="-";
        }

        this.hygene_Education=data.tblSchoolDetail.hygeneEducation;
        this.female_Toilet_Type =data.tblSchoolDetail.femaleToiletType;
        this.total_Female_Toilets=data.tblSchoolDetail.totalFemaleToilets;
        this.total_Female_Toilets_Usable=data.tblSchoolDetail.totalFemaleToiletsUsable;

        if(data.tblSchoolDetail.femaleToiletAccessibility="true"){
          this.female_Toilet_Accessibility="Yes"
        }else if(data.tblSchoolDetail.femaleToiletAccessibility="false"){
          this.female_Toilet_Accessibility="No"
        }else{
          this.female_Toilet_Accessibility="-"
        }

        this.male_Toilet_Type=data.tblSchoolDetail.maleToiletType;
        this.total_Male_Toilets=data.tblSchoolDetail.totalMaleToilets;
        this.total_Male_Toilets_Usable=data.tblSchoolDetail.totalMaleToiletsUsable;

        if(data.tblSchoolDetail.maleToiletAccessibility=="true"){
          this.male_Toilet_Accessibility="yes";
        }else if(data.tblSchoolDetail.maleToiletAccessibility=="false"){
          this.male_Toilet_Accessibility="No"
        }
        else{
          this.male_Toilet_Accessibility="-"
        }

        this.comon_Toilet_Type=data.tblSchoolDetail.comonToiletType;
        this.total_Common_Toilets=data.tblSchoolDetail.totalCommonToilets;
        this.total_Common_Toilets_Usable=data.tblSchoolDetail.totalCommonToiletsUsable;

        if(data.tblSchoolDetail.commonToiletAccessibility=="true"){
          this.common_Toilet_Accessibility="Yes";
        }else if(data.tblSchoolDetail.commonToiletAccessibility=="false"){
          this.common_Toilet_Accessibility="No"
        }else{
          this.common_Toilet_Accessibility="-"
        }

        this.main_Source_of_Drinking_Water=data.tblSchoolDetail.mainSourceOfDrinkingWater
      }    
    })

  }
 
  editWashInfo(){
    this.parentShowWash.emit(); 
    this.dataOfWashInfoFromView.emit(this.schoolAddViewModel);
  }
  
}
