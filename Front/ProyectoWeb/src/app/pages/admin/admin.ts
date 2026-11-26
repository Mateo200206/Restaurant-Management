import { Component, inject, OnInit, signal, input } from '@angular/core';
import { ProductsService } from '../../services/products-service';
import { RouterLink } from '@angular/router';
import { CategoriesService } from '../../services/categories-service';
import { required, maxLength, form, FormField } from '@angular/forms/signals';
import { Category } from '../../interfaces/category';
import Swal from 'sweetalert2';
import { Spinner } from '../../components/spinner/spinner';

@Component({
  selector: 'app-admin',
  imports: [RouterLink, FormField, Spinner],
  templateUrl: './admin.html',
  styleUrl: './admin.scss',
})
export class Admin implements OnInit {
  productsService = inject(ProductsService)
  categoriesService = inject(CategoriesService)
  editingCategoryId = signal<number | null>(null);
  loading = signal(true);

  async ngOnInit(){
    await this.productsService.getMyProducts();
    await this.categoriesService.getMyCategories();
    this.loading.set(false);
  }

  async onDeleteProduct(id: number) {
    const result = await Swal.fire({
      title: "¿Eliminar producto?",
      text: "No se puede deshacer.",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Eliminar",
      cancelButtonText: "Cancelar"
    });
    if (!result.isConfirmed) return;

    const ok = await this.productsService.deleteProduct(id);
    if (!ok) Swal.fire("Error", "No se pudo eliminar el producto", "error");
  }

  async onToggleHappyHour(id: number, value: boolean) {
    const ok = await this.productsService.setHappyHour(id, value);
    if (!ok) Swal.fire("Error", "No se pudo cambiar el happy hour", "error");
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
    const result = await Swal.fire({
      title: "¿Eliminar categoría?",
      text: "También se borran todos sus productos. No se puede deshacer.",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Eliminar",
      cancelButtonText: "Cancelar"
    });
    if (!result.isConfirmed) return;

    const ok = await this.categoriesService.deleteCategory(id);
    if (!ok) Swal.fire("Error", "No se pudo eliminar la categoría", "error");
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
    if (!ok) Swal.fire("Error", "No se pudo exportar. ¿Tenés productos cargados?", "error");
  }
}
