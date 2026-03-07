import { Component, signal, WritableSignal, input } from '@angular/core';
import { NgClass, CurrencyPipe } from '@angular/common';
import { IProduct } from '../../models/product.model';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'bot-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
  imports: [NgClass, CurrencyPipe],
})

export class ProductDetailsComponent {
  product = input.required<IProduct>();
  stock: WritableSignal<number> = signal(10);

  constructor(private cartService: CartService) {

  }
    
  getImagePath(): string {
    return `images/robot-parts/${this.product().imageName}`;
  }

  addCart(event: MouseEvent): void {
    setTimeout(() => {
        this.stock.update((p) => p - 1);
    }, 100);
    const cartItem = {
      ...this.product(),
      price: this.product().price * (1 - this.product().discount / 100)
    };
    this.cartService.addToCart(cartItem).subscribe({
      next: (data) => {
        console.log('Product added to cart:', data);
      },
      error: (error) => {
        console.error('Error adding product to cart:', error);
      }
    });
    console.log(event);
  }

  getPriceClass() {
    return {
      strikethrough: this.product().discount > 0      
    };
  }
}
