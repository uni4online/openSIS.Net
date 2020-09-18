import { Component, OnInit,Input,Output,EventEmitter } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';
import {SchoolAddViewModel } from '../../../../models/schoolDetailsModel';
import {ActivatedRoute, Router} from '@angular/router';
import { SchoolService } from '../../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import {fadeInRight400ms} from 'src/@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
@Component({
  selector: 'vex-view-general-info',
  templateUrl: './view-general-info.component.html',
  styleUrls: ['./view-general-info.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class ViewGeneralInfoComponent implements OnInit {
  @Input() schoolId: Number;
  @Output() imageResponse: EventEmitter<any> = new EventEmitter();
  @Output() parentShowWash :EventEmitter<any> = new EventEmitter<any>();
  @Output("dataOfgeneralInfoFromView") dataOfgeneralInfoFromView: EventEmitter<any> =   new EventEmitter();
  icEdit = icEdit;
  public tenant = "";
  public internet="";
  public electricity="";
  public status="";
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  constructor(private Activeroute: ActivatedRoute,private generalInfoService:SchoolService,
     private snackbar: MatSnackBar,private translateService:TranslateService
    ) { 
    this.Activeroute.params.subscribe(params => { this.tenant ='OpensisV2'; });
    translateService.use('en');
  } 

  ngOnInit(): void {
    this.getSchoolGeneralInfoDetails();
  }
  editGeneralInfo(){
    this.parentShowWash.emit(this.schoolAddViewModel);    
    this.dataOfgeneralInfoFromView.emit(this.schoolAddViewModel);
  }
  formatDate(date:any){
    if(date !== "" && date !== null){
      var formattedDate = date.split('T');
      return formattedDate[0];
    }else{
      return "-";
    }
    
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
        this.imageResponse.emit(this.schoolAddViewModel);
        this.internet= this.schoolAddViewModel.tblSchoolDetail.internet?'Yes':'No';
        this.electricity= this.schoolAddViewModel.tblSchoolDetail.electricity?'Yes':'No';
        this.status= this.schoolAddViewModel.tblSchoolDetail.status?'Yes':'No';
        this.schoolAddViewModel.tblSchoolDetail.date_School_Opened= this.formatDate(this.schoolAddViewModel.tblSchoolDetail.date_School_Opened);
        this.schoolAddViewModel.tblSchoolDetail.date_School_Closed= this.formatDate(this.schoolAddViewModel.tblSchoolDetail.date_School_Closed);
        
        
      }
    })

  }

}
