import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SchoolDetailsComponent } from './school-details.component';
import { AddSchoolComponent } from '../../add-school/add-school.component';
import { MarkingPeriodsComponent } from '../../marking-periods/marking-periods.component';
import { NoticesComponent } from '../../notices/notices.component';
import { ViewSchoolComponent } from '../../view-school/view-school.component';
import { ViewWashInfoComponent } from '../../view-school/view-wash-info/view-wash-info.component';
import { RoomsComponent } from '../../rooms/rooms.component';
import{SectionsComponent}from'../../sections/sections.component';


const routes: Routes = [
 {
     path:'',
     component: SchoolDetailsComponent
 },
 {path:'marking-periods',component:MarkingPeriodsComponent},
 {path:'notices',component:NoticesComponent},
 {path:'add-school',component:AddSchoolComponent}, 
 {path:'view-school',component:ViewSchoolComponent},
 {path:'view-school/view-wash-info',component:ViewWashInfoComponent},
 {path:'rooms',component:RoomsComponent},
{path:'sections',component:SectionsComponent},
];



@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SchoolDetailsRoutingModule {
}
