import { Injectable, signal } from '@angular/core';
import { Restaurant } from '../interfaces/restaurant';

@Injectable({
  providedIn: 'root',
})

export class RestaurantsService 
{ 
  readonly URL_BASE = "https://localhost:7074/api";
  restaurants = signal<Restaurant[]>([]);

  async getRestaurants()
  { 
    const response = await fetch(this.URL_BASE + "/restaurants");
    if (!response.ok) return;
    const list : Restaurant[] = await response.json();
    this.restaurants.set(list)
  }
  
  async getRestaurantById(id:number){
    const response = await fetch(this.URL_BASE + "/restaurants/"+id);
    if (!response.ok) return null;
    const restaurant : Restaurant = await response.json();
    return restaurant;
  }
}
