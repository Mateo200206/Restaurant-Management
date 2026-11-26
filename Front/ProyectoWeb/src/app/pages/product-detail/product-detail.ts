import { Component, input, signal, inject, OnInit } from '@angular/core';
import { Product } from '../../interfaces/product';
import { ProductsService } from '../../services/products-service';
import { Spinner } from '../../components/spinner/spinner';
import { Location } from '@angular/common';

@Component({
  selector: 'app-product-detail',
  imports: [Spinner],
  templateUrl: './product-detail.html',
  styleUrl: './product-detail.scss',
})
export class ProductDetail implements OnInit {
  id = input.required<number>();
  product = signal<Product | null>(null);
  productsService = inject(ProductsService);
  loading = signal(true);

  location = inject(Location);

  volver() {
    this.location.back();
  }

  async ngOnInit(){
    try {
      this.product.set(await this.productsService.getProductById(this.id()))
    } finally {
      this.loading.set(false);
    }
  }
}
