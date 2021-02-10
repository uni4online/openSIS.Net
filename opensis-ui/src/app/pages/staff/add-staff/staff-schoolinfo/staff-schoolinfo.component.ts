import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icAdd from '@iconify/icons-ic/baseline-add';
import icClear from '@iconify/icons-ic/baseline-clear';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { StaffService } from '../../../../services/staff.service';
import { StaffSchoolInfoListModel, StaffSchoolInfoModel } from '../../../../models/staffModel';
import { Profile } from '../../../../enums/opensis-profile.enum';
import { Subject } from '../../../../enums/temp-subjects-list.enum';
import { GetAllGradeLevelsModel } from '../../../../models/gradeLevelModel';
import { GradeLevelService } from '../../../../services/grade-level.service';
import { OnlySchoolListModel } from '../../../../models/getAllSchoolModel';
import { SchoolService } from '../../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import icEdit from '@iconify/icons-ic/edit';
import moment from 'moment';
import { ImageCropperService } from '../../../../services/image-cropper.service';
import { ModuleIdentifier } from '../../../../enums/module-identifier.enum';
import { SharedFunction } from '../../../shared/shared-function';

@Component({
  selector: 'vex-staff-schoolinfo',
  templateUrl: './staff-schoolinfo.component.html',
  styleUrls: ['./staff-schoolinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StaffSchoolinfoComponent implements OnInit {
  getSchoolList: OnlySchoolListModel = new OnlySchoolListModel();
  staffCreate = SchoolCreate;
  profiles=Object.keys(Profile);
  subjects=Object.keys(Subject);
  @Input() staffDetailsForViewAndEdit;
  @Input() staffCreateMode: SchoolCreate;
  @ViewChild('f') currentForm: NgForm;
  divCount=[1];
  getAllGradeLevels:GetAllGradeLevelsModel = new GetAllGradeLevelsModel();
  staffSchoolInfoModel:StaffSchoolInfoModel=new StaffSchoolInfoModel();
  icAdd = icAdd;
  icClear = icClear;
  icEdit = icEdit;
  selectedSchoolId=[];
  otherGradeLevelTaught;
  otherSubjectTaught;
  cloneStaffModel;
  moduleIdentifier=ModuleIdentifier;
  constructor(public translateService: TranslateService,
    private snackbar: MatSnackBar,
    private staffService:StaffService,
    private gradeLevelService:GradeLevelService,
    private schoolService: SchoolService,
    private imageCropperService:ImageCropperService,
    private commonFunction: SharedFunction) {
    translateService.use('en');
  }

  ngOnInit(): void {
    if(this.staffCreateMode == this.staffCreate.EDIT){
      this.staffCreateMode = this.staffCreate.ADD
    }

    if (this.staffCreateMode == this.staffCreate.ADD) {
      this.getAllGradeLevel();
      this.callAllSchool();
      this.getAllStaffSchoolInfo()
    } else if (this.staffCreateMode == this.staffCreate.VIEW) {
      this.staffService.changePageMode(this.staffCreateMode);
      this.getAllStaffSchoolInfo();
    }
  }

  getAllGradeLevel(){
    this.getAllGradeLevels.schoolId=+sessionStorage.getItem("selectedSchoolId");
    this.getAllGradeLevels._tenantName=sessionStorage.getItem("tenant");
    this.getAllGradeLevels._token=sessionStorage.getItem("token");
    this.gradeLevelService.getAllGradeLevels(this.getAllGradeLevels).subscribe((res)=>{
       this.getAllGradeLevels.tableGradelevelList=res.tableGradelevelList;
    })
  }

  callAllSchool() {
    this.getSchoolList.tenantId = sessionStorage.getItem("tenantId");
    this.getSchoolList._tenantName = sessionStorage.getItem("tenant");
    this.getSchoolList._token = sessionStorage.getItem("token");
    this.schoolService.GetAllSchools(this.getSchoolList).subscribe((data) => {
      this.getSchoolList.schoolMaster = data.schoolMaster;
    });
  }

  onSchoolChange(schoolId, indexOfDynamicRow) {
    let index = this.getSchoolList.schoolMaster.findIndex((x) => {
      return x.schoolId === +schoolId;
    });
    this.staffSchoolInfoModel.staffSchoolInfoList[indexOfDynamicRow].schoolAttachedName=this.getSchoolList.schoolMaster[index].schoolName;
    this.selectedSchoolId[indexOfDynamicRow]=+schoolId
  }

  addMoreSchoolInfo(){
    this.staffSchoolInfoModel.staffSchoolInfoList.push(new StaffSchoolInfoListModel)
    this.divCount.push(2);
  }

  deleteSchoolInfo(index){
    this.divCount.splice(index,1);
    this.staffSchoolInfoModel.staffSchoolInfoList.splice(index,1);
    this.selectedSchoolId.splice(index,1);
  }

  submitSchoolInfo(){
    this.currentForm.form.markAllAsTouched();
    if(this.currentForm.form.valid){
      this.updateSchoolInfo();
    }
  }

  compareDate(index){
    let endDate=this.staffSchoolInfoModel.staffSchoolInfoList[index].endDate
    if(this.staffSchoolInfoModel.staffSchoolInfoList[index].startDate!=null){
      if(endDate==null || moment(endDate)>moment()){
        return true;
    }else{
      return false;
    }
    }else{
      return false;
    }
    
  }

  getAllStaffSchoolInfo(){
    this.staffSchoolInfoModel.staffId=this.staffService.getStaffId();
    this.staffSchoolInfoModel.staffSchoolInfoList[0].staffId=this.staffService.getStaffId();
    this.staffService.viewStaffSchoolInfo(this.staffSchoolInfoModel).subscribe((res)=>{
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Staff School Info Failed to Fetch. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          this.snackbar.open('Staff School Info Failed to Fetch. ' + res._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.staffSchoolInfoModel=res;
          this.cloneStaffModel=JSON.stringify(this.staffSchoolInfoModel)
          if(this.staffSchoolInfoModel.otherGradeLevelTaught!=null || this.staffSchoolInfoModel.otherGradeLevelTaught!=null){
          this.otherGradeLevelTaught=this.staffSchoolInfoModel.otherGradeLevelTaught.split(',');
          this.otherSubjectTaught=this.staffSchoolInfoModel.otherSubjectTaught.split(',');
          }
             this.manipulateArray();     
        }
      }
     }) 
  }

  manipulateArray(){
    for(let i=0;i<this.staffSchoolInfoModel.staffSchoolInfoList?.length;i++){
      this.staffSchoolInfoModel.staffSchoolInfoList[i].schoolAttachedId=this.staffSchoolInfoModel.staffSchoolInfoList[i].schoolAttachedId.toString();
      delete this.staffSchoolInfoModel.staffSchoolInfoList[i].staffMaster;
      this.selectedSchoolId[i]=+this.staffSchoolInfoModel.staffSchoolInfoList[i].schoolAttachedId;
      let endDate=this.staffSchoolInfoModel.staffSchoolInfoList[i].endDate
      if(endDate!=null && moment(endDate)<moment()){
        Object.assign(this.staffSchoolInfoModel.staffSchoolInfoList[i],{hide:true})
        this.selectedSchoolId.splice(i,1);
      }else{
        Object.assign(this.staffSchoolInfoModel.staffSchoolInfoList[i],{hide:false})
      }
    };  
  }

  updateSchoolInfo(){
    this.staffSchoolInfoModel.staffId=this.staffService.getStaffId();
    this.staffSchoolInfoModel.joiningDate=this.commonFunction.formatDateSaveWithoutTime(this.staffSchoolInfoModel.joiningDate)
    if(this.otherGradeLevelTaught!=undefined){
      this.staffSchoolInfoModel.otherGradeLevelTaught=this.otherGradeLevelTaught.toString()
    }
    if(this.otherSubjectTaught!=undefined){
      this.staffSchoolInfoModel.otherSubjectTaught=this.otherSubjectTaught.toString()
    }
    this.staffSchoolInfoModel?.staffSchoolInfoList?.map((item)=>{
      item.staffId=this.staffService.getStaffId();
      item.startDate=this.commonFunction.formatDateSaveWithoutTime(item.startDate);
      item.endDate=this.commonFunction.formatDateSaveWithoutTime(item.endDate)
    });
    this.staffSchoolInfoModel._token=sessionStorage.getItem("token");
    this.staffSchoolInfoModel._tenantName=sessionStorage.getItem("tenant");
       this.staffService.updateStaffSchoolInfo(this.staffSchoolInfoModel).subscribe((res)=>{
        if (typeof (res) == 'undefined') {
          this.snackbar.open('Staff School Info Update failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (res._failure) {
            this.snackbar.open('Staff School Info Update failed. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          } else {
            this.snackbar.open('Staff School Info Update Successful.', '', {
              duration: 10000
            });
            this.staffSchoolInfoModel=res;
            this.cloneStaffModel=JSON.stringify(this.staffSchoolInfoModel)
            if(this.staffCreateMode==this.staffCreate.EDIT || this.staffCreateMode==this.staffCreate.ADD){
              this.staffCreateMode = this.staffCreate.VIEW
            }
          this.staffService.changePageMode(this.staffCreateMode);
            this.manipulateArray();     
          }
        }
       }) 
  }

  editSchoolInfo() {
    if(this.staffSchoolInfoModel.staffSchoolInfoList!=null){
      for(let i=0;i<this.staffSchoolInfoModel.staffSchoolInfoList?.length;i++){
        this.divCount[i]=2;
      }
    }else{
    this.staffSchoolInfoModel=new StaffSchoolInfoModel();
    }
    this.getAllGradeLevel();
    this.callAllSchool();
    this.staffCreateMode=this.staffCreate.EDIT;
    this.staffService.changePageMode(this.staffCreateMode);
  }
  cancelEdit(){
    if(this.staffSchoolInfoModel!==JSON.parse(this.cloneStaffModel)){
      this.staffSchoolInfoModel=JSON.parse(this.cloneStaffModel);
      this.staffService.sendDetails(JSON.parse(this.cloneStaffModel));
    }
    this.staffCreateMode=this.staffCreate.VIEW;
    this.staffService.changePageMode(this.staffCreateMode);
    this.imageCropperService.cancelImage("staff");
  }
}

