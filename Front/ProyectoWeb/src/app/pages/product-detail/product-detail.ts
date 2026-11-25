import { Component, input, signal, inject, OnInit } from '@angular/core';
import { Product } from '../../interfaces/product';
import { ProductsService } from '../../services/products-service';

@Component({
  selector: 'app-product-detail',
  imports: [],
  templateUrl: './product-detail.html',
  styleUrl: './product-detail.scss',
})
export class ProductDetail implements OnInit {
  id = input.required<number>();
  product = signal<Product | null>(null);
  productsService = inject(ProductsService);

  async ngOnInit(){
    this.product.set(await this.productsService.getProductById(this.id()))
  }
}
