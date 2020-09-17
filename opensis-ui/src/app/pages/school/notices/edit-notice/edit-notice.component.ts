import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { EditorChangeContent, EditorChangeSelection } from 'ngx-quill';

@Component({
  selector: 'vex-edit-notice',
  templateUrl: './edit-notice.component.html',
  styleUrls: ['./edit-notice.component.scss',
  '../../../../../../node_modules/quill/dist/quill.snow.css',
  '../../../../../@vex/styles/partials/plugins/_quill.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditNoticeComponent implements OnInit {

  icClose = icClose;

  constructor(private dialogRef: MatDialogRef<EditNoticeComponent>, private fb: FormBuilder) { }

  ngOnInit(): void {
  }
  changedEditor(event: EditorChangeContent | EditorChangeSelection) {

    if(event.source== 'user'){
      console.log("test",document.querySelector(".ql-editor").innerHTML);
    }
  }

}
