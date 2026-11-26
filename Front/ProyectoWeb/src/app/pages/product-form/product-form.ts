import { Component, inject, OnInit, signal, input } from '@angular/core';
import { ProductFormModel, ProductRequest } from '../../interfaces/product';
import { CategoriesService } from '../../services/categories-service';
import { ProductsService } from '../../services/products-service';
import { Router } from '@angular/router';
import { form, required, maxLength, min, max, FormField } from '@angular/forms/signals';

@Component({
  selector: 'app-product-form',
  imports: [FormField],
  templateUrl: './product-form.html',
  styleUrl: './product-form.scss',
})
export class ProductForm implements OnInit {
  router = inject(Router)
  categoriesService = inject(CategoriesService)
  productsService = inject(ProductsService);
  id = input<number>();

  async ngOnInit(){
    await this.categoriesService.getMyCategories();
    const id = this.id();
    if (id) {
      const product = await this.productsService.getProductById(id);
      if (product) {
        this.productModel.set({
          name: product.name,
          description: product.description ?? "",
          price: product.price,
          imageUrl: product.imageUrl ?? "",
          isFeatured: product.isFeatured,
          categoryId: String(product.categoryId)
        });
        this.discountModel.set({ discountPercentage: product.discountPercentage });
      }
    }
  }

  productModel = signal<ProductFormModel>({
    name: "",
    description: "",
    price: 0,
    imageUrl: "",
    isFeatured: false,
    categoryId: "",
  });
  productForm = form(this.productModel, (schema) => {
    required(schema.name);
    maxLength(schema.name, 50);
    min(schema.price, 0.01);
    max(schema.price, 100000);
    required(schema.categoryId);
  });
  async onSubmit(event: Event) {
    event.preventDefault();

    const data: ProductRequest = {
      ...this.productModel(),                                 //el spread de objetos: copia todos los campos del modelo.
      categoryId: Number(this.productModel().categoryId)      //pisa ese campo con la versión numérica. Number("3") da 3.
    };

    const id = this.id();
    let res;
    if (id) {
      res = await this.productsService.updateProduct(id, data);
    } else {
      res = await this.productsService.createProduct(data);
    }

    if (res) {
      this.router.navigate(["/admin"]);
    }
  }

  discountModel = signal({ discountPercentage: 0 });
  discountForm = form(this.discountModel, (schema) => {
    min(schema.discountPercentage, 0);
    max(schema.discountPercentage, 100);
  });

  async onSubmitDiscount(event: Event) {
    event.preventDefault();
    const id = this.id();
    if (!id) return;
    const ok = await this.productsService.setDiscount(id, this.discountModel().discountPercentage);
    if (ok) this.router.navigate(["/admin"]);
  }
    
}
