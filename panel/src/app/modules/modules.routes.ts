import { Routes } from '@angular/router';

export const routeModules: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./Dashboards/dashboards.routes').then((m) => m.route),
    data: { breadcrumb: 'Dashboard' },
  },
  {
    path: 'config',
    loadChildren: () => import('./Config/config.routes').then((m) => m.route),
    data: { breadcrumb: 'Configuración' },
  },
];
