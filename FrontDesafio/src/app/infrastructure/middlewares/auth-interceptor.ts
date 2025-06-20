import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  // Envia a requisição clonada e trata erros
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      // Aqui você pode lidar com erros, como redirecionar para a página de login se o token for inválido
      console.error('Erro na requisição:', error);
      const errorMsg = `${
        error.error.message ||
        'Falha na comunicação com o servidor. Tente novamente!'
      }`;
      console.log(error);
      return throwError(() => errorMsg);
    })
  );
};
