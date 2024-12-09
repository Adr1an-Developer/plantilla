import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpApi } from '../../../core/http/http-api';

@Injectable({
  providedIn: 'root',
})
export class ProfileRepository {
  constructor(private http: HttpClient) {}

  getAllProfile(): Observable<any> {
    return this.http.get(`${HttpApi.profileList}`);
  }

  getProfile(id: string): Observable<any> {
    return this.http.get(`${HttpApi.getProfile}/${id}`);
  }

  addProfile(data: any): Observable<any> {
    return this.http.post(`${HttpApi.addProfile}`, data);
  }

  updateProfile(id: string, data: any): Observable<any> {
    return this.http.put(`${HttpApi.updateProfile}/${id}`, data);
  }

  deleteProfile(id: string): Observable<any> {
    return this.http.delete(`${HttpApi.deleteProfile}/${id}`);
  }
}
