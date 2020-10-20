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
import { LoaderService } from '../../../services/loader.service';

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
  loading:boolean;
  constructor(private dialog: MatDialog, 
    public translateService: TranslateService, 
    private _noticeService: NoticeService,
    private _membershipService:MembershipService, 
    private snackbar: MatSnackBar,
    private loaderService: LoaderService) {
    translateService.use('en');
    this.loaderService.isLoading.subscribe((v) => {
      this.loading = v;
    });
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
          this.noticeList =res.noticeList.filter(
            m=>{
                let validFrom=moment(m.validFrom).format('DD-MM-YYYY').toString();
                let validFromarr=validFrom.split("-");
                let validFromDate=+validFromarr[0]
                let validFromMonth=+validFromarr[1]
                let validFromYear=+validFromarr[2]
                let validTo=moment(m.validTo).format('DD-MM-YYYY').toString();
                let validToarr=validTo.split('-');
                let validToDate=+validToarr[0]
                let validToMonth=+validToarr[1]
                let validToYear=+validToarr[2]
                let today=moment().format('DD-MM-YYYY').toString();
                let todayarr=today.split('-');
                let todayDate=+todayarr[0]
                let todayMonth=+todayarr[1]
                let todayYear=+todayarr[2]

                if(validFromYear<=todayYear && validToYear>=todayYear){
                  if(validFromMonth<=todayMonth && validToMonth>=todayMonth){
                    if(validFromDate<=todayDate && validToDate>=todayDate){
                      return m;
                    }
                  }
                }
            }
          )
        }
        else if (this.recordFor.toLowerCase() == "upcoming") {
          this.noticeList =  res.noticeList.filter(
            m=>{
              
              let validFrom=moment(m.validFrom).format('DD-MM-YYYY').toString();
              let validFromarr=validFrom.split("-");
              let validFromDate=+validFromarr[0]
              let validFromMonth=+validFromarr[1]
              let validFromYear=+validFromarr[2]
              let today=moment().format('DD-MM-YYYY').toString();
              let todayarr=today.split('-');
              let todayDate=+todayarr[0]
              let todayMonth=+todayarr[1]
              let todayYear=+todayarr[2]
              if(validFromYear>todayYear){
                return m;
              }
              else if(validFromYear==todayYear){
                if(validFromMonth>todayMonth){
                  return m;
                }
                else if(validFromMonth==todayMonth){
                  if(validFromDate>todayDate){
                    return m;
                  }
                }
              }
            }
          )
        }
        else if (this.recordFor.toLowerCase() == "past") {
          this.noticeList =res.noticeList.filter(
            m=>{
              
              let validTo=moment(m.validTo).format('DD-MM-YYYY').toString();
              let validToarr=validTo.split('-');
              let validToDate=+validToarr[0]
              let validToMonth=+validToarr[1]
              let validToYear=+validToarr[2]
              let today=moment().format('DD-MM-YYYY').toString();
              let todayarr=today.split('-');
              let todayDate=+todayarr[0]
              let todayMonth=+todayarr[1]
              let todayYear=+todayarr[2]
              if(validToYear<todayYear){
                return m;
              }
              else if(validToYear==todayYear){
                if(validToMonth<todayMonth){
                  return m;
                }
                else if(validToMonth==todayMonth){
                  if(validToDate<todayDate){
                    return m;
                  }
                }
              }
            }
          )
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
