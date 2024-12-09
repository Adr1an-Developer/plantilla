import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpApi } from '../../../core/http/http-api';

@Injectable({
  providedIn: 'root',
})
export class AccessRepository {
  constructor(private http: HttpClient) {}

  getAllAccess(): Observable<any> {
    return this.http.get(`${HttpApi.MenuPermissionList}`);
  }

  getAccess(id: string): Observable<any> {
    return this.http.get(`${HttpApi.getMenuPermission}/${id}`);
  }

  getAccessByProfile(id: string): Observable<any> {
    return this.http.get(`${HttpApi.MenuPermissionListByProfile}/${id}`);
  }
  addAccess(data: any): Observable<any> {
    return this.http.post(`${HttpApi.addMenuPermission}`, data);
  }

  updateAccess(id: string, data: any): Observable<any> {
    return this.http.put(`${HttpApi.updateMenuPermission}/${id}`, data);
  }

  deleteAccess(id: string): Observable<any> {
    return this.http.delete(`${HttpApi.deleteMenuPermission}/${id}`);
  }
}
