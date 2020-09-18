import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSpinnerOverlayComponent } from './mat-spinner-overlay/mat-spinner-overlay.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { ProfileImageComponent } from './profile-image/profile-image.component'

import { ImageCropperModule } from 'ngx-image-cropper';

import {MatSnackBarModule} from '@angular/material/snack-bar';
import { MatDialogModule} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IconModule } from '@visurel/iconify-angular';
import { PhoneMaskDirective } from './phone-mask.directive';

@NgModule({
  declarations: [MatSpinnerOverlayComponent, ProfileImageComponent,PhoneMaskDirective],
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
  exports:[MatSpinnerOverlayComponent, ProfileImageComponent,PhoneMaskDirective]
})
export class SharedModuleModule { }
