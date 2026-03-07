import { Component, inject, signal } from '@angular/core';
import { CartItemsComponent } from "../cart-items/cart-items.compnent";
import { IProduct } from '../../models/product.model';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'bot-cart',
  imports: [CartItemsComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})
export class CartComponent {
  cartItems = signal<IProduct[]>([]);

  constructor(private svc: CartService) {
      this.fetchCartItems();
  } 

  fetchCartItems() {
    this.svc.getCart().subscribe({
      next: (data) => {
        this.cartItems.set(data);
      },
      error: (error) => {
        console.error('Error fetching cart items:', error);
      }
    });
  }

  onItemRemoved(productId: number) {
    this.cartItems.update(items =>
      items.filter(item => item.id !== productId)
    );
  }
}
