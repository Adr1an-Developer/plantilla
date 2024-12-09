import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpApi } from '../../../core/http/http-api';

@Injectable({
  providedIn: 'root',
})
export class UserRepositoryService {
  constructor(private http: HttpClient) {}

  getAllUsers(): Observable<any> {
    return this.http.get(`${HttpApi.UsersList}`);
  }
  getUser(id: string): Observable<any> {
    return this.http.get(`${HttpApi.getUser}/${id}`);
  }

  addUser(data: any): Observable<any> {
    return this.http.post(`${HttpApi.addUser}`, data);
  }

  updateUser(id: string, data: any): Observable<any> {
    return this.http.put(`${HttpApi.updateUser}/${id}`, data);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete(`${HttpApi.deleteUser}/${id}`);
  }
}
