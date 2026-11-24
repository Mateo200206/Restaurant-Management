import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { RestaurantMenu } from './pages/restaurant-menu/restaurant-menu';

export const routes: Routes = [
    {
        path: "",
        component: Home
    },
    {
        path: "restaurant/:id",
        component: RestaurantMenu
    }
];
