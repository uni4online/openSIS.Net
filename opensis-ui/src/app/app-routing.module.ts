import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomLayoutComponent } from './custom-layout/custom-layout.component';
import { 
  AuthGuard as AuthGuard } from '../app/common/auth.guard';
const routes: Routes = [
  // { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: '',
    loadChildren: () => import('./pages/auth/login/login.module').then(m => m.LoginModule),
  },
  {
    path: ':id',
    loadChildren: () => import('./pages/auth/login/login.module').then(m => m.LoginModule),
  },  
  {
    path: 'school',
    component: CustomLayoutComponent,
    children: [
      // {
      //   path: 'dashboards',
      //   redirectTo: '/'
      // },
      {
        path: 'dashboards',
        loadChildren: () => import('./pages/dashboards/dashboard-analytics/dashboard-analytics.module').then(m => m.DashboardAnalyticsModule),
        canActivate: [AuthGuard]
      },
      {
        path: '',
        children: [
          {
            path: 'schoolinfo',
            loadChildren: () => import('./pages/school/school-details/school-details/school-details.module').then(m => m.SchoolDetailsModule),
            canActivate: [AuthGuard]
            // data: {
            //   toolbarShadowEnabled: true,
            //   scrollDisabled: true
            // }
          }
        ]

      }
    ]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    // preloadingStrategy: PreloadAllModules,
    scrollPositionRestoration: 'enabled',
    relativeLinkResolution: 'corrected',
    anchorScrolling: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
