import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthRepositoryService } from '../infrastructure/authRepository.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private authRepositoryService: AuthRepositoryService) {}

  login(username: string, password: string): Observable<any> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.authRepositoryService.login(username, password);
    return r;
  }
}
