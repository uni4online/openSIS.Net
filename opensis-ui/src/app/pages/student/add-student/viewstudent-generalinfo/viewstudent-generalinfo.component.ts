import { Component, OnInit,Input,Output,EventEmitter } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';
import { SharedFunction } from '../../../shared/shared-function';
import { CommonService } from '../../../../services/common.service';
import { CountryModel } from '../../../../models/countryModel';
@Component({
  selector: 'vex-viewstudent-generalinfo',
  templateUrl: './viewstudent-generalinfo.component.html',
  styleUrls: ['./viewstudent-generalinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ],
})
export class ViewstudentGeneralinfoComponent implements OnInit {
  @Input() data;
  @Output() editData: EventEmitter<object> = new EventEmitter();
 
  icEdit = icEdit; 
  countryListArr=[]; 
  countryName="-";
  nationality="-";
  countryModel: CountryModel = new CountryModel();
  constructor(public translateService:TranslateService,
    private commonFunction:SharedFunction,
    private commonService: CommonService) {
    translateService.use('en');
   }

  ngOnInit(): void {
    this.data.dob= this.commonFunction.formatDateInEditMode(this.data.dob);
    this.data.dob= this.commonFunction.formatDate(this.data.dob);
    
    this.getAllCountry();
  }
  editGeneralInfo(){
    this.editData.emit(this.data);
    
  }
  getAllCountry(){
    
    this.commonService.GetAllCountry(this.countryModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.countryListArr=[];
      }
      else {
        if (data._failure) {
          this.countryListArr=[];
        } else {        
          this.countryListArr=data.tableCountry; 
          this.countryListArr.map((val) => {
          var countryInNumber = +this.data.countryOfBirth;  
          var nationality=+this.data.nationality; 
            if(val.id === countryInNumber){
              this.countryName= val.name;
            }
            if(val.id === nationality){
              this.nationality= val.name;
            }
          })  
         
        }
      }
  
    })  
  }
}
