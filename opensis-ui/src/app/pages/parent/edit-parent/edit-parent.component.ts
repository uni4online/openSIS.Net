import { Component, Input, OnInit } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
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
import { takeUntil } from 'rxjs/operators';

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
  pageStatus:string="View Parent";
  icGeneralInfo = icGeneralInfo;
  icAddress = icAddress;
  icAccessInfo = icAccessInfo;
  destroySubject$: Subject<void> = new Subject();
  pageId = 'General Info';
  parentCreate = SchoolCreate;
  parentId: number;
  @Input() parentCreateMode: SchoolCreate = SchoolCreate.VIEW;
  addParentInfoModel: AddParentInfoModel = new AddParentInfoModel();
  parentTitle;
  responseImage: string;
  
  enableCropTool = true;
  constructor(private layoutService: LayoutService,
    private parentInfoService:ParentInfoService,
    private imageCropperService:ImageCropperService) {
    this.layoutService.collapseSidenav();
    this.imageCropperService.getCroppedEvent().pipe(takeUntil(this.destroySubject$)).subscribe((res) => {
      this.parentInfoService.setParentImage(res[1]);
    });
    this.parentInfoService.modeToUpdate.pipe(takeUntil(this.destroySubject$)).subscribe((res)=>{
      if(res==this.parentCreate.EDIT){
        this.pageStatus="Edit Parent"
      }else if(res==this.parentCreate.VIEW){
        this.pageStatus="View Parent"
      }
    });
  }

  ngOnInit(): void {
    this.parentCreateMode = this.parentCreate.VIEW;
    this.parentId = this.parentInfoService.getParentId();   
    this.getParentDetailsUsingId();
  }
  

  getParentDetailsUsingId(){
    this.addParentInfoModel.parentInfo.parentId = this.parentId;
    this.parentInfoService.viewParentInfo(this.addParentInfoModel).subscribe(data => {
      this.addParentInfoModel = data;
      this.parentInfoService.sendDetails(this.addParentInfoModel);
      this.parentTitle = this.addParentInfoModel.parentInfo.salutation + " " +this.addParentInfoModel.parentInfo.firstname + " " + this.addParentInfoModel.parentInfo.middlename+ " " + this.addParentInfoModel.parentInfo.lastname;
    
      this.responseImage = this.addParentInfoModel.parentInfo.parentPhoto;     
      this.parentInfoService.setParentImage(this.responseImage);
      
    });
  }

  showPage(pageId) {    
    localStorage.setItem("pageId",pageId); 
    this.pageId=localStorage.getItem("pageId");
  }

  ngOnDestroy() {
    this.parentInfoService.setParentImage(null);
    this.destroySubject$.next();
    this.destroySubject$.complete();
  }

}
