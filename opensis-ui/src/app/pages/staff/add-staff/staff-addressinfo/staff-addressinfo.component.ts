import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { CountryModel } from '../../../../models/countryModel';
import { StaffAddModel } from '../../../../models/staffModel';
import { LanguageModel } from '../../../../models/languageModel';
import icCheckBox from '@iconify/icons-ic/check-box';
import icCheckBoxOutlineBlank from '@iconify/icons-ic/check-box-outline-blank';
import icEdit from '@iconify/icons-ic/edit';
import { TranslateService } from '@ngx-translate/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { StaffService } from '../../../../services/staff.service';
import { CommonService } from '../../../../services/common.service';
import { LoginService } from '../../../../services/login.service';
import { SharedFunction } from '../../../../pages/shared/shared-function';
import { StaffRelation } from '../../../../enums/staff-relation.enum';



@Component({
  selector: 'vex-staff-addressinfo',
  templateUrl: './staff-addressinfo.component.html',
  styleUrls: ['./staff-addressinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StaffAddressinfoComponent implements OnInit {
  staffCreate = SchoolCreate;
  @Input() staffDetailsForViewAndEdit;
  @Input() categoryId;
  @ViewChild('f') currentForm: NgForm;
  @Input() staffCreateMode: SchoolCreate;
  countryModel: CountryModel = new CountryModel();
  staffAddModel: StaffAddModel = new StaffAddModel();
  languages: LanguageModel = new LanguageModel();
  mailingAddressSameToHome: boolean = false;
  staffRelationEnum = Object.keys(StaffRelation);
  countryListArr = [];
  module = 'Staff';
  countryName = "-";
  mailingAddressCountry = "-";
  data;
  languageList;
  checkBoxChecked = false;
  icEdit = icEdit;
  icCheckBox = icCheckBox;
  icCheckBoxOutlineBlank = icCheckBoxOutlineBlank;


  constructor(public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private staffService: StaffService,
    private commonService: CommonService,
    private loginService: LoginService,
    private commonFunction: SharedFunction) {
    translateService.use('en');
  }

  ngOnInit(): void {

    if (this.staffCreateMode == this.staffCreate.VIEW) {
      this.data = this.staffDetailsForViewAndEdit?.staffMaster;
      this.staffAddModel = this.staffDetailsForViewAndEdit;
      if (this.data.mailingAddressSameToHome) {
        this.mailingAddressSameToHome = true;
      } else {
        this.mailingAddressSameToHome = false;
      }
      this.getAllCountry();
    } else {
      this.staffAddModel = this.staffService.getStaffDetails();
    }

    this.getAllCountry();
  }

  copyHomeAddress(check) {

    if (this.staffAddModel.staffMaster.mailingAddressSameToHome === false || this.staffAddModel.staffMaster.mailingAddressSameToHome === undefined) {

      if (this.staffAddModel.staffMaster.homeAddressLineOne !== undefined && this.staffAddModel.staffMaster.homeAddressCity !== undefined &&
        this.staffAddModel.staffMaster.homeAddressState !== undefined && this.staffAddModel.staffMaster.homeAddressZip !== undefined) {

        this.staffAddModel.staffMaster.mailingAddressLineOne = this.staffAddModel.staffMaster.homeAddressLineOne;
        this.staffAddModel.staffMaster.mailingAddressLineTwo = this.staffAddModel.staffMaster.homeAddressLineTwo;
        this.staffAddModel.staffMaster.mailingAddressCity = this.staffAddModel.staffMaster.homeAddressCity;
        this.staffAddModel.staffMaster.mailingAddressState = this.staffAddModel.staffMaster.homeAddressState;
        this.staffAddModel.staffMaster.mailingAddressZip = this.staffAddModel.staffMaster.homeAddressZip;
        this.staffAddModel.staffMaster.mailingAddressCountry = this.staffAddModel.staffMaster.homeAddressCountry;

      } else {

        this.checkBoxChecked = check ? true : false;
        this.snackbar.open('Please Provide All Mandatory Fields First', '', {
          duration: 10000
        });
      }

    } else {
      this.staffAddModel.staffMaster.mailingAddressLineOne = "";
      this.staffAddModel.staffMaster.mailingAddressLineTwo = "";
      this.staffAddModel.staffMaster.mailingAddressCity = "";
      this.staffAddModel.staffMaster.mailingAddressState = "";
      this.staffAddModel.staffMaster.mailingAddressZip = "";
      this.staffAddModel.staffMaster.mailingAddressCountry = null;
    }
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
          if (this.staffCreateMode == this.staffCreate.VIEW) {
            this.countryListArr.map((val) => {
              var countryInNumber = +this.data.homeAddressCountry;
              var mailingAddressCountry = +this.data.mailingAddressCountry;
              if (val.id === countryInNumber) {
                this.countryName = val.name;
              }
              if (val.id === mailingAddressCountry) {
                this.mailingAddressCountry = val.name;
              }
            })
          }
        }
      }
    })
  }

  submitAddress() {
    this.staffAddModel.selectedCategoryId = this.staffAddModel.fieldsCategoryList[this.categoryId].categoryId;
    this.staffAddModel._token = sessionStorage.getItem("token");
    this.staffAddModel._tenantName = sessionStorage.getItem("tenant");
    this.staffService.updateStaff(this.staffAddModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Staff Updation failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('Staff Updation failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.snackbar.open('Staff Update Successful.', '', {
            duration: 10000
          });
          this.staffCreateMode = this.staffCreate.VIEW;
        }
      }

    })
  }


  editAddressContactInfo() {
    this.staffCreateMode = this.staffCreate.EDIT
  }

  cancelEdit() {
    this.staffCreateMode = this.staffCreate.VIEW
  }

}
