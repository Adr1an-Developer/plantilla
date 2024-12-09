import { Routes } from '@angular/router';
import { AccessComponent } from './access.component';
import { AccessDetailComponent } from './access-detail/access-detail.component';

export const route: Routes = [
  {
    path: '',
    component: AccessComponent,
    data: { breadcrumb: 'Lista de Permisos de Acceso' },
  },
  {
    path: 'new',
    component: AccessDetailComponent,
    data: { breadcrumb: 'Nuevo Acceso' },
  },
  {
    path: ':id',
    component: AccessDetailComponent,
    data: { breadcrumb: 'Modificar Accesos' },
  },
];
