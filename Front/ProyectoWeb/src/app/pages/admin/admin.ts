import { Component, inject, OnInit, signal, input } from '@angular/core';
import { ProductsService } from '../../services/products-service';
import { RouterLink } from '@angular/router';
import { CategoriesService } from '../../services/categories-service';
import { required, maxLength, form, FormField } from '@angular/forms/signals';
import { Category } from '../../interfaces/category';

@Component({
  selector: 'app-admin',
  imports: [RouterLink, FormField],
  templateUrl: './admin.html',
  styleUrl: './admin.scss',
})
export class Admin implements OnInit {
  productsService = inject(ProductsService)
  categoriesService = inject(CategoriesService)
  editingCategoryId = signal<number | null>(null);

  ngOnInit(){
    this.productsService.getMyProducts();
    this.categoriesService.getMyCategories();
  }

  async onDeleteProduct(id: number) {
    if (!confirm("¿Seguro que querés eliminar este producto?")) return;
    const ok = await this.productsService.deleteProduct(id);
    if (!ok) alert("No se pudo eliminar el producto");
  }

  categoryModel = signal({ name: "" });
  categoryForm = form(this.categoryModel, (schema) => {
    required(schema.name);
    maxLength(schema.name, 50);
  });

  async onSubmitCategory(event: Event){
    event.preventDefault();
    const id = this.editingCategoryId();
    let res;
    if (id){
      res = await this.categoriesService.updateCategory(id, this.categoryModel().name);
    }else{
      res = await this.categoriesService.createCategory(this.categoryModel().name)
    }
    if (res){
      this.categoryModel.set({name:""});
      this.editingCategoryId.set(null);
    }
  }

  async onDeleteCategory(id: number) {
    if (!confirm("¿Eliminar la categoría? También se borran todos sus productos.")) return;
    const ok = await this.categoriesService.deleteCategory(id);
    if (!ok) alert("No se pudo eliminar la categoría");
  }

  async onToggleHappyHour(id: number, value: boolean) {
    const response = await this.productsService.setHappyHour(id, value);
    if (!response) alert("No se pudo cambiar el happy hour");
  }

  onEditCategory(category: Category) {
    this.editingCategoryId.set(category.id);
    this.categoryModel.set({ name: category.name });
  }

  onCancelEdit(){
    this.categoryModel.set({name:""});
    this.editingCategoryId.set(null);
  }

  async onExportMenu() {
    const ok = await this.productsService.exportMenu();
    if (!ok) alert("No se pudo exportar. ¿Tenés productos cargados?");
  }
}
