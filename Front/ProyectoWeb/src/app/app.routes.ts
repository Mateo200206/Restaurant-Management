import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { RestaurantMenu } from './pages/restaurant-menu/restaurant-menu';
import { ProductDetail } from './pages/product-detail/product-detail';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Admin } from './pages/admin/admin';
import { onlyOwnerGuard } from './guards/only-owner-guard';
import { onlyPublicGuard } from './guards/only-public-guard';
import { ProductForm } from './pages/product-form/product-form';
import { Account } from './pages/account/account';


export const routes: Routes = [
    {
        path: "",
        component: Home
    },
    {
        path: "restaurant/:id",
        component: RestaurantMenu
    },
    {
        path: "product/:id",
        component: ProductDetail
    },
    {
        path: "login",
        component: Login,
        canActivate: [onlyPublicGuard]
    },
    {
        path:"register",
        component: Register,
        canActivate: [onlyPublicGuard]
    },
    {
        path: "admin",
        component: Admin,
        canActivate: [onlyOwnerGuard]
    },
    {
        path: "admin/products/new",
        component: ProductForm,
        canActivate: [onlyOwnerGuard]
    },
    {
        path: "admin/products/:id/edit",
        component: ProductForm,
        canActivate: [onlyOwnerGuard]
    },
    {
        path: "admin/account",
        component: Account,
        canActivate: [onlyOwnerGuard]
    },
];
