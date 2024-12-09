import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule, DOCUMENT } from '@angular/common';
import { GlobalService } from '../../../../core/services/global.service';
import { AuthService } from '../../../../core/services/auth.service';
import {
  MatToolbar,
  MatToolbarModule,
  MatToolbarRow,
} from '@angular/material/toolbar';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { ThemeService } from '../../../../core/services/theme.service';
import { MatButtonModule } from '@angular/material/button';
import { SidenavService } from '../../../../core/services/sidenav.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatDividerModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  title: string = '';
  isDarkThemeActivate = false;
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private themeService: ThemeService,
    public globalService: GlobalService,
    public sidenavService: SidenavService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.title = this.globalService.titleApp;
    this.themeService.darkThemeEnabled$.subscribe((isEnabled) => {
      this.isDarkThemeActivate = isEnabled;
      this.applyTheme();
    });
  }

  toggleTheme() {
    this.themeService.enableDarkTheme(!this.isDarkThemeActivate);
  }

  applyTheme() {
    if (this.isDarkThemeActivate) {
      document.body.classList.add('dark-mode');
    } else {
      document.body.classList.remove('dark-mode');
    }
  }
  isSidebarCollapsed = false;

  logout() {
    this.authService.logout();
  }
}
