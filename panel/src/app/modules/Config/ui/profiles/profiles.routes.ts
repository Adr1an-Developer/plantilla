import { Route, Routes } from '@angular/router';
import { ProfilesComponent } from './profiles.component';
import { ProfilesDetailComponent } from './profiles-detail/profiles-detail.component';

export const route: Routes = [
  {
    path: '',
    component: ProfilesComponent,
    data: { breadcrumb: 'Lista de Perfiles' },
  },
  {
    path: 'new',
    component: ProfilesDetailComponent,
    data: { breadcrumb: 'Nuevo Perfil' },
  },
  {
    path: ':id',
    component: ProfilesDetailComponent,
    data: { breadcrumb: 'Modificar Perfil' },
  },
];
