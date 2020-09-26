import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSpinnerOverlayComponent } from './mat-spinner-overlay/mat-spinner-overlay.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { ProfileImageComponent } from './profile-image/profile-image.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { MatDialogModule} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IconModule } from '@visurel/iconify-angular';
import { PhoneMaskDirective } from './directives/phone-mask.directive';

import { EmtyBooleanCheckPipe } from 'src/app/pages/shared-module/user-define-pipe/emty-boolean-check-pipe';
import { EmtyValueCheckPipe } from 'src/app/pages/shared-module/user-define-pipe/emty-value-check.pipe';
import {EmtyNumberCheckPipe} from 'src/app/pages/shared-module/user-define-pipe/emty-number-check.pipe';

@NgModule({
  declarations: [MatSpinnerOverlayComponent, ProfileImageComponent,PhoneMaskDirective,EmtyBooleanCheckPipe,
    EmtyValueCheckPipe,EmtyNumberCheckPipe],
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    ImageCropperModule,
    MatSnackBarModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    IconModule
  ],
  exports:[MatSpinnerOverlayComponent, ProfileImageComponent,PhoneMaskDirective,EmtyBooleanCheckPipe,EmtyValueCheckPipe,EmtyNumberCheckPipe]
})
export class SharedModuleModule { }
