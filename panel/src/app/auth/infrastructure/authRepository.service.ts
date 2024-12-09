import { Injectable } from '@angular/core';
import { HttpApi } from '../../core/http/http-api';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthRepositoryService {
  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    var params: any = {
      userName: username,
      password: password,
    };
    return this.http.post(`${HttpApi.oauthLogin}`, params);
  }
}
