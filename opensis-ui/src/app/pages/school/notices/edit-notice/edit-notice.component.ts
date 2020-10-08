import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { EditorChangeContent, EditorChangeSelection } from 'ngx-quill';
import { GetAllMembersList, Membership } from '../../../../../app/models/membershipModel';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, ThemePalette } from '@angular/material/core';
import { NoticeService } from '../../../../../app/services/notice.service';
import { TranslateService } from '@ngx-translate/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NoticeAddViewModel } from '../../../../../app/models/noticeModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { MY_FORMATS } from '../../../../pages/shared/format-datepicker';
import { MembershipService } from '../../../../../app/services/membership.service';

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
 
  constructor(private dialogRef: MatDialogRef<EditNoticeComponent>, private router: Router, private Activeroute: ActivatedRoute, private fb: FormBuilder, private _noticeService: NoticeService,private _membershipService: MembershipService, public translateService: TranslateService, private snackbar: MatSnackBar) {
    translateService.use('en');
    
  }

  ngOnInit(): void {

    var noticeId = localStorage.getItem('noticeId');
    //this.noticeAddViewModel._tenantName = this.tenant;
    this.noticeAddViewModel.notice.noticeId = +noticeId;
   // this.noticeAddViewModel._token = sessionStorage.getItem("token");
    if (this.noticeAddViewModel.notice.noticeId > 0) {
      this.AddOrEditNotice = "editNotice";
      this._noticeService.viewNotice(this.noticeAddViewModel).subscribe((res) => {
        this.noticeAddViewModel = res;
        if (this.noticeAddViewModel.notice.targetMembershipIds != null) {
          var membershipIds = this.noticeAddViewModel.notice.targetMembershipIds.split(',');
          this.memberArray = membershipIds.map(Number);
        }
      });

    }
    else {
      this.AddOrEditNotice = "Add Notice";
    }

    //this.getAllMembersList._tenantName = this.tenant;
    //this.getAllMembersList._token = sessionStorage.getItem("token");
    this._membershipService.getAllMembers(this.getAllMembersList).subscribe((res) => {

      this.getAllMembersList = res;
     
    });



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
   // this.noticeAddViewModel._tenantName = this.tenant;
    //this.noticeAddViewModel._token = sessionStorage.getItem("token");
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
          }

        });
      }
      else {
        this._noticeService.addNotice(this.noticeAddViewModel).subscribe(data => {
          if (data._failure) {
            this.snackbar.open('Notice saving failed. ' + data._message, '', {
              duration: 10000
            });
          } else {
            this.snackbar.open('Notice saved successful. ' ,'', {
              duration: 10000
            });
          }
        });
      }
      this.afterClosed.emit(true);
      this.dialogRef.close();
    }
  }

  changedEditor(event: EditorChangeContent | EditorChangeSelection) {

    if (event.source == 'user') {
      this.body = document.querySelector(".ql-editor").innerHTML;
    }
  }
  updateCheck(event) {
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

}
