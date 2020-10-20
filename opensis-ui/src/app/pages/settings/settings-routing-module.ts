import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SettingsComponent } from './settings.component';
import { SchoolSettingsComponent } from '../settings/school-settings/school-settings.component';
import { StudentSettingsComponent } from '../settings/student-settings/student-settings.component';
import { GradeLevelsComponent } from '../school/grade-levels/grade-levels.component';
import { SectionsComponent } from '../school/sections/sections.component';
import { RoomsComponent } from '../school/rooms/rooms.component';
import {EnrollmentCodesComponent} from '../student/enrollment-codes/enrollment-codes.component';
import {AttendanceSettingsComponent} from '../settings/attendance-settings/attendance-settings.component';
const routes: Routes = [
  {
    path: '',
    component: SettingsComponent
  },
  {
    path:'school-settings',
    component:SchoolSettingsComponent,
    children:[
    
      {path:'',component:GradeLevelsComponent},
      {path:'',component:SectionsComponent},
      {path:'',component:RoomsComponent},
    
  ]},
  {
    path:'student-settings',
    component:StudentSettingsComponent,
    children:[     
      {path:'',component:EnrollmentCodesComponent},
    ]
  }, 
  {
    path:'attendance-settings',
    component:AttendanceSettingsComponent,
    children:[     
      
    ]
  }  
];



@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule {
}
