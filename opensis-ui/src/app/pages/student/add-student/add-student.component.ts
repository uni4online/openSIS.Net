import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import {fadeInRight400ms} from '../../../../@vex/animations/fade-in-right.animation';
import { ImageCropperService } from 'src/app/services/image-cropper.service';
import { SchoolAddViewModel } from '../../../models/schoolMasterModel';
import { ActivatedRoute } from '@angular/router';
import { SchoolService } from '../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedFunction } from '../../../pages/shared/shared-function';

@Component({
  selector: 'vex-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.scss'],
  animations: [  
    fadeInRight400ms    
  ]
})
export class AddStudentComponent implements OnInit {

  pageTitle = 'General Info';
  pageId: string = '';

  displayGeneralInfo = true;
  displayAddressAndContacts = false;
  displayEnrollmentInfo = false;
  displayFamilyInfo = false;

  GeneralInfoFlag: boolean = true;
  AddressAndContactsFlag: boolean = false;
  EnrollmentInfoFlag: boolean = false;
  FamilyInfoFlag: boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
    this.showPage(this.pageId);
  }

  showPage(pageId = 'General Info') {
    this.pageTitle = pageId;
    if (pageId === 'General Info') {
      this.displayGeneralInfo = true;
      this.GeneralInfoFlag = true;
    } else {
      this.displayGeneralInfo = false;
      this.GeneralInfoFlag = false;
    }

    if (pageId === 'Address & Contacts') {
      this.displayAddressAndContacts = true;
      this.AddressAndContactsFlag = true;
    } else {
      this.displayAddressAndContacts = false;
      this.AddressAndContactsFlag = false;
    }

    if (pageId === 'School & Enrollment Info') {
      this.displayEnrollmentInfo = true;
      this.EnrollmentInfoFlag = true;
    } else {
      this.displayEnrollmentInfo = false;
      this.EnrollmentInfoFlag = false;
    }

    if (pageId === 'Family Info') {
      this.displayFamilyInfo = true;
      this.FamilyInfoFlag = true;
    } else {
      this.displayFamilyInfo = false;
      this.FamilyInfoFlag = false;
    }
  }

}
