import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IBaseService } from '../../domain/interfaces/base.interface';

// const headers = new HttpHeaders({
//   'Cache-Control': 'no-cache, no-store, must-revalidate, post-check=0, pre-check=0',
//   Pragma: 'no-cache',
//   Expires: '0',
//   Authorization: `Bearer ${localStorage.getItem('access_token')}`,
// });

export abstract class BaseService implements IBaseService {
  private url = environment.apiUrl;

  constructor(public http: HttpClient) {}

  private getHeaders(customHeaders?: HttpHeaders): HttpHeaders {
    const token = localStorage.getItem('access_token') || '';
    if (token) {
      return (
        customHeaders ||
        new HttpHeaders({
          Authorization: `Bearer ${token}`,
        })
      );
    }
    return new HttpHeaders({});
  }

  public getUrl(): string {
    return this.url;
  }

  get<T>(
    endpoint: string,
    options?: {
      params?: HttpParams | { [param: string]: string | string[] };
      headers?: HttpHeaders;
    }
  ): Observable<T> {
    const url = `${this.url}${endpoint}`;
    return this.http.get<T>(url, { headers: this.getHeaders(), ...options });
  }

  post<T>(
    endpoint: string,
    body?: any,
    options?: {
      params?: HttpParams | { [param: string]: string | string[] };
      headers?: HttpHeaders;
    }
  ): Observable<any> {
    const url = `${this.url}${endpoint}`;
    return this.http.post<T>(url, body, {
      headers: this.getHeaders(),
      ...options,
    });
  }

  postBlob(
    endpoint: string,
    body?: any,
    options?: {
      params?: HttpParams | { [param: string]: string | string[] };
      headers?: HttpHeaders;
    }
  ): Observable<any> {
    const url = `${this.url}${endpoint}`;
    return this.http.post(url, body, {
      headers: this.getHeaders(),
      ...options,
      responseType: 'blob',
    });
  }

  delete<T>(
    endpoint: string,
    options?: {
      params?: HttpParams | { [param: string]: string | string[] };
      headers?: HttpHeaders;
    }
  ): Observable<any> {
    const url = `${this.url}${endpoint}`;
    return this.http.delete<T>(url, { headers: this.getHeaders(), ...options });
  }

  put<T>(
    endpoint: string,
    body: any,
    options?: {
      params?: HttpParams | { [param: string]: string | string[] };
      headers?: HttpHeaders;
    }
  ): Observable<any> {
    const url = `${this.url}${endpoint}`;
    return this.http.put<T>(url, body, {
      headers: this.getHeaders(),
      ...options,
    });
  }
}
