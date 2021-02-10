import { Component, Input, OnInit } from '@angular/core';
import icPreview from '@iconify/icons-ic/round-preview';
import icPeople from '@iconify/icons-ic/twotone-people';
import icMoreVert from '@iconify/icons-ic/more-vert';
import { NoticeDeleteModel } from '../../../../../app/models/noticeDeleteModel';
import { NoticeService } from '../../../../../app/services/notice.service';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NoticeAddViewModel, NoticeListViewModel } from '../../../../models/noticeModel';
import { MatDialog } from '@angular/material/dialog';
import { EditNoticeComponent } from '../edit-notice/edit-notice.component';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmDialogComponent } from '../../../../../app/pages/shared-module/confirm-dialog/confirm-dialog.component';
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
  showMember = true;
  
  noticeDeleteModel = new NoticeDeleteModel();
  @Input() title: string;
  @Input() notice;
  @Input() noticeId: number;
  @Input() imageUrl: string;
  @Input() visibleFrom: string;
  @Input() visibleTo: number;
  @Input() getAllMembersList;

  constructor(private dialog: MatDialog,private noticeService: NoticeService,public translateService: TranslateService, private Activeroute: ActivatedRoute, private snackbar: MatSnackBar) {
    translateService.use('en');
  }

  ngOnInit(): void {
    if(this.visibleTo.toString() === ""){
      this.showMember = false;
    }
    
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
  this.noticeDeleteModel.NoticeId= +id;
  this.noticeService.deleteNotice(this.noticeDeleteModel).subscribe(
    (res) => {
    if (res._failure) {
      this.snackbar.open('Notice Deletion failed. ' + res._message, '', {
        duration: 10000
      });
    } else {
      //  this.noticeService.getAllNotice(this.noticeListViewModel).subscribe((res) => {
      //    this.noticeListViewModel = res;
      //  });
      this.snackbar.open('Notice Deleted Successful.', '', {
        duration: 10000
      });
      this.noticeService.changeNotice(true)      
    }
  });
}

    
  editNotice(noticeId: number) 
  {
    this.dialog.open(EditNoticeComponent, {
      data: {allMembers:this.getAllMembersList,notice:this.notice,membercount:this.getAllMembersList.getAllMemberList.length},
      width: '800px'
    })    
  }
}
