import { Routes } from '@angular/router';
import { AuthGuard } from './infrastructure/guard/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./pages/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: 'home',
    loadComponent: () =>
      import('./pages/home/home.component').then((m) => m.HomeComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'feature-toggles',
    loadComponent: () =>
      import('./pages/feature-toggles/feature-toggles.component').then(
        (m) => m.FeatureTogglesComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'ambientes',
    loadComponent: () =>
      import('./pages/ambientes/ambientes.component').then(
        (m) => m.AmbientesComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: '',
  },
];
