import { Component, input, output } from '@angular/core';
import { IProduct } from '../../models/product.model';
import { CurrencyPipe, NgClass } from '@angular/common';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'bot-cart-items',
  imports: [CurrencyPipe, NgClass],
  templateUrl: './cart-items.component.html',
  styleUrl: './cart-items.component.css',
})

export class CartItemsComponent {
  product = input.required<IProduct>();
  itemRemoved = output<number>();

  constructor( private cartService: CartService) {

  }
    
  getImagePath(): string {
    console.log(this.product())
    return `images/robot-parts/${this.product().imageName}`;
  }

  removeFromCart(event: MouseEvent): void {    
    event.preventDefault();
    const productId = this.product().id;
    this.cartService.removeFromCart(this.product().id).subscribe({
      next: () => {        
        console.log(`Product with ID ${this.product().id} removed from cart.`);
        this.itemRemoved.emit(productId);
      },
      error: (error) => {
        console.error('Error removing product from cart:', error);
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
