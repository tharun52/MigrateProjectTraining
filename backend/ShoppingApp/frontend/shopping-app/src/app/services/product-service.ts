import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Product } from '../models/Product';
import { environment } from '../../environments/environment';



@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/Product/get/${id}`);
  }
  getAllProducts(): Observable<any[]> {
    return this.http.get<any>(`${this.apiUrl}/Product/`).pipe(
      map(response => response['$values'] || [])
    );
  }
}
