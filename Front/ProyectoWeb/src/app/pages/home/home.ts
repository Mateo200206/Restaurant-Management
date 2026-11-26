import { Component, inject, OnInit, signal } from '@angular/core';
import { RestaurantsService } from '../../services/restaurants-service';
import { RouterLink } from '@angular/router';
import { Spinner } from '../../components/spinner/spinner';


@Component({
  selector: 'app-home',
  imports: [RouterLink, Spinner],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})

export class Home implements OnInit{
  restaurantsService = inject(RestaurantsService);
  loading = signal(true);

  async ngOnInit() {
    await this.restaurantsService.getRestaurants();
    this.loading.set(false);
  }
}
