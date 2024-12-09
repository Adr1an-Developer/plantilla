import { Routes } from '@angular/router';
import { DashboardComponent } from './ui/dashboard/dashboard.component';

export const route: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: DashboardComponent,
    data: { breadcrumb: 'Inicio' },
  },
];
