import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

const registerByPages: number[] = [10, 25, 50, 100];

@Injectable({
  providedIn: 'root',
})
export class GlobalService {
  public titleApp: string = 'SoftGeek-Lotery';
  public versionApp: string = 'v1.0';
  public token: string = 'keySoftGeek-Lotery';
  public defaultPagesSelected: number = 50;

  constructor() {
    if (this.isProd() == false) {
      this.titleApp = `${this.titleApp} ${this.versionApp} (Dev)`;
    }
  }

  private isProd(): boolean {
    return environment.production;
  }

  public registerByPages(): number[] {
    return registerByPages;
  }
}
