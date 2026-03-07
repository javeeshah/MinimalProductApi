import { Inject, Injectable } from '@angular/core';
import { IProduct } from '../models/product.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private baseUrl = 'https://localhost:7007/products';
  constructor(private http: HttpClient) {

  }

  getproducts(): Observable<IProduct[]> {
     return this.http.get<IProduct[]>(this.baseUrl);
  }
}