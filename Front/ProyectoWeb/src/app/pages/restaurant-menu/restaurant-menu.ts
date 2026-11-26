import { Component, input, OnInit, signal, inject } from '@angular/core';
import { Restaurant } from '../../interfaces/restaurant';
import { RestaurantsService } from '../../services/restaurants-service';
import { ProductsService } from '../../services/products-service';
import { CategoriesService } from '../../services/categories-service';
import { RouterLink } from '@angular/router';
import { Spinner } from '../../components/spinner/spinner';

@Component({
  selector: 'app-restaurant-menu',
  imports: [RouterLink, Spinner],
  templateUrl: './restaurant-menu.html',
  styleUrl: './restaurant-menu.scss',
})
export class RestaurantMenu implements OnInit{
  id = input.required<number>();
  restaurant = signal<Restaurant | null>(null);
  restaurantsService = inject(RestaurantsService);
  productsService = inject(ProductsService);
  categoriesService = inject(CategoriesService);
  selectedCategoryId = signal<number | null>(null);
  loading = signal(true);

  async ngOnInit(){
    try {
      this.restaurant.set(await this.restaurantsService.getRestaurantById(this.id()))
      await this.productsService.getProductsByRestaurant(this.id())
      await this.categoriesService.getCategoriesByRestaurant(this.id())
    } finally {
      this.loading.set(false);
    }
  }
}
