import { Component, inject, OnInit } from '@angular/core';
import { RestaurantsService } from '../../services/restaurants-service';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})

export class Home implements OnInit{
  restaurantsService = inject(RestaurantsService);
  ngOnInit() {
    this.restaurantsService.getRestaurants();
  }
}
