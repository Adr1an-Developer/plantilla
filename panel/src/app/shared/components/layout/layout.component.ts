import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { GlobalService } from '../../../core/services/global.service';
import { HeaderComponent } from './header/header.component';
import { BreadcrumbsComponent } from '../breadcrumbs/breadcrumbs.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { FooterComponent } from './footer/footer.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    SidenavComponent,
    FooterComponent,
    BreadcrumbsComponent,
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss',
})
export class LayoutComponent implements OnInit {
  title: string = '';
  themeActivo: string = '';
  constructor(public globalService: GlobalService) {}

  ngOnInit(): void {
    // this.title = `${environment.sistem.title} - ${environment.sistem.version}`;

    this.title = `t`;
  }
}
