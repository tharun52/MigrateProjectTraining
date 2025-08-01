import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  placeOrder(orderData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Order/place`, orderData);
  }
  getAllOrders(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Order`);
  }
  downloadOrderPdf() {
    return this.http.get(`${this.baseUrl}/Order/export-orders`, { responseType: 'blob' });
  }
}
