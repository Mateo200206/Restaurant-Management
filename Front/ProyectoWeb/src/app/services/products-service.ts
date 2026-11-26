import { inject, Injectable, signal } from '@angular/core';
import { Product, ProductRequest } from '../interfaces/product';
import { AuthService } from './auth-service';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  readonly URL_BASE = "https://localhost:7074/api";
  products = signal<Product[]>([]);
  myProducts = signal<Product[]>([]);
  authService = inject(AuthService);

  async getProductsByRestaurant(restaurantId:number){
    const response = await fetch(this.URL_BASE+"/restaurants/"+ restaurantId +"/products")
    if (!response.ok) return;
    const list : Product[] = await response.json();
        this.products.set(list);
  }

  async getProductById(id:number){
    const response = await fetch(this.URL_BASE + "/products/"+id);
    if (!response.ok) return null;
    const product : Product = await response.json();
    return product;
  }

  async getMyProducts(){
    const response = await fetch(this.URL_BASE + "/products", {
      headers: { Authorization: "Bearer " + this.authService.token() }
    });
    if (!response.ok) return;
    const list : Product[] = await response.json();
      this.myProducts.set(list);
  }

  async deleteProduct(id:number){
    const response = await fetch(this.URL_BASE + "/products/"+ id, {
      method: "DELETE",
      headers: { Authorization: "Bearer " + this.authService.token() }
    });
    if (!response.ok) return false;
    this.myProducts.update(lista => lista.filter(p => p.id !== id));
    return true;
  }

  async createProduct(data: ProductRequest){
    const response = await fetch(this.URL_BASE + "/products", {
      method: "POST",
      headers:{ 
        "content-type": "application/json", 
        Authorization: "Bearer " + this.authService.token() 
      },
      body: JSON.stringify(data)        
    });
    if (!response.ok) return false;
    const newProduct : Product = await response.json();
    this.myProducts.update(lista => [...lista, newProduct]);
    return true;
  }

  async updateProduct(id:number, data:ProductRequest){
    const response = await fetch(this.URL_BASE + "/products/"+id, {
      method: "PUT",
      headers:{ 
        "content-type": "application/json", 
        Authorization: "Bearer " + this.authService.token() 
      },
      body: JSON.stringify(data)        
    });
    if (!response.ok)return false;
    await this.getMyProducts();
    return true;    
  }

  async setHappyHour(id: number, value: boolean){
    const response = await fetch(this.URL_BASE + "/products/"+id+"/happy-hour", {
      method: "PUT",
      headers:{ 
        "content-type": "application/json", 
        Authorization: "Bearer " + this.authService.token() 
      },
      body: JSON.stringify({ hasHappyHour: value })
    });
    if (!response.ok)return false;
    await this.getMyProducts();
    return true;
  }

  async setDiscount(id: number, percentage: number){
    const response = await fetch(this.URL_BASE + "/products/"+id+"/discount", {
      method: "PUT",
      headers:{ 
        "content-type": "application/json", 
        Authorization: "Bearer " + this.authService.token() 
      },
      body: JSON.stringify({ discountPercentage: percentage })
    });
    if (!response.ok)return false;
    await this.getMyProducts();
    return true;
  }

  async exportMenu() {
    const response = await fetch(this.URL_BASE + "/products/export", {
      headers: { Authorization: "Bearer " + this.authService.token() }
    });
    if (!response.ok) return false;

    const file = await response.blob();  //lee la respuesta como archivo, no como texto ni JSON. "Blob" = un bloque de datos
    const url = URL.createObjectURL(file); //le da al archivo una dirección temporal dentro del navegador

    const link = document.createElement("a"); //crea un link invisible
    link.href = url;
    link.download = "menu.csv"; //le dice al navegador "descargá con este nombre" en lugar de abrirlo.
    link.click(); //hace clic solo, por código.

    URL.revokeObjectURL(url); //libera esa dirección temporal, para no gastar memoria.
    return true;
  }
}
