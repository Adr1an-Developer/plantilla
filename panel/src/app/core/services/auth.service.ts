import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalService } from './global.service';
import { IUserLogged } from '../../auth/domain/IUserLogged';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private router: Router, private globalService: GlobalService) {}

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  setToken(token: string): void {
    localStorage.setItem(this.globalService.token, token ?? '');
  }

  getToken(): string | null {
    return localStorage.getItem(this.globalService.token);
  }

  getUserLogged(): IUserLogged {
    var token: string | null = this.getToken();
    var obj = {} as IUserLogged;
    try {
      obj = jwtDecode(token ?? '');

      var isFirstLogin: boolean =
        obj.isFirstLogin.toString() == 'False' ? false : true;
      var result: IUserLogged = {
        UserId: obj.UserId,
        FullName: obj.FullName,
        TypeUser: obj.TypeUser,
        Email: obj.Email,
        Username: obj.Username,
        Name: obj.Name,
        isFirstLogin: isFirstLogin,
      };
      return result;
    } catch (error) {
      return obj;
    }
  }

  logout(): void {
    localStorage.removeItem(this.globalService.token);
    this.router.navigate(['/auth']);
  }
}
