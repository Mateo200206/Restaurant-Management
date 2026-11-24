import { Injectable, signal } from '@angular/core';
import { Category } from '../interfaces/category';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  readonly URL_BASE = "https://localhost:7074/api";
  categories = signal<Category[]>([]);

   async getCategoriesByRestaurant(restaurantId:number){
      const response = await fetch(this.URL_BASE+ "/restaurants/" + restaurantId + "/categories")
      if (!response.ok) return;
      const list : Category[] = await response.json();
          this.categories.set(list);
    }
}
