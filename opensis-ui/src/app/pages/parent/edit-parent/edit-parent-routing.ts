import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditParentComponent } from './edit-parent.component';

const routes: Routes = [
    {
        path:'',
        component: EditParentComponent
    } 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EditParentRoutingModule {
}
