import { Component, signal, inject } from '@angular/core';
import { LoginRequest } from '../../interfaces/auth';
import { form, required, email, FormField } from '@angular/forms/signals';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login',
  imports: [FormField],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  authService = inject(AuthService);
  router = inject(Router);
  loginError = signal(false);

  loginModel = signal<LoginRequest>({
    email: "",
    password: ""
  });
  loginForm = form(this.loginModel, (schema) => {
    required(schema.email);
    email(schema.email);
    required(schema.password);
  });

  async onLogin(event: Event) {
    this.loginError.set(false)
    event.preventDefault();
    const resultForm = await this.authService.login(this.loginModel());
    if (resultForm) {
      this.router.navigate(["/"]);
    }else{
      this.loginError.set(true);
    }
  }

}
