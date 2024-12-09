import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResult, IResults } from '../../../core/infractucture/IData';
import { IMenu, IAddMenu } from '../domain/menu.model';
import { MenuRepository } from '../infrastructure/menuRepository.service';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  constructor(private menuRepository: MenuRepository) {}

  getAll(): Observable<IResults<IMenu>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.menuRepository.getAllMenu();
    return r;
  }

  getById(id: string): Observable<IResult<IMenu>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.menuRepository.getMenu(id);
    return r;
  }

  Add(data: IAddMenu): Observable<IResults<IMenu>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.menuRepository.addMenu(data);
    return r;
  }

  Update(id: string, data: IMenu): Observable<IResults<IMenu>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.menuRepository.updateMenu(id, data);
    return r;
  }

  Delete(id: string): Observable<IResult<IMenu>> {
    // Aquí se podría agregar lógica adicional antes de llamar al servicio API
    const r = this.menuRepository.deleteMenu(id);
    return r;
  }
}
