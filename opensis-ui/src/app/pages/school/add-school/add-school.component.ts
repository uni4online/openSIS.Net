import { Component, OnInit, OnDestroy } from '@angular/core';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';
import { ImageCropperService } from '../../../services/image-cropper.service';

import { SchoolAddViewModel } from '../../../models/schoolMasterModel';
import { ActivatedRoute } from '@angular/router';
import { SchoolService } from '../../../services/school.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedFunction } from '../../../pages/shared/shared-function';
import { CommonService } from '../../../services/common.service';
import { LayoutService } from '../../../../../src/@vex/services/layout.service';
import { CustomFieldAddView, CustomFieldListViewModel, CustomFieldModel } from '../../../../../src/app/models/customFieldModel';
import { CustomFieldService } from '../../../services/custom-field.service';
import { FieldsCategoryAddView, FieldsCategoryListView, FieldsCategoryModel } from '../../../models/fieldsCategoryModel';
import { LoaderService } from '../../../services/loader.service';
import { SchoolCreate } from '../../../enums/school-create.enum';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
@Component({
  selector: 'vex-add-school',
  templateUrl: './add-school.component.html',
  styleUrls: ['./add-school.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})

export class AddSchoolComponent implements OnInit, OnDestroy {
  SchoolCreate = SchoolCreate;
  schoolCreateMode: SchoolCreate = SchoolCreate.ADD;
  schoolTitle = "Add School Information";
  pageStatus = "Add School"
  responseImage: string;
  image: string = '';
  fieldsCategory = [];
  fieldsCategoryListView = new FieldsCategoryListView();
  schoolId: number = null;
  enableCropTool = false;
  indexOfCategory :number = 0;
  modeForImage = true;
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  customFieldModel: [CustomFieldModel];
  currentCategory = 1;
  loading: boolean;
  destroySubject$: Subject<void> = new Subject();
  constructor(private _imageCropperService: ImageCropperService,
    private Activeroute: ActivatedRoute,
    private snackbar: MatSnackBar,
    private _schoolService: SchoolService,
    private commonFunction: SharedFunction,
    private layoutService: LayoutService,
    private _loaderService: LoaderService,
    private customFieldservice: CustomFieldService) {
    this.layoutService.collapseSidenav();
    this._imageCropperService.getUncroppedEvent().pipe(takeUntil(this.destroySubject$)).subscribe((res)=>{
      this._schoolService.setSchoolImage(btoa(res.target.result));
    });
    this._schoolService.categoryToSend.subscribe((res) => {
        this.currentCategory = this.currentCategory + 1;
    });
    this._loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
  }

  ngOnInit() {
    this.schoolCreateMode = this.SchoolCreate.ADD;
    this._schoolService.sendDetails(this.schoolAddViewModel);
    this.schoolId = this._schoolService.getSchoolId();
    if (this.schoolId != null) {
      this.schoolCreateMode = this.SchoolCreate.VIEW;
      this._imageCropperService.nextMessage(true);
      this.getSchoolGeneralandWashInfoDetails();
      this.onViewMode();
    }else if (this.schoolCreateMode == this.SchoolCreate.ADD) {
      this.getAllFieldsCategory();
    }

  }

  onViewMode() {
    this.pageStatus = "View School"

  }

  changeCategory(categoryDetails,index) {
    let schoolDetails = this._schoolService.getSchoolDetails();
    if (schoolDetails != undefined || schoolDetails != null) {
      this.schoolCreateMode = this.SchoolCreate.EDIT;
      this.currentCategory = categoryDetails.categoryId;
      this.indexOfCategory= index;
      this.schoolAddViewModel = schoolDetails;
    }

    if (this.schoolCreateMode == this.SchoolCreate.VIEW) {
      this.pageStatus = "View School"
      this._imageCropperService.nextMessage(true);
      this.currentCategory = categoryDetails.categoryId;
      this.indexOfCategory= index;
    }
  }

  getAllFieldsCategory() {
    this.fieldsCategoryListView.module = "School";
    this.fieldsCategoryListView.schoolId = +sessionStorage.getItem('selectedSchoolId');
    this.customFieldservice.getAllFieldsCategory(this.fieldsCategoryListView).subscribe((res) => {
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Custom Field list failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          this.snackbar.open('Custom Field list failed. ' + res._message, 'LOL THANKS', {
            duration: 10000
          });
        }
        else {
          this.fieldsCategory = res.fieldsCategoryList.filter(x => x.isSystemCategory == true);
         

        }
      }
    }
    );
  }

  getSchoolGeneralandWashInfoDetails() {
    this.schoolAddViewModel.schoolMaster.schoolDetail[0].schoolId = this._schoolService.getSchoolId();
    this.schoolAddViewModel.schoolMaster.schoolId = this.schoolAddViewModel.schoolMaster.schoolDetail[0].schoolId;
    this._schoolService.ViewSchool(this.schoolAddViewModel).subscribe(data => {
      this.schoolAddViewModel = data;
      this._schoolService.sendDetails(this.schoolAddViewModel);
      this.fieldsCategory = data.schoolMaster.fieldsCategory;
      this.schoolTitle = this.schoolAddViewModel.schoolMaster.schoolName;
      this.responseImage = this.schoolAddViewModel.schoolMaster.schoolDetail[0].schoolLogo;
      this._schoolService.setSchoolImage(this.responseImage);
    });
  }

  ngOnDestroy() {
    this._schoolService.setSchoolDetails(null)
    this._schoolService.setSchoolImage(null);
    this._schoolService.setSchoolId(null);
    this.destroySubject$.next();
  }

}
