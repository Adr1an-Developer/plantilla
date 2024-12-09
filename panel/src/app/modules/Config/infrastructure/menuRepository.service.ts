import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpApi } from '../../../core/http/http-api';

@Injectable({
  providedIn: 'root',
})
export class MenuRepository {
  constructor(private http: HttpClient) {}

  getAllMenu(): Observable<any> {
    return this.http.get(`${HttpApi.MenuList}`);
  }

  getMenu(id: string): Observable<any> {
    return this.http.get(`${HttpApi.getMenu}/${id}`);
  }

  addMenu(data: any): Observable<any> {
    return this.http.post(`${HttpApi.addMenu}`, data);
  }

  updateMenu(id: string, data: any): Observable<any> {
    return this.http.put(`${HttpApi.updateMenu}/${id}`, data);
  }

  deleteMenu(id: string): Observable<any> {
    return this.http.delete(`${HttpApi.deleteMenu}/${id}`);
  }
}
