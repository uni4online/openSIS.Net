import { Component, OnInit, EventEmitter, Output, Input, ViewChild } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { WashInfoEnum } from '../../../../enums/wash-info.enum';
import { SchoolAddViewModel } from '../../../../models/schoolMasterModel';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import { SchoolService } from '../../../../../app/services/school.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoaderService } from '../../../../../app/services/loader.service';
import { SharedFunction } from '../../../shared/shared-function';
import { ImageCropperService } from '../../../../services/image-cropper.service';
import icEdit from '@iconify/icons-ic/twotone-edit';
import { SchoolCreate } from '../../../../enums/school-create.enum';

@Component({
  selector: 'vex-wash-info',
  templateUrl: './wash-info.component.html',
  styleUrls: ['./wash-info.component.scss'],
  animations: [

    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class WashInfoComponent implements OnInit {
  schoolCreate = SchoolCreate;
  @Input() schoolCreateMode: SchoolCreate;
  icEdit = icEdit;
  @Input() schoolDetailsForViewAndEdit;
  @Input() categoryId;
  form: FormGroup
  washinfo = WashInfoEnum;
  @ViewChild('f') currentForm: NgForm;
  f: NgForm;
  module = "School";
  schoolAddViewModel: SchoolAddViewModel = new SchoolAddViewModel();
  loading: boolean;
  formActionButtonTitle = "submit";
  constructor(private fb: FormBuilder,
    private schoolService: SchoolService,
    private snackbar: MatSnackBar,
    private router: Router,
    public translateService: TranslateService,
    private loaderService: LoaderService,
    private commonFunction: SharedFunction,
    private imageCropperService: ImageCropperService) {

    translateService.use('en');
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
  }
  ngOnInit(): void {
    if (this.schoolCreateMode == this.schoolCreate.VIEW) {
      this.schoolService.changePageMode(this.schoolCreateMode);
      this.imageCropperService.enableUpload(false);
      this.schoolAddViewModel = this.schoolDetailsForViewAndEdit;
    } else {
      this.imageCropperService.enableUpload(true);
      this.schoolService.changePageMode(this.schoolCreateMode);
      this.schoolAddViewModel = this.schoolService.getSchoolDetails();
    }
  }

  editWashInfo() {
    this.formActionButtonTitle = "update";
    this.imageCropperService.enableUpload(true);
    this.schoolCreateMode = this.schoolCreate.EDIT;
    this.schoolService.changePageMode(this.schoolCreateMode);
  }
  cancelEdit() {
    this.imageCropperService.enableUpload(false);
    this.schoolCreateMode = this.schoolCreate.VIEW;
    this.schoolService.changePageMode(this.schoolCreateMode);
  }

  submit() {
    this.currentForm.form.markAllAsTouched();
    if (this.currentForm.form.valid) {
      this.schoolAddViewModel.selectedCategoryId = this.schoolAddViewModel.schoolMaster.fieldsCategory[this.categoryId].categoryId;
      this.schoolService.UpdateSchool(this.schoolAddViewModel).subscribe(data => {
        if (typeof (data) == 'undefined') {
          this.snackbar.open(`Wash Info Updation failed` + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (data._failure) {
            this.snackbar.open(`Wash Info Updation failed` + data._message, 'LOL THANKS', {
              duration: 10000
            });
          } else {
            this.snackbar.open(`Wash Info Updation Successful`, '', {
              duration: 10000
            });
            this.schoolCreateMode = this.schoolCreate.VIEW;
            this.schoolService.changeMessage(true);
           this.schoolService.changePageMode(this.schoolCreateMode);
            
          }
        }

      })
    }
  }

}
