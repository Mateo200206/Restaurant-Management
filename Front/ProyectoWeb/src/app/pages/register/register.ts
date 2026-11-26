import { Component, signal, inject } from '@angular/core';
import { form, email, FormField, required, validate } from '@angular/forms/signals';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { RegisterFormModel, RegisterRequest } from '../../interfaces/auth';

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

  registerModel = signal<RegisterFormModel>({
    email: "",
    password: "",
    password2: "",
    name: "",
    description: "",
    imageUrl: "",
  });
  registerForm = form(this.registerModel, (schema) => {
    required(schema.email);
    email(schema.email);
    required(schema.password);
    required(schema.name);
    // La contraseña repetida tiene que ser igual a la primera
    validate(schema.password2, ({ value }) => {
      if (value() === this.registerModel().password) return null;
      return { kind: "passwordMatch", message: "Las contraseñas no coinciden" };
    });
  });

  async onRegister(event: Event) {
    this.registerError.set(false)
    event.preventDefault();
    // Armo lo que se manda al back, sin la contraseña repetida
    const data: RegisterRequest = {
      email: this.registerModel().email,
      password: this.registerModel().password,
      name: this.registerModel().name,
      description: this.registerModel().description,
      imageUrl: this.registerModel().imageUrl
    };
    const resultForm = await this.authService.register(data);
    if (resultForm) {
      this.router.navigate(["/login"]);
    }else{
      this.registerError.set(true);
    }
  }
}
