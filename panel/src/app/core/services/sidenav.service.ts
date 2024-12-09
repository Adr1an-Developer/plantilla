import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SidenavService {
  // Define un Signal para controlar el estado del sidenav (abierto o cerrado)
  private isOpenSignal = signal(true);

  // Método para obtener el estado actual del sidenav
  get isOpen() {
    return this.isOpenSignal();
  }

  // Método para abrir el sidenav
  open() {
    this.isOpenSignal.set(true);
  }

  // Método para cerrar el sidenav
  close() {
    this.isOpenSignal.set(false);
  }

  // Método para alternar el estado del sidenav
  toggle() {
    this.isOpenSignal.update((open) => !open);
  }
}
