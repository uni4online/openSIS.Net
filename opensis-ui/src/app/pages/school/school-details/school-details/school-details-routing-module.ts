import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SchoolDetailsComponent } from './school-details.component';
import { AddSchoolComponent } from '../../add-school/add-school.component';
import { MarkingPeriodsComponent } from '../../marking-periods/marking-periods.component';
import { NoticesComponent } from '../../notices/notices.component';
import { CalendarComponent } from '../../calendar/calendar.component';

//import { RoomsComponent } from '../../rooms/rooms.component';
//import{SectionsComponent}from'../../sections/sections.component';
//import { GradeLevelsComponent } from '../../grade-levels/grade-levels.component';


const routes: Routes = [
 {
     path:'',
     component: SchoolDetailsComponent
 },
 {path:'marking-periods',component:MarkingPeriodsComponent},
 {path:'notices',component:NoticesComponent}, 
 {path:'add-school',component:AddSchoolComponent},
 {path:'schoolcalendars',component: CalendarComponent}
 //{path:'rooms',component:RoomsComponent},
 //{path:'sections',component:SectionsComponent},
 //{path:'grade-levels',component:GradeLevelsComponent},
];



@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SchoolDetailsRoutingModule {
}
