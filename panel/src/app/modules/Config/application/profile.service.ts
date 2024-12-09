import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResult, IResults } from '../../../core/infractucture/IData';
import { ProfileRepository } from '../infrastructure/profileRepository.service';
import { IAddProfile, IProfile } from '../domain/profile.model';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  constructor(private profileRepository: ProfileRepository) {}

  getAll(): Observable<IResults<IProfile>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.profileRepository.getAllProfile();
    return r;
  }

  getById(id: string): Observable<IResult<IProfile>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.profileRepository.getProfile(id);
    return r;
  }

  Add(data: IAddProfile): Observable<IResults<IProfile>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.profileRepository.addProfile(data);
    return r;
  }

  Update(id: string, data: IProfile): Observable<IResults<IProfile>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.profileRepository.updateProfile(id, data);
    return r;
  }

  Delete(id: string): Observable<IResult<IProfile>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.profileRepository.deleteProfile(id);
    return r;
  }
}
