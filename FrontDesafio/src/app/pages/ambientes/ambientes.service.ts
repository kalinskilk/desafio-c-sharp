import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  IAmbientes,
  ICreateAmbientes,
} from '../../domain/interfaces/ambientes';
import { BaseService } from '../../infrastructure/services/base.service';

@Injectable({
  providedIn: 'root',
})
export class AmbientesService extends BaseService {
  constructor(public override http: HttpClient) {
    super(http);
  }

  getAmbientes(): Observable<IAmbientes[]> {
    return this.get('/ambiente');
  }

  saveAmbientes(ambiente: ICreateAmbientes): Observable<IAmbientes> {
    return this.post('/ambiente', ambiente);
  }
}
