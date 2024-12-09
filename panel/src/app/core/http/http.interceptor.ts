import { inject } from '@angular/core';
import {
  HttpEvent,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { environment } from '../../../environments/environment';

export const httpInterceptorFunctional: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  const clonedRequest = req.clone({
    setHeaders: token ? { Authorization: `Bearer ${token}` } : {},
    url: `${environment.backend.host}/${req.url}`,
  });

  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      // Aquí puedes manejar los errores como sea necesario
      console.error('Error status:', error.status);
      console.error('Error details:', error.message);

      // Puedes agregar lógica para redirigir al usuario, mostrar un mensaje, etc.
      // Por ejemplo, puedes redirigir al usuario al inicio de sesión si es un 401
      if (error.status === 401) {
        // Manejo de error específico para no autorizado
        authService.logout();
      }

      // Propaga el error para que el componente llamante también pueda manejarlo
      return throwError(() => error);
    })
  );
};
