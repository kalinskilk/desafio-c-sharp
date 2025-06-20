import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import Toast from '../../domain/interfaces/toast';

@Injectable({
  providedIn: 'root',
})
export class ToastMessageService {
  constructor(private messageService: MessageService) {}

  mostrar(toast: Toast) {
    this.messageService.add({
      severity: toast.severity,
      summary: toast.summary,
      detail: toast.detail,
    });
  }

  mostrarSucesso(detail: string) {
    this.messageService.add({
      severity: 'success',
      summary: 'Sucesso',
      detail: detail,
    });
  }

  mostrarAviso(detail: string, isThrow = false) {
    this.messageService.add({
      severity: 'warn',
      summary: 'Atenção',
      detail: detail,
      styleClass: 'my-toast',
    });

    if (isThrow) throw new Error(detail);
  }

  mostrarErro(detail: string, isThrow = true, life = 3000) {
    this.messageService.add({
      severity: 'error',
      summary: 'Erro',
      detail: detail,
      life,
    });

    if (isThrow) {
      throw new Error(detail);
    }
  }

  mostrarInfo(detail: string) {
    this.messageService.add({
      severity: 'info',
      summary: 'Info',
      detail: detail,
    });
  }
}
