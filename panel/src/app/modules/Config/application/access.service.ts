import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResult, IResults } from '../../../core/infractucture/IData';
import { AccessRepository } from '../infrastructure/accessRepository.service';
import { IAccess, IAddAccess } from '../domain/access.model';

@Injectable({
  providedIn: 'root',
})
export class AccessService {
  constructor(private accessRepository: AccessRepository) {}

  getAll(): Observable<IResults<IAccess>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.accessRepository.getAllAccess();
    return r;
  }

  getById(id: string): Observable<IResult<IAccess>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.accessRepository.getAccess(id);
    return r;
  }
  getByProfileId(id: string): Observable<IResult<IAccess>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.accessRepository.getAccessByProfile(id);
    return r;
  }
  Add(data: IAddAccess): Observable<IResults<IAccess>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.accessRepository.addAccess(data);
    return r;
  }

  Update(id: string, data: IAccess): Observable<IResults<IAccess>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.accessRepository.updateAccess(id, data);
    return r;
  }

  Delete(id: string): Observable<IResult<IAccess>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.accessRepository.deleteAccess(id);
    return r;
  }
}
