import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ProductDetails } from '../../app/ui/shopping/product-details/product-details.component';
import { apiUrl } from './bff-config';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  baseClientUrl = apiUrl + "/product"

  constructor(private http: HttpClient) {}

  getProductDetails(productId: string): Observable<ProductDetails> {
    return this.http.get<ProductDetails>(`${this.baseClientUrl}/${productId}`);
  }

  getAllProducts(): Observable<ProductDetails[]> {
    return this.http.get<ProductDetails[]>(`${this.baseClientUrl}/all`);
  }

  getPruductsByIds(productIds: string[]): Observable<ProductDetails[]> {
    return this.http.post<ProductDetails[]>(`${this.baseClientUrl}/all`, productIds);
  }

  getPruductsBySearchTerm(searchTerm: string): Observable<ProductDetails[]> {
    return this.http.get<ProductDetails[]>(`${this.baseClientUrl}/search/${searchTerm}`);
  }
}
