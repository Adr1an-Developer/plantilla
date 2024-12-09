import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { LoginService } from '../../application/login.service';
import { finalize, Subscription } from 'rxjs';
import { LoadingBackdropService } from '../../../core/services/loading-backdrop.service';
import { GlobalService } from '../../../core/services/global.service';
import { NotificationService } from '../../../core/services/notification.service';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { DOCUMENT } from '@angular/common';
import { ThemeService } from '../../../core/services/theme.service';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatIcon,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  titleApp: string = '';
  isDarkThemeActivate = false;
  /**
   *
   */
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private authService: AuthService,
    private loginService: LoginService,
    private router: Router,
    private loadingBackdropService: LoadingBackdropService,
    public formBuilder: FormBuilder,
    public notificationService: NotificationService,
    private themeService: ThemeService,
    private globalService: GlobalService
  ) {
    this.initFormBuilder();
    this.titleApp = globalService.titleApp;
    this.isDarkThemeActivate = localStorage.getItem('themes') == 'dark';
  }
  async ngOnInit() {
    this.themeService.darkThemeEnabled$.subscribe((isEnabled) => {
      this.isDarkThemeActivate = isEnabled;
      this.applyTheme();
    });
  }
  private initFormBuilder() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
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

  async login() {
    this.loadingBackdropService.show();
    await this.loginService
      .login(this.loginForm.value.email, this.loginForm.value.password)
      .subscribe({
        next: (resp) => {
          this.authService.setToken(resp.result);
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages,
            time: 5000,
          });
          this.router.navigate(['/']);
        },
        error: (e) => {
          console.error(e);

          let err = e.error;
          this.notificationService.add({
            type: 'Error',
            title: 'Error Grave',
            message: e.message,
            time: 10000,
          });
          this.authService.logout();
          this.loadingBackdropService.hide();
        },
        complete: () => {
          this.loadingBackdropService.hide();
        },
      });
  }
}
