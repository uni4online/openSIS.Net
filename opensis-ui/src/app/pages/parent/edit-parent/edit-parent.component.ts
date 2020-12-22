import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LayoutService } from 'src/@vex/services/layout.service';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import icGeneralInfo from '@iconify/icons-ic/outline-account-circle';
import icAddress from '@iconify/icons-ic/outline-location-on';
import icAccessInfo from '@iconify/icons-ic/outline-lock-open';
import { ImageCropperService } from 'src/app/services/image-cropper.service';
import { SchoolCreate } from 'src/app/enums/school-create.enum';
import { ParentInfoService } from 'src/app/services/parent-info.service';
import { AddParentInfoModel } from 'src/app/models/parentInfoModel';

@Component({
  selector: 'vex-edit-parent',
  templateUrl: './edit-parent.component.html',
  styleUrls: ['./edit-parent.component.scss'],
  animations: [  
    fadeInRight400ms,
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditParentComponent implements OnInit {

  icGeneralInfo = icGeneralInfo;
  icAddress = icAddress;
  icAccessInfo = icAccessInfo;
  pageId = 'General Info';
  parentCreate = SchoolCreate;
  parentId: number;
  parentCreateMode: SchoolCreate = SchoolCreate.VIEW;
  addParentInfoModel: AddParentInfoModel = new AddParentInfoModel();
  parentTitle;
  constructor(private layoutService: LayoutService,
    private _parentInfoService:ParentInfoService,) {
    this.layoutService.collapseSidenav();
  }

  ngOnInit(): void {

    this.parentCreateMode = this.parentCreate.VIEW;
    this.parentId = this._parentInfoService.getParentId();
   
    this.getParentDetailsUsingId();

  }

  getParentDetailsUsingId(){
    this.addParentInfoModel.parentInfo.parentId = this.parentId;
    this._parentInfoService.viewParentInfo(this.addParentInfoModel).subscribe(data => {
      this.addParentInfoModel = data;
      this._parentInfoService.sendDetails(this.addParentInfoModel);
      this.parentTitle = this.addParentInfoModel.parentInfo.salutation + " " +this.addParentInfoModel.parentInfo.firstname + " " + this.addParentInfoModel.parentInfo.middlename+ " " + this.addParentInfoModel.parentInfo.lastname;
     // this._parentInfoService.sendDetails(this.addParentInfoModel);
      //console.log("parentId",this.addParentInfoModel);
      //this.fieldsCategory = data.fieldsCategoryList;
      //
      //this.responseImage = this.addParentInfoModel.parentInfo.studentPhoto;
      //this.studentTitle = this.addParentInfoModel.parentInfo.firstGivenName + " " + this.addParentInfoModel.parentInfo.lastFamilyName;
      //this._parentInfoService.setStudentImage(this.responseImage);
      //this.checkCriticalAlertFromMedical(this.addParentInfoModel);
    });
  }

  showPage(pageId) {    
    localStorage.setItem("pageId",pageId); 
    this.pageId=localStorage.getItem("pageId");
  }

}
