import { Component, inject,signal } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { RestaurantsService } from '../../services/restaurants-service';
import { Router } from '@angular/router';
import { UpdateAccountRequest } from '../../interfaces/auth';
import { required, form, FormField, validate } from '@angular/forms/signals';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-account',
  imports: [FormField],
  templateUrl: './account.html',
  styleUrl: './account.scss',
})
export class Account {
  authService = inject(AuthService);
  restaurantService = inject(RestaurantsService);
  router = inject(Router)

  accountModel = signal<UpdateAccountRequest>({
    name: "",
    description: "",
    imageUrl: "",
    password: "",
    currentPassword: ""
  });
  accountForm = form(this.accountModel, (schema) => {
    required(schema.name);
    // Si escribe una contraseña nueva, tiene que poner la actual
    validate(schema.currentPassword, ({ value }) => {
      if (this.accountModel().password !== "" && value() === "") {
        return { kind: "required", message: "Para cambiar la contraseña, poné la actual" };
      }
      return null;
    });
  });

  async ngOnInit(){
    const id = this.authService.getRestaurantId()
    if (id){
      const restaurant = await this.restaurantService.getRestaurantById(id);
      if(restaurant){
        this.accountModel.set({
          name: restaurant.name,
          description: restaurant.description ?? "",
          imageUrl: restaurant.imageUrl ?? "",
          password: "",
          currentPassword: ""
        });
      }
    }
  }
 
  async onSubmit(event: Event) {
    event.preventDefault();
    const status = await this.authService.updateAccount(this.accountModel());

    if (status === 204) {
      // Limpio los campos de contraseña para que no queden escritos
      this.accountModel.update(datos => ({ ...datos, password: "", currentPassword: "" }));
      Swal.fire({
        toast: true,
        position: "top-end",
        icon: "success",
        title: "Datos actualizados",
        showConfirmButton: false,
        timer: 2000
      });
    } else if (status === 400) {
      Swal.fire("Error", "La contraseña actual es incorrecta", "error");
    } else {
      Swal.fire("Error", "No se pudieron guardar los cambios", "error");
    }
  }

  async onDeleteAccount() {
    const result = await Swal.fire({
      title: "¿Borrar tu cuenta?",
      text: "Se borran también todas tus categorías y productos. No se puede deshacer.",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Borrar cuenta",
      cancelButtonText: "Cancelar"
    });
    if (!result.isConfirmed) return;

    const ok = await this.authService.deleteAccount();
    if (ok) {
      this.router.navigate(["/"]);
    } else {
      Swal.fire("Error", "No se pudo borrar la cuenta", "error");
    }
  }
}
