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
  recordFor: string;
  constructor(private dialog: MatDialog, public translateService: TranslateService, private _noticeService: NoticeService, private snackbar: MatSnackBar) {
    translateService.use('en');

  }

  ngOnInit(): void {
    this.showRecords('All');
  }
  showRecords(event) {
    this._noticeService.getAllNotice(this.noticeListViewModel).subscribe((res) => {
      this.noticeList = res.noticeList;
      if (event != 'All') {
        this.recordFor = event.target.innerHTML;
        var today = new Date();
        if (this.recordFor.toLowerCase() == "today") {
          this.noticeList = res.noticeList.filter(m => new Date(m.validFrom).toDateString() == today.toDateString());
        }
        else if (this.recordFor.toLowerCase() == "upcoming") {
          this.noticeList = res.noticeList.filter(m => new Date(m.validFrom)  > new Date());
        }
        else if (this.recordFor.toLowerCase() == "past") {
          this.noticeList = res.noticeList.filter(m => new Date(m.validFrom)  < new Date());
        }
      }

    });
  }

  openAddNew() {
    localStorage.setItem('noticeId', '');
    this.dialog.open(EditNoticeComponent, {
      data: null,
      width: '800px'
    });

  }


}
