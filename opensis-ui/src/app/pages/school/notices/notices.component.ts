import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import icArrowDropDown from '@iconify/icons-ic/arrow-drop-down';
import icAdd from '@iconify/icons-ic/add';
import { MatDialog } from '@angular/material/dialog';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import { EditNoticeComponent } from '../notices/edit-notice/edit-notice.component';
import { NoticeService } from '../../../../app/services/notice.service';
import { NoticeListViewModel } from '../../../../app/models/noticeModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { MembershipService } from '../../../../app/services/membership.service';
import { GetAllMembersList } from '../../../../app/models/membershipModel';
import moment from 'moment';

@Component({
  selector: 'vex-notices',
  templateUrl: './notices.component.html',
  styleUrls: ['./notices.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class NoticesComponent implements OnInit {

  @Input() afterClosed = new EventEmitter<boolean>();
  noticeListViewModel: NoticeListViewModel = new NoticeListViewModel();
  noticeList = [];
  icPreview = icArrowDropDown;
  icAdd = icAdd;
  activateOpenAddNew:boolean=true;
  recordFor: string;
  getAllMembersList: GetAllMembersList = new GetAllMembersList();
  constructor(private dialog: MatDialog, 
    public translateService: TranslateService, 
    private _noticeService: NoticeService,
    private _membershipService:MembershipService, 
    private snackbar: MatSnackBar) {
    translateService.use('en');
    this._noticeService.currentNotice.subscribe(
      res=>{
        if(res){
          this.showRecords('All');
        }
      }
    )

  }

  ngOnInit(): void {
    this.showRecords('All');
    this.getMemberList();
  }
  getMemberList(){
    this._membershipService.getAllMembers(this.getAllMembersList).subscribe(
      (res) => {
        if(typeof(res)=='undefined'){
          this.snackbar.open('No Member Found. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {     
            this.snackbar.open('No Member Found. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          }
          else {       
            this.getAllMembersList = res;   
          }
        }
    });
  }
  showRecords(event) {
    this._noticeService.getAllNotice(this.noticeListViewModel).subscribe(
      (res) => {
      this.noticeList = res.noticeList;
      if (event != 'All') {
        this.recordFor = event.target.innerHTML;
        var today = new Date();
        if (this.recordFor.toLowerCase() == "today") {
          this.noticeList = res.noticeList.filter(m => moment(m.validFrom).format('DD-MM-YYYY')<= moment().format('DD-MM-YYYY') && moment(m.validTo).format('DD-MM-YYYY')>= moment().format('DD-MM-YYYY'));
        }
        else if (this.recordFor.toLowerCase() == "upcoming") {
          this.noticeList = res.noticeList.filter(m => moment(m.validFrom).format('DD-MM-YYYY')> moment().format('DD-MM-YYYY'));
        }
        else if (this.recordFor.toLowerCase() == "past") {

          this.noticeList = res.noticeList.filter(m => moment(m.validTo).format('DD-MM-YYYY')< moment().format('DD-MM-YYYY'));
          
        }
      }
    });
  }

  openAddNew() {
      this.dialog.open(EditNoticeComponent, {
        data: {allMembers:this.getAllMembersList,membercount:this.getAllMembersList.getAllMemberList.length},
        width: '800px'
      }).afterClosed().subscribe(
        res=>{
          if(res){
            this.showRecords('All');
          }
        }
      )
  }


}
