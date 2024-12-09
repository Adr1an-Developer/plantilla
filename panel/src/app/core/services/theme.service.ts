import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private darkThemeEnabled = new BehaviorSubject<boolean>(
    this.getInitialTheme()
  );
  darkThemeEnabled$ = this.darkThemeEnabled.asObservable();

  constructor() {}

  enableDarkTheme(enabled: boolean): void {
    this.darkThemeEnabled.next(enabled);
    localStorage.setItem('darkTheme', JSON.stringify(enabled));
  }

  private getInitialTheme(): boolean {
    const savedTheme = localStorage.getItem('darkTheme');
    return savedTheme !== null ? JSON.parse(savedTheme) : false;
  }
}
