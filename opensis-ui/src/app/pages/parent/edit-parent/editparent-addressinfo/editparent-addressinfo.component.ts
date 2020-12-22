import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/edit';
import icClear from '@iconify/icons-ic/baseline-clear';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { AddParentInfoModel } from '../../../../models/parentInfoModel';
import { CountryModel } from '../../../../models/countryModel';
import { CommonService } from '../../../../services/common.service';
import { ParentInfoService } from '../../../../services/parent-info.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'vex-editparent-addressinfo',
  templateUrl: './editparent-addressinfo.component.html',
  styleUrls: ['./editparent-addressinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class EditparentAddressinfoComponent implements OnInit {

  icAdd = icAdd;
  icClear = icClear;
  icEdit = icEdit;
  @Input() parentDetailsForViewAndEdit;
  @ViewChild('f') currentForm: NgForm;
  f: NgForm;
  parentCreate = SchoolCreate;
  @Input() parentCreateMode: SchoolCreate;
  addParentInfoModel: AddParentInfoModel = new AddParentInfoModel();
  countryModel: CountryModel = new CountryModel();
  countryListArr = [];
  countryName = "-";
  country = '-';
  data;

  constructor(private fb: FormBuilder,
    private snackbar: MatSnackBar,
    public translateService: TranslateService,
    private commonService: CommonService,
    private _parentInfoService: ParentInfoService) {
    translateService.use('en');
  }

  ngOnInit(): void {
    this.parentCreateMode = this.parentCreate.VIEW;
    this.addParentInfoModel = this.parentDetailsForViewAndEdit;    
    this.getAllCountry();
  }


  viewParentInfo() {

    this.addParentInfoModel.parentInfo.parentId = 1;
    this._parentInfoService.viewParentInfo(this.addParentInfoModel).subscribe(data => {
      this.addParentInfoModel.parentInfo = data.parentInfo;
      
    });

  }

  editAddressContactInfo() {
    this.parentCreateMode = this.parentCreate.EDIT;

    this.addParentInfoModel.parentInfo.country = + this.addParentInfoModel.parentInfo.country;
  }

  getAllCountry() {
    this.commonService.GetAllCountry(this.countryModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.countryListArr = [];
      }
      else {
        if (data._failure) {
          this.countryListArr = [];
        } else {

          this.countryListArr = data.tableCountry;
          if (this.parentCreateMode == this.parentCreate.VIEW) {
            this.viewCountryName();
          }

          this.countryListArr.map((val) => {
            var country = + this.addParentInfoModel.parentInfo.country;
            if (val.id === country) {

              this.country = val.name;
            }

          })


        }
      }
    })
  }


  viewCountryName() {
    this.countryListArr.map((val) => {
      var countryInNumber = + this.addParentInfoModel.parentInfo.country;
      if (val.id === countryInNumber) {
        this.countryName = val.name;

      }

    })
  }

  submit() {
    this._parentInfoService.updateParentInfo(this.addParentInfoModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Address Updation failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('Address Updation failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.snackbar.open('Address Update Successful.', '', {
            duration: 10000
          });
          this.viewCountryName();
          this.parentCreateMode = this.parentCreate.VIEW;
        }
      }

    });
  }
}
