import { ErrorHandler, inject, Injectable } from '@angular/core';
import { ToastMessageService } from '../services/toast.service';

@Injectable()
export class CustomErrorHandler implements ErrorHandler {
  toastService = inject(ToastMessageService);
  handleError(error: any) {
    const msg =
      typeof error === 'string' ? error : error.message ?? 'Erro desconhecido';
    this.toastService.mostrarErro(msg, false, 5000);
  }
}
