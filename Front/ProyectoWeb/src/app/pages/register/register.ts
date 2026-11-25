import { Component, signal, inject } from '@angular/core';
import { form,email,FormField, required } from '@angular/forms/signals';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { RegisterRequest } from '../../interfaces/auth';

@Component({
  selector: 'app-register',
  imports: [FormField, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  router = inject(Router);
  registerError = signal(false);
  authService = inject(AuthService);

  registerModel = signal<RegisterRequest>({
    email: "",
    password: "",
    name: "",
    description: "",
    imageUrl: "",
  });
  registerForm = form(this.registerModel, (schema) => {
    required(schema.email);
    email(schema.email);
    required(schema.password);
    required(schema.name)
  });

  async onRegister(event: Event) {
    this.registerError.set(false)
    event.preventDefault();
    const resultForm = await this.authService.register(this.registerModel());
    if (resultForm) {
      this.router.navigate(["/login"]);
    }else{
      this.registerError.set(true);
    }
  }
}
