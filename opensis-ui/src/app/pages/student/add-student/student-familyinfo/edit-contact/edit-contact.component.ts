import { Component, OnInit ,Inject} from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import icBack from '@iconify/icons-ic/baseline-arrow-back';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { StudentService } from '../../../../../services/student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { AddParentInfoModel,ParentInfoList } from '../../../../../models/studentModel';
import { salutation,suffix ,relationShip} from '../../../../../enums/studentAdd.enum';
import { FormControl } from '@angular/forms';
import { ReplaySubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'vex-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]

})
export class EditContactComponent implements OnInit {

  icClose = icClose;
  icBack = icBack;
  form: FormGroup;
  addParentInfoModel: AddParentInfoModel = new AddParentInfoModel(); 
  parentInfoList:ParentInfoList=new ParentInfoList();
  contactModalTitle="addContact";
  contactModalActionTitle="submit";
  isEdit=false;
  salutationEnum=Object.keys(salutation);
  suffixEnum = Object.keys(suffix);
  relationShipEnum = Object.keys(relationShip);
  contact=[];
  searchCtrl: FormControl;
  searchFilterCtrl: FormControl;
  mode;
  viewData:any;
  public filteredContacts: ReplaySubject<ParentInfoList[]> = new ReplaySubject<ParentInfoList[]>(1);
  protected _onDestroy = new Subject<void>();

  constructor(
      private dialogRef: MatDialogRef<EditContactComponent>,
      private fb: FormBuilder, 
      public translateService:TranslateService,
      private studentService:StudentService,
      private snackbar: MatSnackBar,
      private router:Router,
      @Inject(MAT_DIALOG_DATA) public data
    ) 
    {
      this.callAllContact();
     
    }

  ngOnInit(): void {
    this.form = this.fb.group(
      {
        salutation: [''],
        firstname: ['',Validators.required],
        middleName: [''],
        lastname: ['',Validators.required],
        suffix: [''],
        relationShip: [''],
        mobile: [''],
        workPhone: [''],
        homePhone: [''],
        personalEmail: [''],
        workEmail: [''],
        userProfile: [''],
        loginEmail: [''],
        passwordHash: [''] ,
        studentAddressSame: [''],
        addressLineOne: [''],
        addressLineTwo: [''],
        country: [''],
        state: [''] ,
        city: [''],
        zip: [''],
        busNo: [''],
        busPickup: [''],
        busDropoff: ['']      
      });
      if(this.data.mode === "view"){       
       this.mode = "view";
       this.viewData = this.data.parentInfo;      
      }else{
        this.addParentInfoModel.parentInfo.contactType = this.data.contactType;
      }  
      
  }

  get f() {
    return this.form.controls;
  }

  closeDialog(){
    this.dialogRef.close(false);
  }
  callAllContact(){
    this.studentService.searchParentInfoForStudent(this.parentInfoList).subscribe(data => {
      this.contact = data.parentInfoForView;      
      /** control for the selected School */
      this.searchCtrl = new FormControl();
      this.searchFilterCtrl = new FormControl();     
      // load the initial School list      
      this.filteredContacts.next(this.contact.slice());
      /** control for the MatSelect filter keyword */
      this.searchFilterCtrl.valueChanges
        .pipe(takeUntil(this._onDestroy))
        .subscribe(() => {
          this.filterContacts();
        });      
    })       
  }
  filterContacts(){
    if (!this.contact) {
      return;
    }
    // get the search keyword
    let search = this.searchFilterCtrl.value;
    if (!search) {
      this.filteredContacts.next(this.contact.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the school
    this.filteredContacts.next(
      this.contact.filter(contact => contact.firstname.toLowerCase().indexOf(search) > -1)
    );
  }
  changeContact(data){
    if(data){
      this.addParentInfoModel.parentInfo.addressLineOne = data.addressLineOne;
      this.addParentInfoModel.parentInfo.firstname = data.firstname; 
      this.addParentInfoModel.parentInfo.lastname = data.lastname; 
      this.addParentInfoModel.parentInfo.schoolId = data.schoolId; 
      this.addParentInfoModel.parentInfo.tenantId = data.tenantId; 
    }     
  }
  submit() {
    if (this.form.valid)
    {      
      this.studentService.addParentForStudent(this.addParentInfoModel).subscribe(data => {
        if (typeof (data) == 'undefined') 
        {
          this.snackbar.open('Parent Information Submission failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else 
        {
          if (data._failure) {
            this.snackbar.open('Parent Information Submission failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          }
          else 
          {
            this.snackbar.open('Parent Information Submission Successful.', '', {
            duration: 10000
            });
            this.dialogRef.close(true);            
          }
        }
      })       
    }
  }

}
