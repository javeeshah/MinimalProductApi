import { Injectable,  Signal,  signal } from '@angular/core';
import { IProduct } from '../models/product.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseUrl: string = 'https://localhost:7007/cart/products';
  cart: Signal<IProduct[]> = signal([]);
  constructor(private http: HttpClient ) {

  }

  getCart(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.baseUrl);
  }

  addToCart(product: IProduct): Observable<IProduct> {
    console.log('Adding to cart:', product);
    return this.http.post<IProduct>(this.baseUrl, product);
  }
  
  removeFromCart(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${productId}`);
  }
}