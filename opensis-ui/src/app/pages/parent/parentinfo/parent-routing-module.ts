import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ParentinfoComponent } from './parentinfo.component';
//import { AddStudentComponent } from '../add-student/add-student.component';


const routes: Routes = [
 {
     path:'',
     component: ParentinfoComponent
 },
 //{path:'student-generalinfo',component:AddStudentComponent},
 
];



@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ParentRoutingModule {
}
