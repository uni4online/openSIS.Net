import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-edit-race',
  templateUrl: './edit-race.component.html',
  styleUrls: ['./edit-race.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class EditRaceComponent implements OnInit {
  icClose = icClose;

  constructor(public translateService:TranslateService,private dialogRef: MatDialogRef<EditRaceComponent>) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

}
