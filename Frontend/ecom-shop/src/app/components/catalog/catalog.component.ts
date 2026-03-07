import { Component, inject, signal } from '@angular/core';
import { ProductDetailsComponent } from "../product-details/product-details.compnent";
import { ProductsService } from '../../services/products.service';
import { IProduct } from '../../models/product.model';

@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.css',
  imports: [ProductDetailsComponent],
  standalone: true
})
export class CatalogComponent {
  private svc = inject(ProductsService);
  products = signal<IProduct[]>([]);
  loading = signal(false);

  constructor() { 
    this.fetchProducts();
  }

  fetchProducts() {
    this.loading.set(true);
    this.svc.getproducts().subscribe({
      next: (data) => {
        this.products.set(data);
        this.loading.set(false);
      },
      error: (error) => {
        console.error('Error fetching products:', error);
        this.loading.set(false);
      }
    });
  }
}
