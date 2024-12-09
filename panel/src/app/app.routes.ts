import { Routes } from '@angular/router';
import { authGuard, Logged } from './core/guards/auth.guard';

export const routes: Routes = [
  // rutas publicas
  {
    path: '',
    redirectTo: 'dashboard', //auth
    pathMatch: 'full',
  },
  {
    path: 'auth',
    loadChildren: async () => import('./auth/auth.routes').then((m) => m.route),
    resolve: [Logged],
  },
  // rutas privadas
  {
    path: '',
    loadComponent: async () =>
      import('./shared/components/layout/layout.component').then(
        (c) => c.LayoutComponent
      ),
    children: [
      {
        path: '',
        loadChildren: async () =>
          import('./modules/modules.routes').then((c) => c.routeModules),
        canActivate: [authGuard],
      },
      {
        path: '**',
        redirectTo: 'dashboard',
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
  },
];
