import { Routes } from '@angular/router';
import { MenuComponent } from './menu.component';
import { MenuDetailComponent } from './menu-detail/menu-detail.component';

export const route: Routes = [
  {
    path: '',
    component: MenuComponent,
    data: { breadcrumb: 'Lista de Menus' },
  },
  {
    path: 'new',
    component: MenuDetailComponent,
    data: { breadcrumb: 'Nuevo Menu' },
  },
  {
    path: ':id',
    component: MenuDetailComponent,
    data: { breadcrumb: 'Detalle de Menu' },
  },
];
