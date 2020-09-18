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
  @Input() schoolId: Number;
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
    this.schoolAddViewModel.tblSchoolDetail.tenant_Id="396862D6-92EA-406A-950F-F8BFF825988F";
    this.schoolAddViewModel.tblSchoolDetail.school_Id=this.schoolId;
    this.schoolAddViewModel.tblSchoolDetail.schoolMaster.school_Id=this.schoolId;
    this.schoolAddViewModel.tblSchoolDetail.schoolMaster.tenant_Id="396862D6-92EA-406A-950F-F8BFF825988F";
    this.generalInfoService.GetGeneralInfoById(this.schoolAddViewModel).subscribe(data => {
      if(data._failure){
        this.snackbar.open('School information failed. '+ data._message, 'LOL THANKS', {
        duration: 10000
        });      
      }else{              
        this.schoolAddViewModel=data;
        
        if(data.tblSchoolDetail.running_Water){
          this.running_Water="Yes"
        }else if(data.tblSchoolDetail.running_Water===false){
          this.running_Water="No"
        }else{
          this.running_Water="-";
        }

        if(data.tblSchoolDetail.currently_Available){
          this.currently_Available="Yes"
        }else if(data.tblSchoolDetail.currently_Available===false){
          this.currently_Available="No";
        }else{
          this.currently_Available="-";
        }

        if(data.tblSchoolDetail.handwashing_Available){
          this.handwashing_Available="Yes"
        }else if(data.tblSchoolDetail.handwashing_Available===false){
          this.handwashing_Available="No"
        }else{
          this.handwashing_Available="-"
        }

        if(data.tblSchoolDetail.soap_and_Water_Available){
          this.soap_and_Water_Available="Yes"
        }else if(data.tblSchoolDetail.soap_and_Water_Available===false){
          this.soap_and_Water_Available="No"
        }else{
          this.soap_and_Water_Available="-";
        }

        this.hygene_Education=data.tblSchoolDetail.hygene_Education;
        this.female_Toilet_Type =data.tblSchoolDetail.female_Toilet_Type;
        this.total_Female_Toilets=data.tblSchoolDetail.total_Female_Toilets;
        this.total_Female_Toilets_Usable=data.tblSchoolDetail.total_Female_Toilets_Usable;

        if(data.tblSchoolDetail.female_Toilet_Accessibility="true"){
          this.female_Toilet_Accessibility="Yes"
        }else if(data.tblSchoolDetail.female_Toilet_Accessibility="false"){
          this.female_Toilet_Accessibility="No"
        }else{
          this.female_Toilet_Accessibility="-"
        }

        this.male_Toilet_Type=data.tblSchoolDetail.male_Toilet_Type;
        this.total_Male_Toilets=data.tblSchoolDetail.total_Male_Toilets;
        this.total_Male_Toilets_Usable=data.tblSchoolDetail.total_Male_Toilets_Usable;

        if(data.tblSchoolDetail.male_Toilet_Accessibility=="true"){
          this.male_Toilet_Accessibility="yes";
        }else if(data.tblSchoolDetail.male_Toilet_Accessibility=="false"){
          this.male_Toilet_Accessibility="No"
        }
        else{
          this.male_Toilet_Accessibility="-"
        }

        this.comon_Toilet_Type=data.tblSchoolDetail.comon_Toilet_Type;
        this.total_Common_Toilets=data.tblSchoolDetail.total_Common_Toilets;
        this.total_Common_Toilets_Usable=data.tblSchoolDetail.total_Common_Toilets_Usable;

        if(data.tblSchoolDetail.common_Toilet_Accessibility=="true"){
          this.common_Toilet_Accessibility="Yes";
        }else if(data.tblSchoolDetail.common_Toilet_Accessibility=="false"){
          this.common_Toilet_Accessibility="No"
        }else{
          this.common_Toilet_Accessibility="-"
        }

        this.main_Source_of_Drinking_Water=data.tblSchoolDetail.main_Source_of_Drinking_Water
      }    
    })

  }
 
  editWashInfo(){
    this.parentShowWash.emit(); 
    this.dataOfWashInfoFromView.emit(this.schoolAddViewModel);
  }
  
}
