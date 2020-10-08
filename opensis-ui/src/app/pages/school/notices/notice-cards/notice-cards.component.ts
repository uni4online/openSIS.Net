import { Component, Input, OnInit, Output } from '@angular/core';
import icPreview from '@iconify/icons-ic/round-preview';
import icPeople from '@iconify/icons-ic/twotone-people';
import icMoreVert from '@iconify/icons-ic/more-vert';
import { NoticeDeleteModel } from 'src/app/models/noticeDeleteModel';
import { NoticeService } from 'src/app/services/notice.service';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NoticeAddViewModel, NoticeListViewModel } from 'src/app/models/noticeModel';
import { MatDialog } from '@angular/material/dialog';
import { EditNoticeComponent } from '../edit-notice/edit-notice.component';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmDialogComponent } from 'src/app/pages/shared-module/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'vex-notice-cards',
  templateUrl: './notice-cards.component.html',
  styleUrls: ['./notice-cards.component.scss']
})
export class NoticeCardsComponent implements OnInit {
  noticeListViewModel: NoticeListViewModel = new NoticeListViewModel();
  noticeaddViewModel: NoticeAddViewModel = new NoticeAddViewModel();
  icPreview = icPreview;
  icPeople = icPeople;
  icMoreVert = icMoreVert;
  noticeDeleteModel = new NoticeDeleteModel();
  @Input() title: string;
  @Input() noticeId: number;
  @Input() imageUrl: string;
  @Input() visibleFrom: string;
  @Input() visibleTo: number;
  //tenant: string;

  constructor(private dialog: MatDialog,private _noticeService: NoticeService,public translateService: TranslateService, private Activeroute: ActivatedRoute, private snackbar: MatSnackBar) {
    //this.tenant = sessionStorage.getItem('tenant');
    translateService.use('en');
  }

  ngOnInit(): void {
  }

  deleteNoticeConfirm(id) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete this notice."}
    });
    // listen to response
    dialogRef.afterClosed().subscribe(dialogResult => {
      // if user pressed yes dialogResult will be true, 
      // if user pressed no - it will be false
      if(dialogResult){
        this.deleteNotice(id);
      }
   });
  }

deleteNotice(id){
  this.noticeDeleteModel._tenantName = sessionStorage.getItem("tenant");
  this.noticeDeleteModel.NoticeId= +id;
  this.noticeDeleteModel._token = sessionStorage.getItem("token");
  this._noticeService.deleteNotice(this.noticeDeleteModel).subscribe((res) => {
    if (res._failure) {
      this.snackbar.open('Notice Deletion failed. ' + res._message, 'LOL THANKS', {
        duration: 10000
      });
    } else {
      // this.noticeListViewModel._tenantName = this.tenant;
      // this.noticeListViewModel._token = sessionStorage.getItem("token");
       this._noticeService.getAllNotice(this.noticeListViewModel).subscribe((res) => {
         this.noticeListViewModel = res;
       });
      this.snackbar.open('Notice Deleted Successful.', '', {
        duration: 10000
      });
    }
  });
}

    
editNotice(noticeId: number) {
    localStorage.setItem('noticeId',noticeId.toString());
    this.dialog.open(EditNoticeComponent, {
      data: null,
      width: '800px'
    });
  }
}
