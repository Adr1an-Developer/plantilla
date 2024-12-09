import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResult, IResults } from '../../../core/infractucture/IData';
import { IAddUser, IUser } from '../domain/user.model';
import { UserRepositoryService } from '../infrastructure/userRepository.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private userRepository: UserRepositoryService) {}

  getAll(): Observable<IResults<IUser>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.userRepository.getAllUsers();
    return r;
  }

  getById(id: string): Observable<IResult<IUser>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.userRepository.getUser(id);
    return r;
  }

  Add(data: IAddUser): Observable<IResults<IUser>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.userRepository.addUser(data);
    return r;
  }

  Update(id: string, data: IUser): Observable<IResults<IUser>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.userRepository.updateUser(id, data);
    return r;
  }

  Delete(id: string): Observable<IResult<IUser>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.userRepository.deleteUser(id);
    return r;
  }
}
