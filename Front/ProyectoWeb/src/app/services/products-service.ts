import { Injectable, signal } from '@angular/core';
import { Product } from '../interfaces/product';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  readonly URL_BASE = "https://localhost:7074/api";
  products = signal<Product[]>([]);

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
  
}
