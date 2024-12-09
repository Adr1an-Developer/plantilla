import { Routes } from '@angular/router';
import { UsersComponent } from './users.component';
import { UserDetailComponent } from './user-detail/user-detail.component';

export const route: Routes = [
  {
    path: '',
    component: UsersComponent,
    data: { breadcrumb: 'Lista de usuarios' },
  },
  {
    path: 'new',
    component: UserDetailComponent,
    data: { breadcrumb: 'Nuevo usuario' },
  },
  {
    path: ':id',
    component: UserDetailComponent,
    data: { breadcrumb: 'Detalle de usuario' },
  },
];
