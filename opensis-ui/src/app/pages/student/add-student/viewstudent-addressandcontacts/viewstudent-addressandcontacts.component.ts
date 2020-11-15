import { Component, OnInit,Input,Output,EventEmitter } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icCheckBox from '@iconify/icons-ic/check-box';
import icCheckBoxOutlineBlank from '@iconify/icons-ic/check-box-outline-blank';
import icEdit from '@iconify/icons-ic/edit';
import { SharedFunction } from '../../../shared/shared-function';
import { CommonService } from '../../../../services/common.service';
import { CountryModel } from '../../../../models/countryModel';
@Component({
  selector: 'vex-viewstudent-addressandcontacts',
  templateUrl: './viewstudent-addressandcontacts.component.html',
  styleUrls: ['./viewstudent-addressandcontacts.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class ViewstudentAddressandcontactsComponent implements OnInit {
  @Input() data;
  @Output() editData: EventEmitter<object> = new EventEmitter();
  
  icEdit = icEdit;
  icCheckBox = icCheckBox;
  icCheckBoxOutlineBlank = icCheckBoxOutlineBlank;
  mailingAddressSameToHome:boolean=false;
  countryListArr=[]; 
  countryName="-";
  mailingAddressCountry="-";
  countryModel: CountryModel = new CountryModel();
  constructor(public translateService:TranslateService,
    private commonFunction:SharedFunction,
    private commonService: CommonService) {
    translateService.use('en');
   }

  ngOnInit(): void {
    if(this.data.mailingAddressSameToHome){
      this.mailingAddressSameToHome = true;
    }else{
      this.mailingAddressSameToHome = false;
    }
    this.getAllCountry();    
  }

  editAddressContactInfo(){
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
          var countryInNumber = +this.data.homeAddressCountry;  
          var mailingAddressCountry=+this.data.mailingAddressCountry; 
            if(val.id === countryInNumber){
              this.countryName= val.name;
            }
            if(val.id === mailingAddressCountry){
              this.mailingAddressCountry= val.name;
            }
          })  
         
        }
      }
  
    }) 
  
   }
}
