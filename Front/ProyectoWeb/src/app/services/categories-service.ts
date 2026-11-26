import { inject, Injectable, signal } from '@angular/core';
import { Category } from '../interfaces/category';
import { AuthService } from './auth-service';
import { ProductsService } from './products-service';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  readonly URL_BASE = "https://localhost:7074/api";
  categories = signal<Category[]>([]);
  myCategories = signal<Category[]>([]);
  authService = inject(AuthService)
  productsService = inject(ProductsService)

  async getCategoriesByRestaurant(restaurantId:number){
    const response = await fetch(this.URL_BASE+ "/restaurants/" + restaurantId + "/categories")
    if (!response.ok) return;
    const list : Category[] = await response.json();  
    this.categories.set(list);
  }

  async getMyCategories(){
      const response = await fetch(this.URL_BASE + "/categories", {
        headers: { Authorization: "Bearer " + this.authService.token() }
      });
      if (!response.ok) return;
      const list : Category[] = await response.json();
        this.myCategories.set(list);
    }
  
    async deleteCategory(id:number){
      const response = await fetch(this.URL_BASE + "/categories/"+ id, {
        method: "DELETE",
        headers: { Authorization: "Bearer " + this.authService.token() }
      });
      if (!response.ok) return false;
      this.myCategories.update(lista => lista.filter(p => p.id !== id));
      await this.productsService.getMyProducts();
      return true;
    }

  async createCategory(name:string){
    const response = await fetch(this.URL_BASE + "/categories", {
      method: "POST",
      headers:{ 
        "content-type": "application/json", 
        Authorization: "Bearer " + this.authService.token() 
      },
      body: JSON.stringify({name: name})        
    });
    if (!response.ok) return false;
      const newCategory : Category = await response.json();
      this.myCategories.update(lista => [...lista, newCategory]);
      return true;
  }

  async updateCategory(id: number, name: string){
    const response = await fetch(this.URL_BASE + "/categories/"+id, {
      method: "PUT",
      headers:{ 
        "content-type": "application/json", 
        Authorization: "Bearer " + this.authService.token() 
      },
      body: JSON.stringify({name: name})        
    });
    if (!response.ok) return false;
    this.myCategories.update(lista =>
      lista.map(c => c.id === id ? { ...c, name: name } : c)
    );
    return true;
  }
}
