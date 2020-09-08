import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SchoolDetailsRoutingModule } from './school-details-routing-module';
import { SchoolDetailsComponent } from './school-details.component';
import { SecondaryToolbarModule } from '../../../../../@vex/components/secondary-toolbar/secondary-toolbar.module';
import { BreadcrumbsModule } from '../../../../../@vex/components/breadcrumbs/breadcrumbs.module';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { PageLayoutModule } from '../../../../../@vex/components/page-layout/page-layout.module';
import { ContainerModule } from '../../../../../@vex/directives/container/container.module';
import { IconModule } from '@visurel/iconify-angular';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [SchoolDetailsComponent],
  imports: [
    CommonModule,
    SchoolDetailsRoutingModule,
    SecondaryToolbarModule,
    BreadcrumbsModule,
    MatIconModule,
    IconModule,
    MatButtonModule,
    PageLayoutModule,
    ContainerModule,
    MatCardModule,
    MatSnackBarModule
  ]
})
export class SchoolDetailsModule { }
