import { Injectable, inject } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private router = inject(Router);

  canActivate(): boolean {
    const token = localStorage.getItem('access_token');
    if (!token?.length) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
