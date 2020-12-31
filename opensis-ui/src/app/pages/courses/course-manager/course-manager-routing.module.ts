import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CourseManagerComponent } from './course-manager.component';



const routes: Routes = [
  {
    path: '',
    component: CourseManagerComponent
  },
  
];



@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CourseManagerRoutingModule {
}
