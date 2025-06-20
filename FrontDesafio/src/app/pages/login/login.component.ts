import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { first } from 'rxjs';
import LoginService from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    InputTextModule,
    ButtonModule,
    CardModule,
    CheckboxModule,
    FormsModule,
  ],
})
export class LoginComponent {
  constructor(private loginService: LoginService, private router: Router) {}
  onLogin() {
    this.loginService
      .login()
      .pipe(first())
      .subscribe((res) => {
        if (res.token) {
          localStorage.setItem('access_token', res.token);
          this.router.navigate(['/home']);
        }
      });
  }
}
