import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthToken } from '../../domain/interfaces/auth';
import { BaseService } from '../../infrastructure/services/base.service';

@Injectable({ providedIn: 'root' })
export default class LoginService extends BaseService {
  constructor(public override http: HttpClient) {
    super(http);
  }

  login(): Observable<AuthToken> {
    return this.post<AuthToken>('/auth/token', {});
  }
}
