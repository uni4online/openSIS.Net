import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { EditorChangeContent, EditorChangeSelection } from 'ngx-quill';
import { GetAllMembersList } from '../../../../../app/models/membershipModel';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { NoticeService } from '../../../../../app/services/notice.service';
import { TranslateService } from '@ngx-translate/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NoticeAddViewModel } from '../../../../../app/models/noticeModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { MY_FORMATS } from '../../../../pages/shared/format-datepicker';
import { MembershipService } from '../../../../../app/services/membership.service';
import * as moment from 'moment';

import { ValidationService } from '../../../shared/validation.service';

@Component({
  selector: 'vex-edit-notice',
  templateUrl: './edit-notice.component.html',
  styleUrls: ['./edit-notice.component.scss',
    '../../../../../../node_modules/quill/dist/quill.snow.css',
    '../../../../../@vex/styles/partials/plugins/_quill.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ]
})
export class EditNoticeComponent implements OnInit {
  AddOrEditNotice: string;
  @Output() afterClosed = new EventEmitter<boolean>();
  getAllMembersList: GetAllMembersList = new GetAllMembersList();
  icClose = icClose;
  body: string = null;
  noticeAddViewModel = new NoticeAddViewModel();
  memberArray: number[] = [];
  form: FormGroup;
  membercount:number;
  constructor(private dialogRef: MatDialogRef<EditNoticeComponent>, 
    @Inject(MAT_DIALOG_DATA) public data:any,
    private router: Router, private Activeroute: ActivatedRoute, private fb: FormBuilder,
     private _noticeService: NoticeService,private _membershipService: MembershipService,
      public translateService: TranslateService, private snackbar: MatSnackBar) {
        
    translateService.use('en');
    
  }

  private formatDate(date: any): string {
    if (date === undefined || date === null) {
      return undefined;
    } else {
      return moment(date).format('YYYY-MM-DDTHH:mm:ss.SSS');
    }
  }

  ngOnInit(): void {
    if(this.data==null){
      this.snackbar.open('Null vallue occur. ','', {
        duration: 10000
      });
    }
    else{
      this.membercount=this.data.membercount
      this.getAllMembersList=this.data.allMembers
      if(this.data.notice==null){
        this.AddOrEditNotice = "Add Notice";
      }
      else{
        this.AddOrEditNotice = "editNotice";
        this.noticeAddViewModel.notice=this.data.notice
        
        this._noticeService.viewNotice(this.noticeAddViewModel).subscribe(
          (res)=>{
            
            this.noticeAddViewModel.notice = res.notice;
            if(this.noticeAddViewModel.notice.targetMembershipIds!=null){
              var membershipIds:string[] = this.noticeAddViewModel.notice.targetMembershipIds.split(',');
          this.memberArray = membershipIds.map(Number);
            }
          }
        )
      }
      
    }
    this.form = this.fb.group({
      Title: ['', Validators.required],
      Body: [''],
      valid_from: ['', Validators.required],
      valid_to: ['', Validators.required],
      TargetMembershipIds: ['']
    });


  }
  get f() {
    return this.form.controls;
  }
  submitNotice() {
    this.noticeAddViewModel.notice.body = this.form.value.Body;
    this.noticeAddViewModel.notice.title = this.form.value.Title;
    this.noticeAddViewModel.notice.validFrom = this.form.value.valid_from;
    this.noticeAddViewModel.notice.validTo = this.form.value.valid_to;
    this.noticeAddViewModel.notice.targetMembershipIds = this.memberArray.toString();
  
    if (this.form.valid) {
      if (this.noticeAddViewModel.notice.noticeId > 0) {
        
          this._noticeService.updateNotice(this.noticeAddViewModel).subscribe(data => {
            if (data._failure) {
              this.snackbar.open('Notice updating failed. ' + data._message,'', {
                duration: 10000
              });
            } else {
              this.snackbar.open('Notice updated successful. ','', {
                duration: 10000
              });
              this._noticeService.changeNotice(true)
              this.dialogRef.close();
            }
  
          });
      }
      else {
      
        this.noticeAddViewModel.notice.validFrom=this.formatDate(this.noticeAddViewModel.notice.validFrom)
        this.noticeAddViewModel.notice.validTo=this.formatDate(this.noticeAddViewModel.notice.validTo)
          this._noticeService.addNotice(this.noticeAddViewModel).subscribe(data => {
            if (data._failure) {
              this.snackbar.open('Notice saving failed. ' + data._message, '', {
                duration: 10000
              });
            } else {
              this.snackbar.open('Notice saved successful. ' ,'', {
                duration: 10000
              });
      this.dialogRef.close(true);
            }
          });
      }
      this.afterClosed.emit(true);
    }
  }

  changedEditor(event: EditorChangeContent | EditorChangeSelection) {
    if (event.source == 'user') {
      this.body = document.querySelector(".ql-editor").innerHTML;
    }
  }
  updateCheck(event) {
    if(this.memberArray.length===this.getAllMembersList.getAllMemberList.length){
      for (let i = 1; i <= this.getAllMembersList.getAllMemberList.length; i++) {
        var index = this.memberArray.indexOf(i);
        if (index > -1) {
          this.memberArray.splice(index, 1);
        }
        else {
          this.memberArray.push(i);
        }
      }
    }
    else if(this.memberArray.length===0){
      for (let i = 1; i <= this.getAllMembersList.getAllMemberList.length; i++) {
        var index = this.memberArray.indexOf(i);
        if (index > -1) {
          this.memberArray.splice(index, 1);
        }
        else {
          this.memberArray.push(i);
        }
      }
    }
    else{
      for (let i = 1; i <= this.getAllMembersList.getAllMemberList.length; i++) {
        let index = this.memberArray.indexOf(i);
        if (index > -1) {
          continue;
        }
        else {
          this.memberArray.push(i);
        }
      }
    }
   

  }
  selectChildren(event, id) {
    event.preventDefault();
    var index = this.memberArray.indexOf(id);
    if (index > -1) {
      this.memberArray.splice(index, 1);
    }
    else {
      this.memberArray.push(id);
    }

  }
  dateCompare() {
   
    let openingDate = this.form.controls.valid_from.value
    let closingDate = this.form.controls.valid_to.value
   
    if (ValidationService.compareValidation(openingDate, closingDate) === false) {
      this.form.controls.valid_to.setErrors({ compareError: true })
      
    }

  }

}
