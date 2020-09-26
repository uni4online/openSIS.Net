import { Component, OnInit,Input,Output,EventEmitter } from '@angular/core';
import { fadeInUp400ms } from 'src/@vex/animations/fade-in-up.animation';
import {fadeInRight400ms} from 'src/@vex/animations/fade-in-right.animation';
import { stagger60ms } from 'src/@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';
import { SchoolService } from 'src/app/services/school.service';
import { SchoolAddViewModel } from 'src/app/models/schoolMasterModel';
import { MatSnackBar } from '@angular/material/snack-bar';
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
 
  @Output() parentShowWash :EventEmitter<object> = new EventEmitter<object>();
  @Output("dataOfWashInfoFromView") dataOfWashInfoFromView: EventEmitter<object> =   new EventEmitter();
  schoolAddViewModel:SchoolAddViewModel= new SchoolAddViewModel();
  icEdit = icEdit;
  @Input() generalAndWashInfoData:SchoolAddViewModel; //This is coming from AddSchool on API Call.
  public tenant = "";
  constructor(
    private generalInfoService:SchoolService,
    private snackbar: MatSnackBar,   
    private translateService : TranslateService

    ) {
      
      translateService.use('en');
   }
   ngOnInit(): void {
    this.getSchoolGeneralInfoDetails();
  }
  getSchoolGeneralInfoDetails()
  {    
      if(this.generalAndWashInfoData._failure){
        this.snackbar.open('School information failed. '+ this.generalAndWashInfoData._message, 'LOL THANKS', {
        duration: 10000
        });      
      }else{  
              
        this.schoolAddViewModel=this.generalAndWashInfoData;
        
      }
  }
 
  editWashInfo(){
    this.parentShowWash.emit(); 
    this.dataOfWashInfoFromView.emit(this.schoolAddViewModel);
  }
  
}
