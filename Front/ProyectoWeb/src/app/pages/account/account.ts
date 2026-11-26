import { Component, inject,signal } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { RestaurantsService } from '../../services/restaurants-service';
import { Router } from '@angular/router';
import { UpdateAccountRequest } from '../../interfaces/auth';
import { required,form, FormField } from '@angular/forms/signals';

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
    password: ""
  });
  accountForm = form(this.accountModel, (schema) => {
    required(schema.name);
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
          password: ""
        });
      }
    }
  }
  
  async onSubmit(event: Event) {
    event.preventDefault();
    const ok = await this.authService.updateAccount(this.accountModel());
    if (ok) {
      alert("Datos actualizados");
    } else {
      alert("No se pudieron guardar los cambios");
    }
  }

  async onDeleteAccount() {
    if (!confirm("¿Seguro que querés borrar tu cuenta? Se borran también todas tus categorías y productos.")) return;
    const ok = await this.authService.deleteAccount();
    if (ok) {
      this.router.navigate(["/"]);
    }
  }
}
