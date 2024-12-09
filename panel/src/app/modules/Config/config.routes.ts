import { Routes } from '@angular/router';

export const route: Routes = [
  {
    path: 'profiles',
    loadChildren: () =>
      import('./ui/profiles/profiles.routes').then((m) => m.route),
    data: { breadcrumb: 'Perfiles' },
  },
  {
    path: 'users',
    loadChildren: () => import('./ui/users/user.routes').then((m) => m.route),
    data: { breadcrumb: 'Usuarios' },
  },
  {
    path: 'menu',
    loadChildren: () => import('./ui/menu/menu.routes').then((m) => m.route),
    data: { breadcrumb: 'Menu' },
  },
  {
    path: 'access',
    loadChildren: () =>
      import('./ui/access/access.routes').then((m) => m.route),
    data: { breadcrumb: 'Accesos' },
  },
];
