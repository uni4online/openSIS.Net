import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';

export interface DialogData {
  title: string;
  message: string;
}

@Component({
  selector: 'vex-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {

  
  dialogData: DialogData;
  title:string;
  message:string;

  constructor(
      public dialogRef: MatDialogRef<ConfirmDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: DialogData,
      public translateService:TranslateService,
              ) {
                translateService.use('en');
              }

  ngOnInit() {

  }

  onConfirm(): void {
      // Close the dialog, return true
      this.dialogRef.close(true);
  }

  onDismiss(): void {
      // Close the dialog, return false
      this.dialogRef.close(false);
  }
}
