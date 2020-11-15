import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/baseline-add';
import icComment from '@iconify/icons-ic/twotone-comment';
import { MatDialog } from '@angular/material/dialog';
import { EditCommentComponent } from './edit-comment/edit-comment.component';


@Component({
  selector: 'vex-student-comments',
  templateUrl: './student-comments.component.html',
  styleUrls: ['./student-comments.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StudentCommentsComponent implements OnInit {

  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;
  icComment = icComment;

  constructor(private fb: FormBuilder, private dialog: MatDialog, public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

  openAddNew() {
    this.dialog.open(EditCommentComponent, {
      width: '800px'
    });
  }

}
