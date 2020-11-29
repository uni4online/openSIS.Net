import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StaffinfoComponent } from './staffinfo/staffinfo.component';
//import { ParentGeneralinfoComponent } from './add-student/student-generalinfo/student-generalinfo.component';


const routes: Routes = [
  {
      path:'',
      component: StaffinfoComponent
  },  
  // {
  //   path:'parent-generalinfo',
  //   component:ParentGeneralinfoComponent
  // } 
];



@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StaffRoutingModule {
}
