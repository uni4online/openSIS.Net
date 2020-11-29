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
import { EmtyBooleanCheckPipe } from './user-define-pipe/emty-boolean-check-pipe';
import { EmtyValueCheckPipe } from './user-define-pipe/emty-value-check.pipe';
import {EmtyNumberCheckPipe} from './user-define-pipe/emty-number-check.pipe';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { TranslateModule } from '@ngx-translate/core';
import {InvalidControlScrollDirective} from './directives/invalid-control-scroll.directive';
import {EmtyBooleanCheckReversePipe} from './user-define-pipe/emty-boolean-check.reverse.pipe';
import { SafePipe } from './user-define-pipe/safeHtml.pipe';
import { TransformDateTimePipe } from './user-define-pipe/transform-datetime-pipe';
import {EmailvalidatorDirective} from './user-defined-directives/emailvalidator.directive';
import {PhonevalidatorDirective} from './user-defined-directives/phonevalidator.directive';
@NgModule({
  declarations: [MatSpinnerOverlayComponent, ProfileImageComponent,PhoneMaskDirective,EmtyBooleanCheckPipe,EmtyBooleanCheckReversePipe,
    EmtyValueCheckPipe,EmtyNumberCheckPipe, ConfirmDialogComponent,InvalidControlScrollDirective,TransformDateTimePipe,EmailvalidatorDirective,
    PhonevalidatorDirective,SafePipe],
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    ImageCropperModule,
    MatSnackBarModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    IconModule,
    MatCardModule,
    TranslateModule
  ],
  exports:[MatSpinnerOverlayComponent, ProfileImageComponent,PhoneMaskDirective,EmtyBooleanCheckPipe,EmtyValueCheckPipe,EmtyNumberCheckPipe,InvalidControlScrollDirective,
    EmtyBooleanCheckReversePipe,TransformDateTimePipe,EmailvalidatorDirective,PhonevalidatorDirective,SafePipe]
})
export class SharedModuleModule { }
