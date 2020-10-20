import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentComponent } from './studentinfo/student.component';
import { StudentGeneralinfoComponent } from './add-student/student-generalinfo/student-generalinfo.component';


const routes: Routes = [
  {
      path:'',
      component: StudentComponent
  },
  {path:'student-generalinfo',component:StudentGeneralinfoComponent} 
];



@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule {
}
